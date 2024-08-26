using CSharpFunctionalExtensions;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Tests.Shared.DemoServices;

#nullable disable

internal sealed class DemoAccountRepository : IAccountRepository
{
    public async Task<Result<Account>> GetAccountById(Guid id)
    {
        await Task.Delay(100);
        
        var account = DemoData.Accounts
            .FirstOrDefault(account => account.Id == id);

        return account is null
            ? Result.Failure<Account>($"account with id {id} doesn't exist")
            : Result.Success(account);
    }

    public async Task<Result<Account>> GetAccountWithUserById(Guid id)
    {
        await Task.Delay(100);
        
        var account = DemoData.Accounts
            .FirstOrDefault(account => account.Id == id);

        return account is null
            ? Result.Failure<Account>($"account with id {id} doesn't exist")
            : Result.Success(account);
    }

    public async Task<Result<Account>> GetAccountByEmail(string email)
    {
        await Task.Delay(100);
        
        var account = DemoData.Accounts
            .FirstOrDefault(account => account.Email == email);

        return account is null
            ? Result.Failure<Account>($"account with email {email} doesn't exist")
            : Result.Success(account);
    }

    public async Task<Result<Account>> GetAccountWithUserByEmail(string email)
    {
        await Task.Delay(100);
        
        var account = DemoData.Accounts
            .FirstOrDefault(account => account.Email == email);

        return account is null
            ? Result.Failure<Account>($"account with email {email} doesn't exist")
            : Result.Success(account);
    }

    public async Task<Result<Guid>> Create(Account account)
    {
        await Task.Delay(100);

        DemoData.Accounts.Add(account);
        return Result.Success(account.Id)!;
    }

    public async Task<Result> UpdateAccountById(Guid id, Account updatedAccount)
    {
        await Task.Delay(100);

        var accountIndex = DemoData.Accounts
            .IndexOf(DemoData.Accounts.FirstOrDefault(account => account.Id == id));
        
        if (accountIndex >= DemoData.Accounts.Count)
            return Result.Failure("wrong index");
        
        DemoData.Accounts[accountIndex] = updatedAccount;
        return Result.Success();
    }

    public async Task<Result> UpdateAccountByEmail(string email, Account updatedAccount)
    {
        await Task.Delay(100);

        var accountIndex = DemoData.Accounts
            .IndexOf(DemoData.Accounts.FirstOrDefault(account => account.Email == email));
        
        if (accountIndex >= DemoData.Accounts.Count)
            return Result.Failure("wrong index");
        
        DemoData.Accounts[accountIndex] = updatedAccount;
        return Result.Success();
    }

    public async Task<Result> UpdateAccountVerificationStatePropertyByEmail(string email, bool confirmationState)
    {
        await Task.Delay(100);

        var accountIndex = DemoData.Accounts
            .IndexOf(DemoData.Accounts.FirstOrDefault(account => account.Email == email));
        
        if (accountIndex >= DemoData.Accounts.Count)
            return Result.Failure("wrong index");
        
        DemoData.Accounts[accountIndex].ConfirmEmail();
        return Result.Success();
    }

    public async Task<Result> DeleteAccountById(Guid id)
    {
        await Task.Delay(100);
        
        DemoData.Accounts.Remove(DemoData.Accounts.FirstOrDefault(account => account.Id == id)!);
        return Result.Success();
    }

    public async Task<Result<List<Account>>> GetAll()
    {
        await Task.Delay(100);

        var accounts = DemoData.Accounts;
        return Result.Success(accounts);
    }
}