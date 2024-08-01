using CSharpFunctionalExtensions;
using ToochiChat.Domain.Constants;
using ToochiChat.Domain.Extensions;

namespace ToochiChat.Domain.Models;

public sealed class User
{
    public string UserName { get; }
    public DateTime BirthDate { get; }
    
    private User(string userName, DateTime birthDate)
    {
        UserName = userName;
        BirthDate = birthDate;
    }

    public static Result<User> Create(string userName, DateTime birthDate)
    {
        if (userName.Length > UserConstants.MaxNameLength) 
            return Result.Failure<User>($"{nameof(userName)} should be lt {UserConstants.MaxNameLength}");

        if (birthDate.CalculateAge() < UserConstants.MinAge)
            return Result.Failure<User>($"age should be gt {UserConstants.MinAge}");
        
        return Result.Success(new User(userName, birthDate));
    }

    public static Result<User> CreateDefault(DateTime birthDate)
    {
        if (birthDate.CalculateAge() < UserConstants.MinAge)
            return Result.Failure<User>($"age should be gt {UserConstants.MinAge}");

        return Result.Success(new User(UserConstants.DefaultUserName, birthDate));
    }
}