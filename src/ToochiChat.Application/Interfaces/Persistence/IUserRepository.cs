using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models;

namespace ToochiChat.Application.Interfaces.Persistence;

public interface IUserRepository
{
    Task<Result<User>> GetUserById(string id);
    Task<Result<User>> GetUserWithAccountById(string id);
    Task<Result> UpdateUserById(string id, User updatedUser);
    Task<Result<List<User>>> GetAll();
}