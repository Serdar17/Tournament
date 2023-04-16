using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tournament.DbContext;
using Tournament.Extensions;
using Tournament.Implementation;
using Tournament.MappingConfiguration;
using Tournament.Middleware;
using Tournament.Models;
using Tournament.Options;
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

builder.Services.Configure<JwtOption>(builder.Configuration.GetSection(JwtOption.Section));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ParticipantProfile>();
});

builder.Services.AddIdentity<Participant, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IParticipantService, ParticipantService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAccountManager, AccountManager>();

#region Configure Serilog

builder.Host.ConfigureSerilog();

#endregion

#region Cors Configure

builder.Services.ConfigureCors();

#endregion

#region Swagger Configuration

builder.Services.ConfigureSwagger();

#endregion

#region Authentication

builder.Services.ConfigureAuthentication(builder);

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/Error");
app.UseHsts();

// app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseCors("EnableCORS");

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();
app.Run();