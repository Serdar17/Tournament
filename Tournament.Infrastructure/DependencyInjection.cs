using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tournament.Domain.Models;
using Tournament.Infrastructure.DbContext;

namespace Tournament.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetValue<string>("ConnectionString:DefaultConnection"));
        });
        
        collection.AddIdentity<Participant, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        return collection;
    }
}