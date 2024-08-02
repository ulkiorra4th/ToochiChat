using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using ToochiChat.Application.Auth.Interfaces;
using ToochiChat.Application.Auth.Interfaces.Infrastructure;
using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Application.Auth.Services;

public sealed class AccountService : IAccountService
{
    private readonly IPasswordSecurityService _passwordSecurityService;
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailConfirmationService _emailConfirmationService;
    private readonly IJwtProvider _jwtProvider;
    
    public AccountService(IAccountRepository accountRepository, IEmailConfirmationService emailConfirmationService, 
        IJwtProvider jwtProvider, IPasswordSecurityService passwordSecurityService)
    {
        _accountRepository = accountRepository;
        _emailConfirmationService = emailConfirmationService;
        _jwtProvider = jwtProvider;
        _passwordSecurityService = passwordSecurityService;
    }
    
    public async Task<Result<string>> Register(string email, string password, DateTime birthDate)
    {
        if (await Exists(email)) return Result.Failure<string>($"user with email {email} already exists");
        
        var userInfoResult = User.CreateDefault(birthDate);
        if (userInfoResult.IsFailure) return Result.Failure<string>("invalid userInfo");
        
        if (!Regex.Match(password, AuthConstants.PasswordRegex).Success) 
            return Result.Failure<string>($"{nameof(password)} is bad");
        
        if (!Regex.Match(email, AuthConstants.EmailRegex).Success) 
            return Result.Failure<string>($"{nameof(email)} is incorrect");

        var salt = _passwordSecurityService.GenerateSalt();
        var hashedPassword = _passwordSecurityService.HashPassword(password, salt);
        var confirmationToken = _emailConfirmationService.GenerateConfirmationToken();
        var accountResult = Account.Create(Guid.NewGuid(), email, hashedPassword, salt, 
            confirmationToken, userInfoResult.Value); 
        
        if (accountResult.IsFailure) return Result.Failure<string>("invalid data");

        var accountCreationResult = await _accountRepository.Create(accountResult.Value);
        if (accountCreationResult.IsFailure) return Result.Failure<string>("Account wasn't saved in DB");
        
        _emailConfirmationService.SendConfirmationEmail(email, confirmationToken);
        return await LogIn(email, password);
    }

    public async Task<Result<string>> LogIn(string email, string password)
    {
        var accountResult = await _accountRepository.GetAccountByEmail(email);
        if (accountResult.IsFailure) return Result.Failure<string>("email or password is incorrect");

        var account = accountResult.Value;
        
        if (!_passwordSecurityService.Verify(password, account.Salt, account.Password))
            return Result.Failure<string>("email or password is incorrect");
        
        var token = _jwtProvider.GenerateToken(account);
        return Result.Success(token);
    }

    private async Task<bool> Exists(string email)
    {
        var accountResult = await _accountRepository.GetAccountByEmail(email);
        return accountResult.IsSuccess;
    }
}