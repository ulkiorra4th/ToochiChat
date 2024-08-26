namespace ToochiChat.Application.Interfaces.Infrastructure;

public interface IEmailVerificationService
{
    void PutEmailInQueue(string email, string verificationCode);
    Task TrySendEmailFromQueue();
    public bool VerifyEmail(string email, string code);
}