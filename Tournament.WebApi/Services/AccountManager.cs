using System.Security.Claims;
using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Participant;
using Tournament.Dto;
using Tournament.Options;

namespace Tournament.Services;

public sealed class AccountManager : IAccountManager
{
    private readonly ITokenService _tokenService;
    private readonly UserManager<Participant> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtOption _jwtOption;

    public AccountManager(ITokenService tokenService, UserManager<Participant> userManager, 
        RoleManager<IdentityRole> roleManager, IOptionsSnapshot<JwtOption> optionsSnapshot)
    {
        _tokenService = tokenService;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtOption = optionsSnapshot.Value;
    }
    
    public async Task<Result<Response>> RegistrationAsync(Participant participant)
    {
        var userExists = await _userManager.FindByNameAsync(participant.UserName);
        if (userExists is not null)
        {
            return Result.Error("User already exists!");
        }
        
        participant.PasswordHash = _userManager.PasswordHasher.HashPassword(participant, participant.PasswordHash);
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

    public async Task<Result<TokenApiModel>> LoginAsync(LoginModel loginModel)
    {
        var participant = await _userManager.FindByNameAsync(loginModel.UserName);
        
        if (participant is null || ! await _userManager.CheckPasswordAsync(participant, loginModel.Password))
        {
            return Result.Error("Invalid username or password");
        }
        
        var participantRoles = await _userManager.GetRolesAsync(participant);
        
        var authClaims = new List<Claim>()
        {
            new (ClaimTypes.Name, participant.UserName),
            new (ClaimTypes.Email, participant.Email),
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

        return Result.Success(new TokenApiModel()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    public async Task<Result<Response>> RegisterAdminAsync(RegisterRoleModel registerRoleModel)
    {
        var participant = await _userManager.FindByNameAsync(registerRoleModel.UserName);
        
        if (participant is null)
        {
            return Result.Error("Invalid username");
        }

        var roles = await _userManager.GetRolesAsync(participant);
        if (roles.Count(role => role.Equals(ParticipantRole.Admin)) == 1)
        {
            return Result.Error("The user already has a role");
        }
        
        if (!await _roleManager.RoleExistsAsync(ParticipantRole.Participant))
        {
            await _roleManager.CreateAsync(new IdentityRole(ParticipantRole.Participant));
        }
        
        if (!await _roleManager.RoleExistsAsync(ParticipantRole.Manager))
        {
            await _roleManager.CreateAsync(new IdentityRole(ParticipantRole.Manager));
        }
        
        if (!await _roleManager.RoleExistsAsync(ParticipantRole.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(ParticipantRole.Admin));
        }

        IdentityResult result = null;
        if (await _roleManager.RoleExistsAsync(ParticipantRole.Admin))
        {
            result = await _userManager.AddToRolesAsync(participant, new List<string>()
            {
                ParticipantRole.Admin
            });
        }

        if (result is null || !result.Succeeded)
        {
            return Result.Error("The administrator role already exists for this user");
        }

        return Result.Success(new Response()
        {
            Status = "Success",
            Message = "The admin role has been successfully added"
        });
    }

    public async Task<Result<Response>> RegisterManagerAsync(RegisterRoleModel registerRoleModel)
    {
        var participant = await _userManager.FindByNameAsync(registerRoleModel.UserName);
        
        if (participant is null)
        {
            return Result.Error("Invalid username");
        } 
        
        var roles = await _userManager.GetRolesAsync(participant);
        if (roles.Count(role => role.Equals(ParticipantRole.Manager)) == 1)
        {
            return Result.Error("The user already has a role");
        }
        
        if (!await _roleManager.RoleExistsAsync(ParticipantRole.Participant))
        {
            await _roleManager.CreateAsync(new IdentityRole(ParticipantRole.Participant));
        }
        
        if (!await _roleManager.RoleExistsAsync(ParticipantRole.Manager))
        {
            await _roleManager.CreateAsync(new IdentityRole(ParticipantRole.Manager));
        }

        IdentityResult result = null;
        if (await _roleManager.RoleExistsAsync(ParticipantRole.Manager))
        {
            result = await _userManager.AddToRolesAsync(participant, new List<string>()
            {
                ParticipantRole.Manager, ParticipantRole.Participant
            });
        }
        
        if (result is null || !result.Succeeded)
        {
            return Result.Error("The administrator role already exists for this user");
        }

        return Result.Success(new Response()
        {
            Status = "Success",
            Message = "The manager role has been successfully added"
        });
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