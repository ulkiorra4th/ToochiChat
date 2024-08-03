using System.Text;
using ToochiChat.Infrastructure.EmailService.Interfaces;

namespace ToochiChat.Infrastructure.EmailService.Implementations.Content;

internal sealed class EmailContentBuilder : IEmailContentBuilder
{
    private readonly IHtmlParser _htmlParser;
    
    public char InsertLinkSymbol { get; set; }
    public string Separator { get; set; }

    public EmailContentBuilder(IHtmlParser htmlParser)
    {
        _htmlParser = htmlParser;
        InsertLinkSymbol = '^';
        Separator = "---";
    }

    public EmailContentBuilder(char insertLinkSymbol, string separator, IHtmlParser htmlParser) : this(htmlParser)
    {
        InsertLinkSymbol = insertLinkSymbol;
        Separator = separator;
    }
    
    public EmailContent BuildMailContent(string subject, string body, string confirmationLink, bool isBodyHtml = false)
    {
        body = InsertConfirmLinkToBody(confirmationLink, body);
        
        return new EmailContent(subject, body, isBodyHtml);
    }
    
    public EmailContent BuildMailContentFromHtml(string htmlFilePath, string confirmationLink)
    {
        var (subject, body) = _htmlParser.Parse(htmlFilePath, Separator);
        body = InsertConfirmLinkToBody(confirmationLink, body);
        
        return new EmailContent(subject, body, isBodyHtml: true);
    }
    
    private string InsertConfirmLinkToBody(string confirmationLink, string body)
    {
        int insertSymbolIndex = body.IndexOf(InsertLinkSymbol);
        if (insertSymbolIndex == -1) throw new InvalidDataException("Insert link symbol has not found");
        
        var sb = new StringBuilder(body);
        
        sb.Insert(insertSymbolIndex + 1, $"{confirmationLink}");
        sb.Remove(insertSymbolIndex, 1);

        return sb.ToString();
    }
}