using Ardalis.Result;
using Tournament.Application.Dto;
using Tournament.Application.Dto.Auth;
using Tournament.Domain.Models;
using Tournament.Domain.Models.Participants;

namespace Tournament.Application.Interfaces;

public interface IAccountManager
{
    public Task<Result<Response>> RegistrationAsync(ApplicationUser participant);

    public Task<Result<AuthResponse>> LoginAsync(LoginModel loginModel);

    public Task<Result<Response>> RegisterAdminAsync(RegisterRoleModel registerRoleModel);
    
    public Task<Result<Response>> RegisterRefereeAsync(RegisterRoleModel registerRoleModel);

    public Task<Result<TokenApiModel>> RefreshTokenAsync(TokenApiModel tokenApiModel);
    
    public Task<Result<Response>> RevokeRefreshTokenByIdAsync(Guid id);

    public Task<Result<Response>> RevokeAllRefreshTokenAsync();
}