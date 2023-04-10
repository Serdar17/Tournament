using Ardalis.Result;
using Microsoft.AspNetCore.Identity;
using Tournament.Dto;
using Tournament.Models;
using Tournament.Services;

namespace Tournament.Implementation;

public class AccountManager : IAccountManager
{
    private readonly IParticipantService _participantService;
    private readonly ITokenService _tokenService;
    private readonly PasswordHasher<Participant> _hasher;

    public AccountManager(IParticipantService participantService, ITokenService tokenService)
    {
        _participantService = participantService;
        _tokenService = tokenService;
        _hasher = new PasswordHasher<Participant>();
    }
    
    public Result<TokenApiModel> RegistrationAsync(Participant participant)
    {
        if (_participantService.GetParticipantByUserName(participant.Email) is not null)
        {
            return Result<TokenApiModel>.Invalid(new List<ValidationError>()
            {
                new ValidationError()
                {
                    Identifier = participant.Email,
                    ErrorMessage = "User with this username already exists"
                }
            });
        }
        
        participant.Password = _hasher.HashPassword(participant, participant.Password);
        var entity = _participantService.Create(participant);
        
        var accessToken = _tokenService.GenerateAccessToken(entity);
        var refreshToken = _tokenService.GenerateRefreshToken(entity);

        return Result<TokenApiModel>.Success(new TokenApiModel()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }

    public Result<TokenApiModel> LoginAsync(LoginModel loginModel)
    {
        var entity = _participantService.GetParticipantByUserName(loginModel.UserName);
        if (entity is null 
            || _hasher.VerifyHashedPassword(entity, entity.Password, loginModel.Password) != PasswordVerificationResult.Success)
        {
            return Result<TokenApiModel>.Invalid(new List<ValidationError>()
            {
                new ValidationError()
                {
                    Identifier = loginModel.UserName,
                    ErrorMessage = "Invalid username or password"
                }
            });
        }
        
        var accessToken = _tokenService.GenerateAccessToken(entity);
        var refreshToken = _tokenService.GenerateRefreshToken(entity);

        return Result<TokenApiModel>.Success(new TokenApiModel()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        });
    }
}