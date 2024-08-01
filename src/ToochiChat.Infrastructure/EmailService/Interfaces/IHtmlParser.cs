namespace ToochiChat.Infrastructure.EmailService.Interfaces;

public interface IHtmlParser
{
    public (string, string) Parse(string filePath, string separator);
}