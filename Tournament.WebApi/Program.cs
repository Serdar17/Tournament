using System.Reflection;
using Serilog;
using Tournament.Application;
using Tournament.Application.Common.Mappings;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.BackgroundServices;
using Tournament.Infrastructure;
using Tournament.Middleware;
using Tournament.Options;
using Tournament.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    cfg.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
});

builder.Services.AddHostedService<TournamentBackgroundService>();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(JwtOption.Section));

builder.Services.AddServices();

builder.Services.AddCoreAdmin();

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

}
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();
app.UseCustomExceptionHandle();

app.UseExceptionHandler("/Error");
app.UseHsts();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("EnableCORS");

app.UseAuthentication();
app.UseAuthorization();

app.UseCoreAdminCustomUrl("admin");
app.UseCoreAdminCustomTitle("Панель администратора");

app.UseMiddleware<JwtMiddleware>();

// app.MapDefaultControllerRoute();
app.MapControllers();

app.Run();