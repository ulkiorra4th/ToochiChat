using Microsoft.Extensions.Caching.Memory;
using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Infrastructure.EmailService.Implementations.Content;
using ToochiChat.Infrastructure.EmailService.Interfaces;

namespace ToochiChat.Infrastructure.EmailService.Implementations;

internal sealed class EmailVerificationService : IEmailVerificationService
{
    private const int CacheLifeTimeInMinutes = 3;
    
    private readonly Queue<EmailModel> _emails = new();
    // TODO: use redis
    private readonly IMemoryCache _cache;

    private readonly IEmailContentBuilder _emailContentBuilder;
    private readonly IEmailSender _emailSender;

    public EmailVerificationService(IEmailContentBuilder emailContentBuilder, IEmailSender emailSender, 
        IMemoryCache cache)
    {
        _emailContentBuilder = emailContentBuilder;
        _emailSender = emailSender;
        _cache = cache;
    }

    public void PutEmailInQueue(string email, string verificationCode)
    {
        var emailContent = _emailContentBuilder.BuildMailContent(verificationCode, isBodyHtml: true);
        
        _cache.Set(email, verificationCode, 
            absoluteExpirationRelativeToNow: TimeSpan.FromMinutes(CacheLifeTimeInMinutes));
        _emails.Enqueue(new EmailModel(email, emailContent));
    }

    public async Task TrySendEmailFromQueue()
    {
        if (_emails.Count == 0) return;
        
        var emailModel = _emails.Dequeue();
        await _emailSender.SendMailAsync(emailModel.To, emailModel.Content);
    }

    public bool VerifyEmail(string email, string code)
    {
        if (!_cache.TryGetValue(email, out string? savedCode)) return false;
        if (code != savedCode) return false;
        
        _cache.Remove(email);
        return true;
    }
}