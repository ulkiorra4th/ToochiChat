using System.Data;
using System.Net;
using System.Net.Mail;
using ToochiChat.Infrastructure.EmailService.Implementations.Content;
using ToochiChat.Infrastructure.EmailService.Interfaces;

namespace ToochiChat.Infrastructure.EmailService.Implementations.Net;

internal sealed class EmailSender : IEmailSender
{
    private MailAddress? _sender;
    private string? _senderMailPassword;
    
    private string? _smtpHost;
    private int _smtpPort;
    
    public EmailSender() { }
    
    public EmailSender(MailAddress sender, string senderMailPassword)
    {
        _sender = sender;
        _senderMailPassword = senderMailPassword;
    }

    public EmailSender(MailAddress sender, string senderMailPassword, string smtpHost, int smtpPort) :
        this(sender, senderMailPassword)
    {
        ConfigureSmtp(smtpHost, smtpPort);
    }

    public EmailSender(string senderAddress, string displayName, string senderMailPassword) 
        : this(new MailAddress(senderAddress, displayName), senderMailPassword)
    { }
    
    public EmailSender(string senderAddress, string displayName, string senderMailPassword, string smtpHost, int smtpPort) 
        : this(new MailAddress(senderAddress, displayName), senderMailPassword, smtpHost, smtpPort)
    { }

    public void ConfigureSender(string senderAddress, string displayName, string senderMailPassword, string host, 
        int port)
    {
        SetSenderAddress(senderAddress, displayName, senderMailPassword);
        ConfigureSmtp(host, port);
    }

    public void SetSenderAddress(string senderAddress, string displayName, string senderMailPassword)
    {
        SetSenderAddress(new MailAddress(senderAddress, displayName), senderMailPassword);
    }

    public void SetSenderAddress(MailAddress sender, string senderMailPassword)
    {
        _sender = sender;
        _senderMailPassword = senderMailPassword;
    }
    
    public void ConfigureSmtp(string host, int port)
    {
        _smtpHost = host;
        _smtpPort = port;
    }
    
    public async Task SendMailAsync(MailAddress receiver, EmailContent content)
    {
        var message = new MailMessage(_sender ?? throw new DataException("Sender is null"), receiver)
        {
            Subject = content.Subject,
            Body = content.Body,
            IsBodyHtml = content.IsBodyHtml
        };
        
        using (var smtp = new SmtpClient(_smtpHost, _smtpPort))
        {
            smtp.Credentials = new NetworkCredential(_sender.Address, _senderMailPassword);
            smtp.EnableSsl = true;
            
            await smtp.SendMailAsync(message);
        }
    }

    public async Task SendMailAsync(string receiverMailAddress, EmailContent content)
    {
        await SendMailAsync(new MailAddress(receiverMailAddress), content);
    }
}