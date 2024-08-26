namespace ToochiChat.Application.Interfaces.Infrastructure;

public interface IHashingService
{
    string GetMD5Hash(string str);
}