using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToochiChat.Application.Auth.Interfaces.Infrastructure;
using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Infrastructure.Authentication;
using ToochiChat.Infrastructure.EmailService.Implementations;
using ToochiChat.Infrastructure.EmailService.Implementations.Content;
using ToochiChat.Infrastructure.EmailService.Implementations.Net;
using ToochiChat.Infrastructure.EmailService.Interfaces;
using ToochiChat.Infrastructure.Security;

namespace ToochiChat.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmailService(this IServiceCollection services,  IConfiguration configuration)
    {
        var emailSenderConfiguration = configuration.GetEmailSenderConfiguration();
        var htmlTemplatePath = configuration.GetEmailHtmlFilePath();
        
        services.TryAddSingleton<IHtmlParser, HtmlParser>();

        services.TryAddSingleton<IEmailContentBuilder>(sp =>
        {
            var htmlParser = sp.GetRequiredService<IHtmlParser>();
            (string subject, string body) = htmlParser.Parse(htmlTemplatePath!);
            return new EmailContentBuilder(subject, body);
        });
        
        services.TryAddSingleton<IEmailSender>(new EmailSender(
            emailSenderConfiguration.GetValue<string>("SenderAddress")!, 
              emailSenderConfiguration.GetValue<string>("DisplayName")!, 
              emailSenderConfiguration.GetValue<string>("SenderMailPassword")!, 
            emailSenderConfiguration.GetValue<string>("SmtpHost")!, 
              emailSenderConfiguration.GetValue<int>("SmtpPort")));
        
        services.TryAddSingleton<IEmailVerificationService, EmailVerificationService>();

        return services;
    }
    
    public static IServiceCollection AddJwtTokens(this IServiceCollection services, IConfiguration configuration)
    {
        services.TryAddSingleton<IJwtProvider, JwtProvider>();
        services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
        
        return services;
    }

    public static IServiceCollection AddPasswordSecurityService(this IServiceCollection services)
    {
        services.TryAddSingleton(RandomNumberGenerator.Create());
        services.TryAddSingleton<IPasswordSecurityService, PasswordSecurityService>();
        
        return services;
    }

    public static IServiceCollection AddHashingService(this IServiceCollection services)
    {
        services.TryAddSingleton<IHashingService, HashingService>();
        return services;
    }
}