using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using ToochiChat.Domain.Constants;

namespace ToochiChat.Domain.Models.Auth;

public sealed class Account
{
    public Guid Id { get; }
    public string Email { get; } 
    public string Password { get; }
    public string Salt { get; }
    public bool IsEmailConfirmed { get; private set; }
    public User UserInfo { get; }
    public DateTime CreationDate { get; }

    private Account(Guid id, string email, string password, string salt, User userInfo, DateTime creationDate)
    {
        Id = id;
        Email = email;
        Password = password;
        Salt = salt;
        UserInfo = userInfo;
        CreationDate = creationDate;
    }

    public static Result<Account> Create(Guid id, string email, string password, string salt, User userInfo)
    {
        if (id.Equals(Guid.Empty))
            return Result.Failure<Account>($"{nameof(id)} is required");
        
        if (!Regex.Match(email, AuthConstants.EmailRegex).Success) 
            return Result.Failure<Account>($"{nameof(email)} is incorrect");
        
        if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(salt))
            return Result.Failure<Account>($"{nameof(password)} and {nameof(salt)} are required");

        return Result.Success(new Account(id, email, password, salt, userInfo, DateTime.Now));
    }

    public void ConfirmEmail()
    {
        IsEmailConfirmed = true;
    }
}