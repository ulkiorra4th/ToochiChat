using ToochiChat.Domain.Models;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Entities;

namespace ToochiChat.Persistence.Mappers;

public class UserMapper : IMapper<User, UserEntity>, ICollectionMapper<User, UserEntity>
{
    public User MapFrom(UserEntity other)
    {
        throw new NotImplementedException();
    }

    public UserEntity MapFrom(User other)
    {
        throw new NotImplementedException();
    }

    public ICollection<User> MapFrom(ICollection<UserEntity> other)
    {
        return other.Select(MapFrom).ToList();
    }

    public ICollection<UserEntity> MapFrom(ICollection<User> other)
    {
        return other.Select(MapFrom).ToList();
    }
}