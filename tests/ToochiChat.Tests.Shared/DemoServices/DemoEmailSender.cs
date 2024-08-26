using System.Net.Mail;
using ToochiChat.Infrastructure.EmailService.Implementations.Content;
using ToochiChat.Infrastructure.EmailService.Interfaces;

namespace ToochiChat.Tests.Shared.DemoServices;

internal sealed class DemoEmailSender : IEmailSender
{
    public string SenderAddress { get; private set; } = null!;
    public string DisplayName { get; private set; } = null!;
    public string SenderMailPassword { get; private set; } = null!;

    public void SetSenderAddress(string senderAddress, string displayName, string senderMailPassword)
    {
        SenderAddress = senderAddress;
        DisplayName = displayName;
        SenderMailPassword = senderMailPassword;
    }

    public void SetSenderAddress(MailAddress sender, string senderMailPassword)
    {
        SenderAddress = sender.Address;
        DisplayName = sender.DisplayName;
        SenderMailPassword = senderMailPassword;
    }

    public async Task SendMailAsync(MailAddress receiver, EmailContent content)
    {
        await Task.Delay(100);
        
        Console.WriteLine($"New message for: {receiver.Address}");
        Console.WriteLine($"SenderAddress: {SenderAddress}\nDisplayName: {DisplayName}\nSenderMailPassword: {SenderMailPassword}");
        Console.WriteLine($"Content:\n\tSubject: {content.Subject}\n\tBody: {content.Body}\n");
    }

    public async Task SendMailAsync(string receiverMailAddress, EmailContent content)
    {
        await Task.Delay(100);
        
        Console.WriteLine($"New message for: {receiverMailAddress}");
        Console.WriteLine($"SenderAddress: {SenderAddress}\nDisplayName: {DisplayName}\nSenderMailPassword: {SenderMailPassword}");
        Console.WriteLine($"Content:\n\tSubject: {content.Subject}\n\tBody: {content.Body}\n");
    }
    
    public void ConfigureSender(string senderAddress, string displayName, string senderMailPassword, string host, int port)
    {
        return;
    }
    
    public void ConfigureSmtp(string host, int port)
    {
        return;
    }
}