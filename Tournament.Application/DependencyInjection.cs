using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Tournament.Application.Common.Mappings;
using MediatR;
using Tournament.Application.Behaviors;

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

        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>), 
            typeof(ValidationBehavior<,>));

        return services;
    }
}