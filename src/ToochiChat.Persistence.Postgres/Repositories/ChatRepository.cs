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
    private readonly ICollectionMapper<Chat, ChatEntity> _chatCollectionMapper;
    private readonly ICollectionMapper<User, UserEntity> _userCollectionMapper;

    public ChatRepository(IMapper<Chat, ChatEntity> chatMapper, ICollectionMapper<Chat, ChatEntity> chatCollectionMapper,
        ICollectionMapper<User, UserEntity> userCollectionMapper, IServiceScopeFactory scopeFactory)
    {
        _chatMapper = chatMapper;
        _chatCollectionMapper = chatCollectionMapper;
        _userCollectionMapper = userCollectionMapper;
        _scopeFactory = scopeFactory;
    }

    public async Task<Result<Chat>> GetChatById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var chatEntity = await context.Chats
            .AsNoTracking()
            .FirstOrDefaultAsync(chat => chat.Id == id);

        return chatEntity is null 
            ? Result.Failure<Chat>($"chat with id {id} doesn't exist") 
            : Result.Success(_chatMapper.MapFrom(chatEntity));
    }

    public async Task<Result<Chat>> GetChatWithMembersById(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var chatEntity = await context.Chats
            .AsNoTracking()
            .Include(chat => chat.Members)
            .FirstOrDefaultAsync(chat => chat.Id == id);

        return chatEntity is null 
            ? Result.Failure<Chat>($"chat with id {id} doesn't exist") 
            : Result.Success(_chatMapper.MapFrom(chatEntity));
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

        var updatedMembersEntity = 
            _userCollectionMapper.MapFrom(updatedChat.Members.ToList());
        
        await context.Chats
            .Where(chat => chat.Id == id)
            .ExecuteUpdateAsync(chat => chat
                .SetProperty(c => c.Title, updatedChat.Title)
                .SetProperty(c => c.Members, updatedMembersEntity)
                );

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

    public async Task<Result<List<Chat>>> GetRange(int offset, int count)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var allChatEntities = await context.Chats
            .AsNoTracking()
            .ToListAsync();
        
        var chatEntities = allChatEntities.GetRange(offset, count);
        var chats = _chatCollectionMapper.MapFrom(chatEntities).ToList();
        
        return Result.Success(chats);
    }
}