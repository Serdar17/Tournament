using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tournament.Application;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Interfaces;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Participants;
using Tournament.Infrastructure;
using Tournament.Data;
using Tournament.Middleware;
using Tournament.Options;
using Tournament.ServiceExtensions;
using Tournament.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetValue<string>("ConnectionString:DefaultConnection"));
});
        
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IApplicationDbContext>(provider =>
    provider.GetService<ApplicationDbContext>());

builder.Services.AddAutoMapper( cfg =>
{
    cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(JwtOption.Section));

builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountManager, AccountManager>();

builder.Services.AddScoped<IScheduleService, ScheduleService>();

builder.Services.AddSingleton<ICurrentUserService, CurrentUserServices>();
// builder.Services.AddCoreAdmin();

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

builder.Host.ConfigureSerilog(builder.Configuration);

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCustomExceptionHandle();

app.UseExceptionHandler("/Error");
app.UseHsts();

app.UseSerilogRequestLogging();
// app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("EnableCORS");

app.UseAuthentication();
app.UseAuthorization();



// app.UseCoreAdminCustomUrl("admin");
// app.UseCoreAdminCustomTitle("Панель администратора");

app.UseMiddleware<JwtMiddleware>();

// app.MapDefaultControllerRoute();
app.MapControllers();

app.Run();