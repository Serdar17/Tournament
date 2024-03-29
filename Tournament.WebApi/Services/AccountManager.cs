﻿using System.Security.Claims;
using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Tournament.Application.Dto;
using Tournament.Application.Dto.Auth;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Participants;
using Tournament.Options;

namespace Tournament.Services;

public sealed class AccountManager : IAccountManager
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtOption _jwtOption;
    private readonly IMapper _mapper;

    public AccountManager(ITokenService tokenService, 
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IOptions<JwtOption> optionsSnapshot, IMapper mapper)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _jwtOption = optionsSnapshot.Value;
    }
    
    public async Task<Result<Response>> RegistrationAsync(ApplicationUser participant)
    {
        var userExists = await _userManager.FindByNameAsync(participant.UserName);
        if (userExists is not null)
        {
            return Result.Error("Пользователь с такой почтой уже существует.");
        }
        
        participant.PasswordHash = _userManager.PasswordHasher.HashPassword(participant, participant.PasswordHash);
        
        participant.SetRating();
        
        var result = await _userManager.CreateAsync(participant);
        if (!result.Succeeded)
        {
            return Result.Error("User creation failed! Please check user details and try again.");
        }

        return Result.Success(new Response()
        {
            Status = "Success",
            Message = "User created successfully!"
        });
    }
 
    public async Task<Result<AuthResponse>> LoginAsync(LoginModel loginModel)
    {
        var participant = await _userManager.Users
            .Where(u => u.UserName == loginModel.Email)
            .Include(u => u.Player)
            .FirstOrDefaultAsync();
        
        if (participant is null || ! await _userManager.CheckPasswordAsync(participant, loginModel.Password))
        {
            return Result.Error("Неверный логин или пароль");
        }
        
        var participantRoles = await _userManager.GetRolesAsync(participant);
        
        var authClaims = new List<Claim>()
        {
            new (ClaimTypes.Name, participant.UserName),
            new ("id", participant.Id)
        };

        foreach (var userRole in participantRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        
        var accessToken = _tokenService.GenerateToken(authClaims);
        var refreshToken = _tokenService.GenerateRefreshToken();

        participant.RefreshToken = refreshToken;
        participant.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOption.RefreshTokenExpiryDurationDays);
        
        await _userManager.UpdateAsync(participant);
        
        return Result.Success(new AuthResponse()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = _mapper.Map<UserDto>(participant)
        });
    }

    public async Task<Result<Response>> RegisterAdminAsync(RegisterRoleModel registerRoleModel)
    {
        var participant = await _userManager.FindByNameAsync(registerRoleModel.Email);
        
        if (participant is null)
        {
            return Result.Error("Invalid username");
        }

        var roles = await _userManager.GetRolesAsync(participant);
        if (roles.Count(role => role.Equals(ParticipantRole.Admin)) == 1)
        {
            return Result.Error("The user already has a role");
        }

        var identityResult = await AddRolesIfNotExists(participant, ParticipantRole.Admin);
        
        if (identityResult is null || !identityResult.Succeeded)
        {
            return Result.Error("The administrator role already exists for this user");
        }

        return Result.Success(new Response()
        {
            Status = "Success",
            Message = "The admin role has been successfully added"
        });
    }

    public async Task<Result<Response>> RegisterRefereeAsync(RegisterRoleModel registerRoleModel)
    {
        var participant = await _userManager.FindByNameAsync(registerRoleModel.Email);
        
        if (participant is null)
        {
            return Result.Error("Invalid username");
        } 
        
        var roles = await _userManager.GetRolesAsync(participant);
        if (roles.Count(role => role.Equals(ParticipantRole.Referee)) == 1)
        {
            return Result.Error("The user already has a role");
        }

        var identityResult = await AddRolesIfNotExists(participant, ParticipantRole.Referee);
        
        if (identityResult is null || !identityResult.Succeeded)
        {
            return Result.Error("The administrator role already exists for this user");
        }

        return Result.Success(new Response()
        {
            Status = "Success",
            Message = "The manager role has been successfully added"
        });
    }
    
    private async Task<IdentityResult?> AddRolesIfNotExists(ApplicationUser participant, string role)
    {
        if (!await _roleManager.RoleExistsAsync(ParticipantRole.Participant))
        {
            await _roleManager.CreateAsync(new IdentityRole(ParticipantRole.Participant));
        }
        
        if (!await _roleManager.RoleExistsAsync(ParticipantRole.Referee))
        {
            await _roleManager.CreateAsync(new IdentityRole(ParticipantRole.Referee));
        }
        
        if (!await _roleManager.RoleExistsAsync(ParticipantRole.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(ParticipantRole.Admin));
        }
        
        IdentityResult? result = null;
        if (await _roleManager.RoleExistsAsync(ParticipantRole.Admin))
        {
            result = await _userManager.AddToRolesAsync(participant, new List<string>()
            {
                role
            });
        }

        return result;
    }

    public async Task<Result<TokenApiModel>> RefreshTokenAsync(TokenApiModel tokenApiModel)
    {
        var accessToken = tokenApiModel.AccessToken;
        var refreshToken = tokenApiModel.RefreshToken;

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

        if (principal is null)
        {
            return Result.Error("Invalid access token or refresh token");
        }

        var userName = principal.Identity.Name;
        var participant = await _userManager.FindByNameAsync(userName);
        
        if (participant == null || participant.RefreshToken != refreshToken ||
            participant.RefreshTokenExpiryTime <= DateTime.Now)
        {
            return Result.Error("Invalid access token or refresh token");
        }
        
        var newAccessToken = _tokenService.GenerateToken(principal.Claims.ToList());
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        
        participant.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(participant);

        return Result.Success(new TokenApiModel()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        });
    }

    public async Task<Result<Response>> RevokeRefreshTokenByIdAsync(Guid id)
    {
        var participant = await _userManager.FindByIdAsync(id.ToString());

        if (participant is null)
        {
            return Result<Response>.Error("Invalid id");
        }

        participant.RefreshToken = null;
        await _userManager.UpdateAsync(participant);

        return Result.Success(new Response()
        {
            Status = "Success",
            Message = "The refresh token has been canceled"
        });
    }

    public async Task<Result<Response>> RevokeAllRefreshTokenAsync()
    {
        var participants = _userManager.Users.ToList();

        foreach (var participant in participants)
        {
            participant.RefreshToken = null;
            await _userManager.UpdateAsync(participant);
        }

        return Result.Success(new Response()
        {
            Status = "Success",
            Message = "All tokens have been cancelled"
        });
    }
}