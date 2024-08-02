using ToochiChat.Application.Interfaces.Infrastructure;

namespace ToochiChat.Application.Tests.Auth.TestServices;

internal sealed class TestEmailConfirmationService : IEmailConfirmationService
{
    public void SendConfirmationEmail(string email, string confirmationToken) => 
        Console.WriteLine($"from: {email}\nmessage: {confirmationToken}");
    
    public string GenerateConfirmationToken() => Guid.NewGuid().ToString();
}