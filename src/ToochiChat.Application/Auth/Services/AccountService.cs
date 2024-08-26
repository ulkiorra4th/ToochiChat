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
    private readonly IEmailVerificationService _emailVerificationService;
    private readonly IJwtProvider _jwtProvider;
    
    public AccountService(IAccountRepository accountRepository, IEmailVerificationService emailVerificationService, 
        IJwtProvider jwtProvider, IPasswordSecurityService passwordSecurityService)
    {
        _accountRepository = accountRepository;
        _emailVerificationService = emailVerificationService;
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
        var accountResult = Account.Create(Guid.NewGuid(), email, hashedPassword, salt, userInfoResult.Value); 
        
        if (accountResult.IsFailure) return Result.Failure<string>("invalid data");

        var accountCreationResult = await _accountRepository.Create(accountResult.Value);
        if (accountCreationResult.IsFailure) return Result.Failure<string>("Account wasn't saved in DB");

        SendVerificationCode(email);
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
    
    public string SendVerificationCode(string email)
    {
        var code = _passwordSecurityService.GenerateVerificationCode();
        _emailVerificationService.PutEmailInQueue(email, code);

        return code;
    }

    public async Task<Result<bool>> Verify(string email, string verificationCode)
    {
        if (!_emailVerificationService.VerifyEmail(email, verificationCode))
            return Result.Failure<bool>("Wrong code");
        
        var updateResult = 
            await _accountRepository.UpdateAccountVerificationStatePropertyByEmail(email, true);

        return updateResult.IsSuccess;
    }

    private async Task<bool> Exists(string email)
    {
        var accountResult = await _accountRepository.GetAccountByEmail(email);
        return accountResult.IsSuccess;
    }
}