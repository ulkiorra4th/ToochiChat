namespace ToochiChat.Infrastructure.EmailService.Implementations.Content;

public sealed record EmailContent(string? Subject, string? Body, bool IsBodyHtml);