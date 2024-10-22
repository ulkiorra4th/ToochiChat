using CSharpFunctionalExtensions;
using ToochiChat.Application.Interfaces;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models;

namespace ToochiChat.Application.Services;

internal sealed class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<Result<User>> Get(string id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<Result<User>> Create(User user)
    {
        throw new NotImplementedException();
    }
}