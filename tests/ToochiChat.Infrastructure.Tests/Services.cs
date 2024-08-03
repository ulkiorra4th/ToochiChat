// #define USE_FAKE_EMAIL_SENDER 

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToochiChat.Infrastructure.EmailService.Interfaces;
using ToochiChat.Infrastructure.Extensions;
using ToochiChat.Infrastructure.Tests.EmailService.TestServices;
namespace ToochiChat.Infrastructure.Tests;

internal static class Services
{
    public static IServiceProvider Provider { get; }
    
    static Services()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
            .AddJsonFile("testconfig.json")
            .Build();
        
        IServiceCollection services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddEmailService(configuration);
        services.AddPasswordSecurityService();
        
#if USE_FAKE_EMAIL_SENDER
        services.RemoveAll<IEmailSender>();
        services.AddSingleton<IEmailSender, TestEmailSender>();
#endif        
        
        Provider = services.BuildServiceProvider();
    }
}