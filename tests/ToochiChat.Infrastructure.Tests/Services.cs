// #define USE_FAKE_EMAIL_SENDER 

using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Tests.Shared;

namespace ToochiChat.Infrastructure.Tests;

internal static class Services
{
    public static IServiceProvider Provider { get; }
    
    static Services()
    {
        var servicesBuilder = new ServicesBuilder()
            .AddEmailService()
            .AddPasswordSecurityService()
            .AddMemoryCache();
        
#if USE_FAKE_EMAIL_SENDER
        servicesBuilder.AddDemoEmailService();
#endif        
        
        Provider = servicesBuilder.Build().BuildServiceProvider();
    }
}