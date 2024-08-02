using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Infrastructure.Extensions;

namespace ToochiChat.Infrastructure.Tests;

internal static class Services
{
    public static IServiceProvider Provider { get; }
    
    static Services()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddEmailService(@"E:\GitHubProjects\ToochiChat\tests\ToochiChat.Infrastructure.Tests\EmailService\TestData\testconfig.json");
        services.AddPasswordSecurityService();

        Provider = services.BuildServiceProvider();
    }
}