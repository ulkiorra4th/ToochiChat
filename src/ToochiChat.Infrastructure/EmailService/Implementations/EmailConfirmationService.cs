using System.Security.Cryptography;
using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Infrastructure.EmailService.Data;
using ToochiChat.Infrastructure.EmailService.Interfaces;

namespace ToochiChat.Infrastructure.EmailService.Implementations;

internal sealed class EmailConfirmationService : IEmailConfirmationService
{
    private readonly Configuration _config;
    private readonly IEmailContentBuilder _emailContentBuilder;
    private readonly IEmailSender _emailSender;

    public EmailConfirmationService(IEmailContentBuilder emailContentBuilder, IEmailSender emailSender, 
        Configuration config)
    {
        _emailContentBuilder = emailContentBuilder;
        _emailSender = emailSender;
        _config = config;
    }

    public string GenerateConfirmationToken()
    {
        return Guid.NewGuid().ToString();
    }
    
    public void ConfirmEmail(int id)
    {
        throw new NotImplementedException();
    }

    public void SendConfirmationEmail(string email, string confirmationToken)
    {
        var confirmationUrl = BuildUrl(_config.Domain, _config.Controller, _config.Action, email, confirmationToken);
        var emailContent = _emailContentBuilder.BuildMailContentFromHtml(_config.HtmlFilePath, confirmationUrl);
        
        _emailSender.SendMailAsync(email, emailContent);
    }
    
    private string BuildUrl(string domain, string controller, string action, string email, string token)
    {
        string hashedEmail;
        
        using (HashAlgorithm sha256 = SHA256.Create())
        {
            var result = sha256.ComputeHash(System.Text.Encoding.ASCII.GetBytes(email));
            hashedEmail = Convert.ToString(result)!;
        }

        return $"{domain}/{controller}/{action}/{hashedEmail}/{token}";
    }
}