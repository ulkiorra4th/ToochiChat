using System.Text;
using ToochiChat.Infrastructure.EmailService.Interfaces;

namespace ToochiChat.Infrastructure.EmailService.Implementations.Content;

internal sealed class HtmlParser : IHtmlParser
{
    private const int SubjectIndex = 0;
    private const int BodyIndex = 1;
    
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

            string subject = splittedContent[SubjectIndex]
                .Replace("\n", String.Empty)
                .Replace("\t", String.Empty)
                .Replace("\r", String.Empty);
            string body = splittedContent[BodyIndex];

            return (subject, body);
        }
    }
}