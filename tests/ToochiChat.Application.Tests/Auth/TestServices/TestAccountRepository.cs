using CSharpFunctionalExtensions;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Application.Tests.Auth.TestServices;

#nullable disable

internal sealed class TestAccountRepository : IAccountRepository
{
    public async Task<Result<Account>> GetAccountById(Guid id)
    {
        await Task.Delay(100);
        
        var account = TestData.Accounts
            .FirstOrDefault(account => account.Id == id);

        return account is null
            ? Result.Failure<Account>($"account with id {id} doesn't exist")
            : Result.Success(account);
    }

    public async Task<Result<Account>> GetAccountWithUserById(Guid id)
    {
        await Task.Delay(100);
        
        var account = TestData.Accounts
            .FirstOrDefault(account => account.Id == id);

        return account is null
            ? Result.Failure<Account>($"account with id {id} doesn't exist")
            : Result.Success(account);
    }

    public async Task<Result<Account>> GetAccountByEmail(string email)
    {
        await Task.Delay(100);
        
        var account = TestData.Accounts
            .FirstOrDefault(account => account.Email == email);

        return account is null
            ? Result.Failure<Account>($"account with email {email} doesn't exist")
            : Result.Success(account);
    }

    public async Task<Result<Account>> GetAccountWithUserByEmail(string email)
    {
        await Task.Delay(100);
        
        var account = TestData.Accounts
            .FirstOrDefault(account => account.Email == email);

        return account is null
            ? Result.Failure<Account>($"account with email {email} doesn't exist")
            : Result.Success(account);
    }

    public async Task<Result<Guid>> Create(Account account)
    {
        await Task.Delay(100);

        TestData.Accounts.Add(account);
        return Result.Success(account.Id)!;
    }

    public async Task<Result> UpdateAccountById(Guid id, Account updatedAccount)
    {
        await Task.Delay(100);

        var accountIndex = TestData.Accounts
            .IndexOf(TestData.Accounts.FirstOrDefault(account => account.Id == id));
        
        if (accountIndex >= TestData.Accounts.Count)
            return Result.Failure("wrong index");
        
        TestData.Accounts[accountIndex] = updatedAccount;
        return Result.Success();
    }

    public async Task<Result> DeleteAccountById(Guid id)
    {
        await Task.Delay(100);
        
        TestData.Accounts.Remove(TestData.Accounts.FirstOrDefault(account => account.Id == id)!);
        return Result.Success();
    }

    public async Task<Result<List<Account>>> GetAll()
    {
        await Task.Delay(100);

        var accounts = TestData.Accounts;
        return Result.Success(accounts);
    }
}