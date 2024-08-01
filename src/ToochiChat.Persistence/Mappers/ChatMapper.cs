using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Chatting;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Entities;

namespace ToochiChat.Persistence.Mappers;

public class ChatMapper : IMapper<Chat, ChatEntity>, ICollectionMapper<Chat, ChatEntity>
{
    private readonly ICollectionMapper<User, UserEntity> _userCollectionMapper;
    private readonly ICollectionMapper<Message, MessageEntity> _messageCollectionMapper;

    public ChatMapper(ICollectionMapper<User, UserEntity> userCollectionMapper, 
        ICollectionMapper<Message, MessageEntity> messageCollectionMapper)
    {
        _userCollectionMapper = userCollectionMapper;
        _messageCollectionMapper = messageCollectionMapper;
    }

    public Chat MapFrom(ChatEntity other)
    {
        var chat = Chat.Create(other.Id!, other.Title!, other.CreationDate).Value;

        chat.AddMembers(_userCollectionMapper.MapFrom(other.Members!));
        chat.AddMessages(_messageCollectionMapper.MapFrom(other.Messages!));

        return chat;
    }

    public ChatEntity MapFrom(Chat other)
    {
        return new ChatEntity
        {
            Id = other.Id,
            Title = other.Title,
            CreationDate = other.CreationDate,
            Members = _userCollectionMapper.MapFrom(other.Members.ToList()),
            Messages = _messageCollectionMapper.MapFrom(other.Messages.ToList())
        };
    }

    public ICollection<Chat> MapFrom(ICollection<ChatEntity> other)
    {
        return other.Select(MapFrom).ToList();
    }

    public ICollection<ChatEntity> MapFrom(ICollection<Chat> other)
    {
        return other.Select(MapFrom).ToList();
    }
}