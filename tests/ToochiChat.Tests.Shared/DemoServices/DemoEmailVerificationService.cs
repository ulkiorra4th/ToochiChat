using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Infrastructure.EmailService.Implementations.Content;

namespace ToochiChat.Tests.Shared.DemoServices;

internal sealed class DemoEmailVerificationService : IEmailVerificationService
{
    private readonly Queue<EmailModel> _emails = new();

    public void PutEmailInQueue(string email, string verificationCode) => 
        _emails.Enqueue(new EmailModel(email, new EmailContent("your code", verificationCode, false)));

    public async Task TrySendEmailFromQueue()
    {
        if (_emails.Count == 0) return;
        
        await Task.Delay(100);
        Console.WriteLine(_emails.Dequeue());
    }

    public bool VerifyEmail(string email, string code)
    {
        return true;
    }

    public string GenerateConfirmationToken() => Guid.NewGuid().ToString();
}