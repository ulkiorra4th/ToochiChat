using ToochiChat.Application.Interfaces.Infrastructure;

namespace ToochiChat.API.BackgroundServices;

public class EmailSenderBackgroundService : BackgroundService
{
    private readonly IEmailVerificationService _emailVerificationService;

    public EmailSenderBackgroundService(IEmailVerificationService emailVerificationService)
    {
        _emailVerificationService = emailVerificationService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await _emailVerificationService.TrySendEmailFromQueue();
        }
    }
}