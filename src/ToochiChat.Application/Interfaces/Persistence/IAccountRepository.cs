using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Application.Interfaces.Persistence;

public interface IAccountRepository
{
    Task<Result<Account>> GetAccountById(Guid id);
    Task<Result<Account>> GetAccountWithUserById(Guid id);
    Task<Result<Account>> GetAccountByEmail(string email);
    Task<Result<Account>> GetAccountWithUserByEmail(string email);
    Task<Result<Guid>> Create(Account account);
    Task<Result> UpdateAccountById(Guid id, Account updatedAccount);
    Task<Result> DeleteAccountById(Guid id);
    Task<Result<List<Account>>> GetAll();
}