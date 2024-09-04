using ToochiChat.Domain.Models;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Mongo.Entities;

namespace ToochiChat.Persistence.Mongo.Mappers;

internal sealed class UserMapper : IMapper<User, UserEntity>
{
    public User MapFrom(UserEntity other)
    {
        throw new NotImplementedException();
    }

    public UserEntity MapFrom(User other)
    {
        throw new NotImplementedException();
    }
}