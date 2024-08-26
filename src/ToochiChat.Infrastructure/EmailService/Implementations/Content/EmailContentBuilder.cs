using System.Text;
using ToochiChat.Infrastructure.EmailService.Interfaces;

namespace ToochiChat.Infrastructure.EmailService.Implementations.Content;

internal sealed class EmailContentBuilder : IEmailContentBuilder
{
    private readonly string _emailSubject;
    private readonly string _emailBody;

    public char InsertLinkSymbol { get; init; } = '^';

    public EmailContentBuilder(string emailSubject, string emailBody)
    {
        _emailSubject = emailSubject;
        _emailBody = emailBody;
    }

    public EmailContentBuilder(char insertLinkSymbol, string emailSubject, string emailBody) : this(emailSubject, emailBody)
    {
        InsertLinkSymbol = insertLinkSymbol;
    }
    
    public EmailContent BuildMailContent(string verificationToken, bool isBodyHtml = false)
    {
        var body = InsertVerificationTokenToBody(verificationToken, _emailBody);
        return new EmailContent(_emailSubject, body, isBodyHtml);
    }
    
    private string InsertVerificationTokenToBody(string verificationToken, string body)
    {
        int insertSymbolIndex = body.IndexOf(InsertLinkSymbol);
        if (insertSymbolIndex == -1) throw new InvalidDataException("Insert link symbol has not found");
        
        var sb = new StringBuilder(body);
        
        sb.Insert(insertSymbolIndex + 1, $"{verificationToken}");
        sb.Remove(insertSymbolIndex, 1);

        return sb.ToString();
    }
}