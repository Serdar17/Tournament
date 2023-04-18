using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tournament.Application.Common.Mappings;
using MediatR;

namespace Tournament.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<ParticipantProfile>();
        });

        return services;
    }
}