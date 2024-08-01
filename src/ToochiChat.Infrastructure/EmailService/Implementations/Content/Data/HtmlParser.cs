using System.Text;
using ToochiChat.Infrastructure.EmailService.Interfaces;

namespace ToochiChat.Infrastructure.EmailService.Implementations.Content.Data;

internal sealed class HtmlParser : IHtmlParser
{
    private const int SUBJECT_INDEX = 0;
    private const int BODY_INDEX = 1;
    
    public (string, string) Parse(string filePath, string separator)
    {
        if (!File.Exists(filePath)) throw new FileNotFoundException();
        if (String.IsNullOrEmpty(separator)) throw new ArgumentException("Separator shouldn't be null or empty");
        
        using (var sr = new StreamReader(filePath, Encoding.UTF8))
        {
            string content = sr.ReadToEnd();
            var splittedContent = content.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            if (splittedContent.Length != 2) 
                throw new InvalidDataException("Header or body has not found in file");

            string subject = splittedContent[SUBJECT_INDEX];
            string body = splittedContent[BODY_INDEX];

            return (subject, body);
        }
    }
}