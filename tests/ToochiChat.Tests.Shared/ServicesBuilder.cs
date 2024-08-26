using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToochiChat.Application.Auth.Interfaces.Infrastructure;
using ToochiChat.Application.Extensions;
using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Infrastructure.EmailService.Interfaces;
using ToochiChat.Infrastructure.Extensions;
using ToochiChat.Persistence.FileSystem.Extensions;
using ToochiChat.Tests.Shared.DemoServices;
using ToochiChat.Tests.Shared.Extensions;

namespace ToochiChat.Tests.Shared;

public sealed class ServicesBuilder
{
    private readonly IServiceCollection _services;
    private readonly IConfiguration _configuration;

    public ServicesBuilder(string configurationFileName)
    {
        _services = new ServiceCollection();
        _configuration = _services.AddConfiguration(configurationFileName);
    }
    
    public IServiceCollection Build() => _services;

    public ServicesBuilder AddApplicationServices()
    {
        _services.AddApplicationServices();
        return this;
    }
    
    public ServicesBuilder AddFilesRepository()
    {
        _services.AddFilesRepository(_configuration);
        return this;
    }

    public ServicesBuilder AddMemoryCache()
    {
        _services.AddMemoryCache();
        return this;
    }

    public ServicesBuilder AddHashingService()
    {
        _services.AddHashingService();
        return this;
        
    }

    public ServicesBuilder AddPasswordSecurityService()
    {
        _services.AddPasswordSecurityService();
        return this;
    }

    public ServicesBuilder AddEmailService()
    {
        _services.AddEmailService(_configuration);
        return this;
    }

    public ServicesBuilder AddDemoEmailService()
    {
        _services.AddEmailService(_configuration);
        _services.RemoveAll<IEmailSender>();
        _services.TryAddSingleton<IEmailSender, DemoEmailSender>();

        return this;
    }

    public ServicesBuilder AddDemoEmailVerificationService()
    {
        _services.AddEmailService(_configuration);
        _services.RemoveAll<IEmailVerificationService>();
        _services.TryAddSingleton<IEmailVerificationService, DemoEmailVerificationService>();

        return this;
    }

    public ServicesBuilder AddJwtProvider()
    {
        _services.AddJwtTokens(_configuration);
        return this;
    }

    public ServicesBuilder AddDemoJwtProvider()
    {
        _services.AddJwtTokens(_configuration);
        _services.RemoveAll<IJwtProvider>();
        _services.TryAddSingleton<IJwtProvider, DemoJwtProvider>();
        
        return this;
    }

    public ServicesBuilder AddDemoAccountRepository()
    {
        _services.TryAddSingleton<IAccountRepository, DemoAccountRepository>();

        return this;
    }
}