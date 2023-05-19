using Tournament.Application.Interfaces;
using Tournament.Services;

namespace Tournament.ServiceExtensions;

public static class ConfigureServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IParticipantService, ParticipantService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAccountManager, AccountManager>();

        services.AddScoped<IScheduleService, ScheduleService>();

        services.AddSingleton<ICurrentUserService, CurrentUserServices>();
    }
}