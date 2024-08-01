using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Application.Interfaces.Persistence;

public interface IAccountRepository
{
    Task<Result<Account>> GetAccountById(string id);
    Task<Result<Account>> GetAccountWithUserById(string id);
    Task<Result<Account>> GetAccountByEmail(string email);
    Task<Result<Account>> GetAccountWithUserByEmail(string email);
    Task<Result<string>> Create(Account account);
    Task<Result> UpdateAccountById(string id, Account updatedAccount);
    Task<Result> DeleteAccountById(string id);
    Task<Result<List<Account>>> GetAll();
}