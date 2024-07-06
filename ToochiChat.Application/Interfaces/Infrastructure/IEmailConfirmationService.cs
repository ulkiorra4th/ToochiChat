namespace ToochiChat.Application.Interfaces.Infrastructure;

public interface IEmailConfirmationService
{
    void SendConfirmationEmail(string email, string confirmationToken);
    string GenerateConfirmationToken();
}