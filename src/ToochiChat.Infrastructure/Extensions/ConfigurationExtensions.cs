using Microsoft.Extensions.Configuration;

namespace ToochiChat.Infrastructure.Extensions;

internal static class ConfigurationExtensions
{
    public static string? GetDomainName(this IConfiguration configuration)
    {
        return configuration
            .GetSection("EmailConfirmationService")
            .GetValue<string>("Domain");
    }

    public static string? GetControllerName(this IConfiguration configuration)
    {
        return configuration
            .GetSection("EmailConfirmationService")
            .GetValue<string>("Controller");
    }
    
    public static string? GetActionName(this IConfiguration configuration)
    {
        return configuration
            .GetSection("EmailConfirmationService")
            .GetValue<string>("Action");
    }
    
    public static string? GetEmailHtmlFilePath(this IConfiguration configuration)
    {
        return configuration
            .GetSection("EmailConfirmationService")
            .GetValue<string>("HtmlFilePath");
    }
    
    public static IConfigurationSection GetEmailSenderConfiguration(this IConfiguration configuration)
    {
        return configuration
            .GetSection("EmailConfirmationService")
            .GetSection("EmailSenderConfiguration");
    }
}