using ToochiChat.Infrastructure.EmailService.Implementations.Content;

namespace ToochiChat.Infrastructure.EmailService.Interfaces;

public interface IEmailContentBuilder
{
    /// <summary>
    /// The symbol in place of which a link to mail confirmation will be inserted.
    /// </summary>
    public char InsertLinkSymbol { get; set; }

    /// <summary>
    /// String that separates the header and body of a mail
    /// </summary>
    public string Separator { get; set; }
    
    /// <summary>
    /// Creates an instance of MailContent.
    /// </summary>
    /// <param name="subject">The subject of the mail</param>
    /// <param name="body">The body of the mail</param>
    /// <param name="confirmationLink">Individual link to confirm email</param>
    /// <param name="isBodyHtml"></param>
    /// <returns>Mail content</returns>
    public EmailContent BuildMailContent(string subject, string body, string confirmationLink, bool isBodyHtml = false);
    
    /// <summary>
    /// Creates an instance of MailContent from HTML file.
    /// Subject and body of the mail in the file should be separated by "---".
    /// </summary>
    /// <param name="htmlFilePath">The path to the HTML file</param>
    /// <param name="confirmationLink">Individual link to confirm email</param>
    /// <exception cref="InvalidDataException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
    /// <returns>Mail content</returns>
    public EmailContent BuildMailContentFromHtml(string htmlFilePath, string confirmationLink);
}