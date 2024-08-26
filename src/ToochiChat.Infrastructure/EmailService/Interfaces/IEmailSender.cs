using System.Net.Mail;
using ToochiChat.Infrastructure.EmailService.Implementations.Content;

namespace ToochiChat.Infrastructure.EmailService.Interfaces;

public interface IEmailSender
{ 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="senderAddress"></param>
    /// <param name="displayName"></param>
    /// <param name="senderMailPassword"></param>
    /// <param name="host"></param>
    /// <param name="port"></param>
    public void ConfigureSender(string senderAddress, string displayName, string senderMailPassword, string host,
        int port);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="senderAddress"></param>
    /// <param name="displayName"></param>
    /// <param name="senderMailPassword"></param>
    public void SetSenderAddress(string senderAddress, string displayName, string senderMailPassword);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="senderMailPassword"></param>
    public void SetSenderAddress(MailAddress sender, string senderMailPassword);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    public void ConfigureSmtp(string host, int port);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiver"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public Task SendMailAsync(MailAddress receiver, EmailContent content);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="receiverMailAddress"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    public Task SendMailAsync(string receiverMailAddress, EmailContent content);
}