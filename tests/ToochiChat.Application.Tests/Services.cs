using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Auth.Interfaces;
using ToochiChat.Application.Auth.Interfaces.Infrastructure;
using ToochiChat.Application.Auth.Services;
using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Application.Tests.Auth.TestServices;
using ToochiChat.Infrastructure.Extensions;

namespace ToochiChat.Application.Tests;

internal static class Services
{
    public static IServiceProvider Provider { get; }
    
    static Services()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddPasswordSecurityService();
        services.AddSingleton<IAccountRepository, TestAccountRepository>();
        services.AddSingleton<IEmailConfirmationService, TestEmailConfirmationService>();
        services.AddSingleton<IJwtProvider, TestJwtProvider>();
        services.AddSingleton<IAccountService, AccountService>();

        Provider = services.BuildServiceProvider();
    }
}