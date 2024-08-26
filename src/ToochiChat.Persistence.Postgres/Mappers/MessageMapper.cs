using ToochiChat.Domain.Models.Chatting;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Postgres.Entities;

namespace ToochiChat.Persistence.Postgres.Mappers;

public class MessageMapper : IMapper<Message, MessageEntity>, ICollectionMapper<Message, MessageEntity>
{
    public Message MapFrom(MessageEntity other)
    {
        throw new NotImplementedException();
    }

    public MessageEntity MapFrom(Message other)
    {
        throw new NotImplementedException();
    }

    public ICollection<Message> MapFrom(ICollection<MessageEntity> other)
    {
        return other.Select(MapFrom).ToList();
    }

    public ICollection<MessageEntity> MapFrom(ICollection<Message> other)
    {
        return other.Select(MapFrom).ToList();
    }
}