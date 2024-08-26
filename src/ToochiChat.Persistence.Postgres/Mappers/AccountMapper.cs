using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Auth;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Postgres.Entities;

namespace ToochiChat.Persistence.Postgres.Mappers;

public class AccountMapper : IMapper<Account, AccountEntity>, ICollectionMapper<Account, AccountEntity>
{
    private readonly IMapper<User, UserEntity> _userMapper;

    public AccountMapper(IMapper<User, UserEntity> userMapper)
    {
        _userMapper = userMapper;
    }
    
    public Account MapFrom(AccountEntity other)
    {
        return Account.Create(other.Id, other.Email!, other.PasswordHash!, other.Salt!,
            _userMapper.MapFrom(other.UserInfo!)).Value!;
    }

    public AccountEntity MapFrom(Account other)
    {
        return new AccountEntity
        {
            Id = other.Id,
            Email = other.Email,
            PasswordHash = other.Password,
            Salt = other.Salt,
            CreationDate = other.CreationDate,
            IsEmailConfirmed = other.IsEmailConfirmed,
            UserInfo = _userMapper.MapFrom(other.UserInfo)
        };
    }

    public ICollection<Account> MapFrom(ICollection<AccountEntity> other)
    {
        return other.Select(MapFrom).ToList();
    }

    public ICollection<AccountEntity> MapFrom(ICollection<Account> other)
    {
        return other.Select(MapFrom).ToList();
    }
}