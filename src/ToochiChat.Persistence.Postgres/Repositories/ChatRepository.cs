using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Chatting;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Postgres.Connection;
using ToochiChat.Persistence.Postgres.Entities;

namespace ToochiChat.Persistence.Postgres.Repositories;

internal sealed class ChatRepository : IChatRepository
{
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly IMapper<Chat, ChatEntity> _chatMapper;
    private readonly IMapper<Message, MessageEntity> _messageMapper;
    
    private readonly ICollectionMapper<Chat, ChatEntity> _chatCollectionMapper;
    private readonly ICollectionMapper<Message, MessageEntity> _messageCollectionMapper;
    private readonly ICollectionMapper<User, UserEntity> _userCollectionMapper;

    public ChatRepository(IMapper<Chat, ChatEntity> chatMapper, ICollectionMapper<Chat, ChatEntity> chatCollectionMapper, 
        IMapper<Message, MessageEntity> messageMapper, ICollectionMapper<Message, MessageEntity> messageCollectionMapper, 
        ICollectionMapper<User, UserEntity> userCollectionMapper, IServiceScopeFactory scopeFactory)
    {
        _chatMapper = chatMapper;
        _chatCollectionMapper = chatCollectionMapper;
        _messageMapper = messageMapper;
        _messageCollectionMapper = messageCollectionMapper;
        _userCollectionMapper = userCollectionMapper;
        _scopeFactory = scopeFactory;
    }

    public async Task<Result<Chat>> GetChatById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var chatEntity = await context.Chats
            .AsNoTracking()
            .Include(chat => chat.Messages)
            .FirstOrDefaultAsync(chat => chat.Id == id);

        if (chatEntity is null) return Result.Failure<Chat>($"chat with id {id} doesn't exist");
        return Result.Success(_chatMapper.MapFrom(chatEntity));
    }

    public async Task<Result<Chat>> GetChatWithMembersById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var chatEntity = await context.Chats
            .AsNoTracking()
            .Include(chat => chat.Messages)
            .Include(chat => chat.Members)
            .FirstOrDefaultAsync(chat => chat.Id == id);

        if (chatEntity is null) return Result.Failure<Chat>($"chat with id {id} doesn't exist");
        return Result.Success(_chatMapper.MapFrom(chatEntity));
    }

    public async Task<Result<Guid>> Create(Chat chat)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var chatEntity = _chatMapper.MapFrom(chat);

        await context.Chats.AddAsync(chatEntity);
        await context.SaveChangesAsync();
        
        return Result.Success(chat.Id)!;
    }

    public async Task<Result> UpdateChatById(Guid id, Chat updatedChat)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var updatedMessagesEntity = 
            _messageCollectionMapper.MapFrom(updatedChat.Messages.ToList());

        var updatedMembersEntity = 
            _userCollectionMapper.MapFrom(updatedChat.Members.ToList());
        
        await context.Chats
            .Where(chat => chat.Id == id)
            .ExecuteUpdateAsync(chat => chat
                .SetProperty(c => c.Title, updatedChat.Title)
                .SetProperty(c => c.Members, updatedMembersEntity)
                .SetProperty(c => c.Messages, updatedMessagesEntity)
            );

        return Result.Success();
    }
    
    // TODO: finish it
    public async Task<Result> AddMessageToChat(Message message)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await context.Messages.AddAsync(_messageMapper.MapFrom(message));
        return Result.Success();
    }

    public async Task<Result> DeleteChatById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await context.Chats
            .Where(chat => chat.Id == id)
            .ExecuteDeleteAsync();

        return Result.Success();
    }

    public async Task<Result<List<Chat>>> GetAll()
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var chatEntities = await context.Chats
            .AsNoTracking()
            .Include(chat => chat.Messages)
            .ToListAsync();

        var chats = _chatCollectionMapper.MapFrom(chatEntities).ToList();
        return Result.Success(chats);
    }
}