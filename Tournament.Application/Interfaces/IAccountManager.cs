using Ardalis.Result;
using Tournament.Domain.Models;
using Tournament.Dto;

namespace Tournament.Application.Interfaces;

public interface IAccountManager
{
    public Task<Result<Response>> RegistrationAsync(Participant participant);

    public Task<Result<TokenApiModel>> LoginAsync(LoginModel loginModel);

    public Task<Result<Response>> RegisterAdminAsync(RegisterRoleModel registerRoleModel);
    
    public Task<Result<Response>> RegisterManagerAsync(RegisterRoleModel registerRoleModel);

    public Task<Result<TokenApiModel>> RefreshTokenAsync(TokenApiModel tokenApiModel);
    
    public Task<Result<Response>> RevokeRefreshTokenByIdAsync(Guid id);

    public Task<Result<Response>> RevokeAllRefreshTokenAsync();
}