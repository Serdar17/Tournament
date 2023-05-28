using Tournament.Application.Interfaces;

namespace Tournament.Services;

sealed class CurrentUserServices : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserServices(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public string UserId
    {
        get
        {
            var id = _httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            return string.IsNullOrEmpty(id)
                ? string.Empty
                : id;
        }
    }
}