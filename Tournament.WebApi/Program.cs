using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using Tournament.Application;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Interfaces;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Extensions;
using Tournament.Infrastructure;
using Tournament.Middleware;
using Tournament.Options;
using Tournament.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
// builder.Services.AddDbContext<CompetitionDbContext>(opt =>
// {
//     opt.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionString:DefaultConnection"));
// });

// builder.Services.AddDbContext<ApplicationDbContext>(opt =>
// {
//     opt.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionString:DefaultConnection"));
// });
//         
// builder.Services.AddIdentity<Participant, IdentityRole>()
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddDefaultTokenProviders();

builder.Services.AddAutoMapper( cfg =>
{
    cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(ICompetitionDbContext).Assembly));
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(JwtOption.Section));

builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountManager, AccountManager>();

builder.Services.AddSingleton<ICurrentUserService, CurrentUserServices>();

#region Cors Configure

builder.Services.ConfigureCors();

#endregion

#region Swagger Configuration

builder.Services.ConfigureSwagger();

#endregion

#region Authentication

builder.Services.ConfigureAuthentication(builder.Configuration);

#endregion

#region Configure Serilog

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.Console()
    .WriteTo.File("TournamentWebApiLog.txt", rollingInterval:
        RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandle();

app.UseExceptionHandler("/Error");
app.UseHsts();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseCors("EnableCORS");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();
app.Run();