using Microsoft.Extensions.Configuration;

namespace ToochiChat.Infrastructure.Tests.Extensions;

internal static class ConfigurationExtensions
{
    public static string? GetEmailHtmlFilePath(this IConfiguration configuration)
    {
        return configuration
            .GetSection("EmailConfirmationService")
            .GetValue<string>("HtmlFilePath");
    }
}