using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models;

namespace ToochiChat.Application.Interfaces;

public interface IUserService
{
    Task<Result<User>> Get(string id);

    Task<Result<User>> Create(User user);
}