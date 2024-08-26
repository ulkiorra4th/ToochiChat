using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Postgres.Connection;
using ToochiChat.Persistence.Postgres.Entities;

namespace ToochiChat.Persistence.Postgres.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly IMapper<User, UserEntity> _userMapper;
    private readonly ICollectionMapper<User, UserEntity> _userCollectionMapper;

    public UserRepository(IMapper<User, UserEntity> userMapper, ICollectionMapper<User, UserEntity> userCollectionMapper, 
        IServiceScopeFactory scopeFactory)
    {
        _userMapper = userMapper;
        _userCollectionMapper = userCollectionMapper;
        _scopeFactory = scopeFactory;
    }
    
    public async Task<Result<User>> GetUserById(string id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var userEntity = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Id == id);

        if (userEntity is null) return Result.Failure<User>($"user with id {id} doesn't exist");
        return Result.Success(_userMapper.MapFrom(userEntity));
    }

    public async Task<Result<User>> GetUserWithAccountById(string id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var userEntity = await context.Users
            .AsNoTracking()
            .Include(user => user.Account)
            .FirstOrDefaultAsync(user => user.Id == id);

        if (userEntity is null) return Result.Failure<User>($"user with id {id} doesn't exist");
        return Result.Success(_userMapper.MapFrom(userEntity));
    }

    public async Task<Result> UpdateUserById(string id, User updatedUser)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await context.Users
            .Where(user => user.Id == id)
            .ExecuteUpdateAsync(user => user
                .SetProperty(u => u.UserName, updatedUser.UserName)
                .SetProperty(u => u.BirthDate, updatedUser.BirthDate)
            );

        return Result.Success();
    }

    public async Task<Result<List<User>>> GetAll()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var userEntities = await context.Users
            .AsNoTracking()
            .ToListAsync();

        var users = _userCollectionMapper.MapFrom(userEntities).ToList();
        return Result.Success(users);
    }
}