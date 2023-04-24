using Tournament.Infrastructure.DbContext;

namespace Tournament.Infrastructure;

public class DbInitializer
{
    public static void Initialize(CompetitionDbContext context)
    {
        context.Database.EnsureCreated();
    }
}