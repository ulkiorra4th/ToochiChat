using ToochiChat.Infrastructure.EmailService.Implementations.Content;

namespace ToochiChat.Infrastructure.EmailService.Interfaces;

public interface IEmailContentBuilder
{
    /// <summary>
    /// The symbol in place of which a link to mail confirmation will be inserted.
    /// </summary>
    public char InsertLinkSymbol { get; init; }
    
    /// <summary>
    /// Creates an instance of MailContent.
    /// </summary>
    /// <param name="subject">The subject of the mail</param>
    /// <param name="body">The body of the mail</param>
    /// <param name="verificationToken">Individual link to confirm email</param>
    /// <param name="isBodyHtml"></param>
    /// <returns>Mail content</returns>
    public EmailContent BuildMailContent(string verificationToken, bool isBodyHtml = false);
}