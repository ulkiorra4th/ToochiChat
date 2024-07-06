using System.Security.Cryptography;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using ToochiChat.Application.Auth.Interfaces.Infrastructure;
using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Infrastructure.Authentication;
using ToochiChat.Infrastructure.EmailService.Data;
using ToochiChat.Infrastructure.EmailService.Implementations;
using ToochiChat.Infrastructure.EmailService.Implementations.Content;
using ToochiChat.Infrastructure.EmailService.Implementations.Content.Data;
using ToochiChat.Infrastructure.EmailService.Implementations.Net;
using ToochiChat.Infrastructure.EmailService.Interfaces;

namespace ToochiChat.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailService(this IServiceCollection services, string configFilePath)
    {
        services.AddSingleton<Configuration>(o => 
            new Configuration(JObject.Parse(File.ReadAllText(configFilePath))));
        
        services.AddSingleton<IHtmlParser, HtmlParser>();
        services.AddSingleton<IEmailContentBuilder, EmailContentBuilder>();
        
        // TODO: read from configuration file
        services.AddSingleton(new EmailSender("address@gmail.com", "name", "password", 
            "smtpHost", 0000));
        
        services.AddSingleton<IEmailSender>(services => services.GetService<EmailSender>()!);
        
        services.AddSingleton<IEmailConfirmationService, EmailConfirmationService>();

        return services;
    }
    
    public static IServiceCollection AddJwtTokens(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        
        return services;
    }

    public static IServiceCollection AddPasswordSecurityService(this IServiceCollection services)
    {
        services.AddSingleton(RandomNumberGenerator.Create());
        services.AddSingleton<IPasswordSecurityService, PasswordSecurityService>();
        
        return services;
    }
}