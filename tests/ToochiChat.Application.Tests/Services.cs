#define USE_FAKE_EMAIL_VERIFICATION_SERVICE

using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Tests.Shared;

namespace ToochiChat.Application.Tests;

internal static class Services
{
    public static IServiceProvider Provider { get; }
    
    static Services()
    {
        var servicesBuilder = new ServicesBuilder()
            .AddPasswordSecurityService()
            .AddDemoAccountRepository()
            .AddEmailService()
            .AddDemoJwtProvider()
            .AddApplicationServices()
            .AddMemoryCache()
            .AddFilesRepository()
            .AddHashingService()
            .ConfigureTestInputDataOptions();
        
#if USE_FAKE_EMAIL_VERIFICATION_SERVICE
        servicesBuilder
            .AddDemoEmailVerificationService();
#endif

        Provider = servicesBuilder.Build().BuildServiceProvider();
    }
}