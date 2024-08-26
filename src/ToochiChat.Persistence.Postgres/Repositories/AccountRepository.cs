using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Auth;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Postgres.Connection;
using ToochiChat.Persistence.Postgres.Entities;

namespace ToochiChat.Persistence.Postgres.Repositories;

internal sealed class AccountRepository : IAccountRepository
{
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly IMapper<User, UserEntity> _userMapper;
    private readonly IMapper<Account, AccountEntity> _accountMapper;
    private readonly ICollectionMapper<Account, AccountEntity> _accountCollectionMapper;

    public AccountRepository(IMapper<User, UserEntity> userMapper, IMapper<Account, AccountEntity> accountMapper, 
        ICollectionMapper<Account, AccountEntity> accountCollectionMapper, IServiceScopeFactory scopeFactory)
    {
        _userMapper = userMapper;
        _accountMapper = accountMapper;
        _accountCollectionMapper = accountCollectionMapper;
        _scopeFactory = scopeFactory;
    }

    public async Task<Result<Account>> GetAccountById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var accountEntity = await context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(account => account.Id == id);

        if (accountEntity is null) return Result.Failure<Account>($"account with id {id} doesn't exist");
        return Result.Success(_accountMapper.MapFrom(accountEntity));
    }

    public async Task<Result<Account>> GetAccountWithUserById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var accountEntity = await context.Accounts
            .AsNoTracking()
            .Include(account => account.UserInfo)
            .FirstOrDefaultAsync(account => account.Id == id);

        if (accountEntity is null) return Result.Failure<Account>($"account with id {id} doesn't exist");
        return Result.Success(_accountMapper.MapFrom(accountEntity));
    }

    public async Task<Result<Account>> GetAccountByEmail(string email)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var accountEntity = await context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(account => account.Email == email);

        if (accountEntity is null) return Result.Failure<Account>($"account with email {email} doesn't exist");
        return Result.Success(_accountMapper.MapFrom(accountEntity));
    }

    public async Task<Result<Account>> GetAccountWithUserByEmail(string email)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var accountEntity = await context.Accounts
            .AsNoTracking()
            .Include(account => account.UserInfo)
            .FirstOrDefaultAsync(account => account.Email == email);

        if (accountEntity is null) return Result.Failure<Account>($"account with email {email} doesn't exist");
        return Result.Success(_accountMapper.MapFrom(accountEntity));
    }

    public async Task<Result<Guid>> Create(Account account)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var accountEntity = _accountMapper.MapFrom(account);

        await context.Accounts.AddAsync(accountEntity);
        await context.SaveChangesAsync();
        
        return Result.Success(accountEntity.Id)!;
    }

    public async Task<Result> UpdateAccountById(Guid id, Account updatedAccount)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var updatedUserInfoEntity = _userMapper.MapFrom(updatedAccount.UserInfo);
        
        await context.Accounts
            .Where(account => account.Id == id)
            .ExecuteUpdateAsync(account => account
                .SetProperty(a => a.Email, updatedAccount.Email)
                .SetProperty(a => a.UserInfo, updatedUserInfoEntity)
                .SetProperty(a => a.Salt, updatedAccount.Salt)
                .SetProperty(a => a.PasswordHash, updatedAccount.Password)
                .SetProperty(a => a.IsEmailConfirmed, updatedAccount.IsEmailConfirmed)
            );

        return Result.Success();
    }
    
    public async Task<Result> UpdateAccountByEmail(string email, Account updatedAccount)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var updatedUserInfoEntity = _userMapper.MapFrom(updatedAccount.UserInfo);
        
        await context.Accounts
            .Where(account => account.Email == email)
            .ExecuteUpdateAsync(account => account
                .SetProperty(a => a.Email, updatedAccount.Email)
                .SetProperty(a => a.UserInfo, updatedUserInfoEntity)
                .SetProperty(a => a.Salt, updatedAccount.Salt)
                .SetProperty(a => a.PasswordHash, updatedAccount.Password)
                .SetProperty(a => a.IsEmailConfirmed, updatedAccount.IsEmailConfirmed)
            );

        return Result.Success();
    }
    
    public async Task<Result> UpdateAccountVerificationStatePropertyByEmail(string email, bool confirmationState)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        await context.Accounts
            .Where(account => account.Email == email)
            .ExecuteUpdateAsync(account => 
                account.SetProperty(a => a.IsEmailConfirmed, confirmationState)
            );
        
        return Result.Success();
    }

    public async Task<Result> DeleteAccountById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await context.Accounts
            .Where(account => account.Id == id)
            .ExecuteDeleteAsync();

        return Result.Success();
    }

    public async Task<Result<List<Account>>> GetAll()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var accountEntities = await context.Accounts
            .AsNoTracking()
            .ToListAsync();

        var accounts = _accountCollectionMapper.MapFrom(accountEntities).ToList();
        return Result.Success(accounts);
    }
}