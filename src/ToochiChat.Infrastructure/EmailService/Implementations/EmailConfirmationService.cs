using Microsoft.Extensions.Configuration;
using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Infrastructure.EmailService.Interfaces;
using ToochiChat.Infrastructure.Extensions;

namespace ToochiChat.Infrastructure.EmailService.Implementations;

internal sealed class EmailConfirmationService : IEmailConfirmationService
{
    private readonly IEmailContentBuilder _emailContentBuilder;
    private readonly IEmailSender _emailSender;
    private readonly IConfiguration _configuration;

    public EmailConfirmationService(IEmailContentBuilder emailContentBuilder, IEmailSender emailSender, 
        IConfiguration configuration)
    {
        _emailContentBuilder = emailContentBuilder;
        _emailSender = emailSender;
        _configuration = configuration;
    }

    public string GenerateConfirmationToken() => Guid.NewGuid().ToString();

    
    public void ConfirmEmail(int id)
    {
        throw new NotImplementedException();
    }

    public void SendConfirmationEmail(string email, string confirmationToken)
    {
        var confirmationUrl = BuildUrl(_configuration.GetDomainName()!, _configuration.GetControllerName()!, 
            _configuration.GetActionName()!, email, confirmationToken);
        var emailContent = _emailContentBuilder.BuildMailContentFromHtml(_configuration.GetEmailHtmlFilePath()!,
            confirmationUrl);
        
        _emailSender.SendMailAsync(email, emailContent);
    }
    
    // TODO: Use code, not url
    private string BuildUrl(string domain, string controller, string action, string email, string token) =>
        $"{domain}/{controller}/{action}/{email.GetHashCode()}/{token}";
}