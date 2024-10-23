namespace ToochiChat.Application.Interfaces.Infrastructure;

public interface IEmailVerificationService
{
    /// <summary>
    /// Puts email send request in the queue
    /// After that a background service will take the request from the queue and send the email
    /// </summary>
    /// <param name="email">email to send verification code</param>
    /// <param name="verificationCode">verification code that will be sent</param>
    void PutEmailInQueue(string email, string verificationCode);
    
    /// <summary>
    /// Tries to send the email from the queue
    /// </summary>
    Task TrySendEmailFromQueue();
    
    /// <summary>
    /// Checks if the verification code equals passed emails cached verification code
    /// </summary>
    /// <param name="email">email</param>
    /// <param name="code">verification code</param>
    /// <returns>Is email verified</returns>
    public bool VerifyEmail(string email, string code);
}