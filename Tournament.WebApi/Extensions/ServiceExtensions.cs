using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace Tournament.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("EnableCORS", cfg => 
            { 
                cfg.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod(); 
            });
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            //This is to generate the Default UI of Swagger Documentation
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "JWT Token Authentication API",
                Description = "ASP.NET Core 7.0 Web API"
            });
            // To Enable authorization using Swagger (JWT)
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }

    public static void ConfigureAuthentication(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtOption:Issuer"],
                ValidAudience = configuration["JwtOption:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOption:Key"]))
            };
        });
    }

    public static void ConfigureSerilog(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((ctx, lc) => lc
            .WriteTo.Console(LogEventLevel.Information,
                outputTemplate:
                "{Timestamp:HH:mm:ss:ms} LEVEL:[{Level}]| THREAD:|{ThreadId}| Source: |{Source}| {Message}{NewLine}{Exception}")
            .WriteTo.Seq("http://localhost:5341")
            .WriteTo.File("log.txt")
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning));
    }
}