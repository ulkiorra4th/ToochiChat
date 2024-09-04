using ToochiChat.Domain.Models.Chatting;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Mongo.Entities;

namespace ToochiChat.Persistence.Mongo.Mappers;

internal sealed class MessageMapper : IMapper<Message, MessageEntity>, ICollectionMapper<Message, MessageEntity>
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
        throw new NotImplementedException();
    }

    public ICollection<MessageEntity> MapFrom(ICollection<Message> other)
    {
        throw new NotImplementedException();
    }
}