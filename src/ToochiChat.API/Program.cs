using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.OpenApi.Models;
using ToochiChat.API.Hubs;
using ToochiChat.Infrastructure.Extensions;
using Serilog;
using ToochiChat.API.Extensions;
using ToochiChat.Application.Extensions;
using ToochiChat.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddRedisCaching(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddSignalR();

#region Logging

builder.Services.AddSerilog(builder.Configuration);
builder.Host.UseSerilog();

#endregion

#region Application Services

builder.Services.AddApplicationServices();

#endregion

#region Persistence

builder.Services.AddDbContextAndRepositories();
builder.Services.AddPersistenceMappers();

#endregion

#region Infrastructure Services

var configFilePath =
    @"E:\GitHubProjects\ToochiChat\ToochiChat.Infrastructure\EmailService\Data\config.json";
builder.Services.AddEmailService(configFilePath);
builder.Services.AddPasswordSecurityService();
builder.Services.AddJwtTokens(builder.Configuration);

#endregion

builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "ToochiChat API", Version = "v1"});
    
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "ToochiChat.API.xml");
    c.IncludeXmlComments(filePath);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSerilogRequestLogging();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<ChatHub>("/chat");
app.MapControllers();

app.Run();