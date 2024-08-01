namespace ToochiChat.Infrastructure.EmailService.Implementations.Content;

public sealed class EmailContent
{
    public string? Subject { get; }
    public string? Body { get; }
    public bool IsBodyHtml { get; }

    internal EmailContent(string subject, string body, bool isBodyHtml)
    {
        Subject = subject;
        Body = body;
        IsBodyHtml = isBodyHtml;
    }
}