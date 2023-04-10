using Ardalis.Result;
using Tournament.Dto;
using Tournament.Models;

namespace Tournament.Services;

public interface IAccountManager
{
    public Result<TokenApiModel> RegistrationAsync(Participant participant);

    public Result<TokenApiModel> LoginAsync(LoginModel loginModel);
}