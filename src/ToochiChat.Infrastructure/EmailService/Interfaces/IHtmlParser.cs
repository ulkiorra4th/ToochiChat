namespace ToochiChat.Infrastructure.EmailService.Interfaces;

public interface IHtmlParser
{
    public string Separator { get; init; }
    public (string, string) Parse(string filePath);
}