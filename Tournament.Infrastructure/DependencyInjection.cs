using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;
using Tournament.Infrastructure.DbContext;
using Tournament.Infrastructure.Repositories;

namespace Tournament.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetValue<string>("ConnectionString:DefaultConnection"));
        });
        
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            
        services.AddTransient<IApplicationDbContext>(provider =>
            provider.GetService<ApplicationDbContext>());

        services.AddTransient<IParticipantRepository, ParticipantRepository>();

        services.AddTransient<ICompetitionRepository, CompetitionRepository>();

        services.AddTransient<IPlayerRepository, PlayerRepository>();

        services.AddTransient<IScheduleRepository, ScheduleRepository>();

        services.AddTransient<IMatchResultRepository, MatchResultRepository>();
        
        return services;
    }
}