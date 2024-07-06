using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using ToochiChat.Domain.Constants;

namespace ToochiChat.Domain.Models.Auth;

public sealed class Account
{
    public string Id { get; }
    public string Email { get; } 
    public string Password { get; }
    public string Salt { get; }
    public bool IsEmailConfirmed { get; private set; }
    public string EmailConfirmationToken { get; }
    public User UserInfo { get; }
    
    public DateTime CreationDate { get; }

    private Account(string id, string email, string password, string salt, 
        string emailConfirmationToken, User userInfo, DateTime creationDate)
    {
        Id = id;
        Email = email;
        Password = password;
        Salt = salt;
        EmailConfirmationToken = emailConfirmationToken;
        UserInfo = userInfo;
        CreationDate = creationDate;
    }

    public static Result<Account> Create(string id, string email, string password, string salt, 
        string emailConfirmationToken, User userInfo)
    {
        if (String.IsNullOrEmpty(id))
            return Result.Failure<Account>($"{nameof(id)} is required");
        
        if (!Regex.Match(email, AuthConstants.EmailRegex).Success) 
            return Result.Failure<Account>($"{nameof(email)} is incorrect");
        
        if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(salt))
            return Result.Failure<Account>($"{nameof(password)} and {nameof(salt)} are required");

        return Result.Success(new Account(id, email, password, salt, emailConfirmationToken, userInfo, DateTime.Now));
    }

    public void ConfirmEmail()
    {
        IsEmailConfirmed = true;
    }
}