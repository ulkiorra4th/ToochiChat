using System.Security.Cryptography;
using System.Text;
using ToochiChat.Application.Interfaces.Infrastructure;

namespace ToochiChat.Infrastructure.Security;

internal sealed class HashingService : IHashingService
{
    public string GetMD5Hash(string str)
    {
        var md5Hash = MD5.Create();
            
        var inputBytes = Encoding.ASCII.GetBytes(str);
        var hashBytes = md5Hash.ComputeHash(inputBytes);
        
        return Convert.ToHexString(hashBytes);
    }
}