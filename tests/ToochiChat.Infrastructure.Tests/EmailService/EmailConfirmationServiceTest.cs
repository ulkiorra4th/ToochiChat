using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Interfaces.Infrastructure;

namespace ToochiChat.Infrastructure.Tests.EmailService;

public sealed class EmailConfirmationServiceTest
{
    private readonly IEmailConfirmationService _emailConfirmationService;
    private readonly IConfiguration _configuration;
    
    public EmailConfirmationServiceTest()
    {
        _emailConfirmationService = Services.Provider.GetRequiredService<IEmailConfirmationService>();
        _configuration = Services.Provider.GetRequiredService<IConfiguration>();
    }

    [Fact]
    public void SendConfirmationEmailTest()
    {
        var token = _emailConfirmationService.GenerateConfirmationToken();
        var receiverAddress = _configuration
            .GetSection("EmailConfirmationService")
            .GetSection("TestData")
            .GetValue<string>("ReceiverAddress") ?? throw new Exception("invalid json config");
        
        _emailConfirmationService.SendConfirmationEmail(receiverAddress, token);
    }
}