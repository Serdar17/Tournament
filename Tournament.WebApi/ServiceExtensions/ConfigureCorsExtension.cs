namespace Tournament.ServiceExtensions;

public static class ConfigureCorsExtension
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("EnableCORS", cfg => 
            { 
                cfg.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod(); 
            });
        });
    }
}