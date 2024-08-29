using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Auth.Interfaces.Infrastructure;
using ToochiChat.Application.Interfaces.Infrastructure;

namespace ToochiChat.Infrastructure.Tests.EmailService;

public sealed class EmailConfirmationServiceTest
{
    private readonly IEmailVerificationService _emailVerificationService;
    private readonly IConfiguration _configuration;
    private readonly IPasswordSecurityService _passwordSecurityService;
    
    public EmailConfirmationServiceTest()
    {
        _emailVerificationService = Services.Provider.GetRequiredService<IEmailVerificationService>();
        _configuration = Services.Provider.GetRequiredService<IConfiguration>();
        _passwordSecurityService = Services.Provider.GetRequiredService<IPasswordSecurityService>();
    }

    [Fact]
    public async Task<(string, string)> EmailSendTest()
    {
        var token = _passwordSecurityService.GenerateVerificationCode();
        var receiverAddress = _configuration
            .GetSection("EmailConfirmationService")
            .GetSection("TestData")
            .GetValue<string>("ReceiverAddress") ?? throw new Exception("invalid json config");
        
        _emailVerificationService.PutEmailInQueue(receiverAddress, token);
        await Task.Delay(TimeSpan.FromSeconds(3));
        await _emailVerificationService.TrySendEmailFromQueue();
        return (receiverAddress, token);
    }

    [Fact]
    public async Task VerifyEmailTest()
    {
        (string email, string code ) = await EmailSendTest();
        var verifyResult = _emailVerificationService.VerifyEmail(email, code);
        
        Assert.True(verifyResult);
    }
    
}