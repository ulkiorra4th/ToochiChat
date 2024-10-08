using CSharpFunctionalExtensions;
using ToochiChat.Application.Interfaces;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.Application.Services;

public sealed class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;

    public ChatService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public async Task<Result<Chat>> GetChatById(Guid id)
    {
        return await _chatRepository.GetChatById(id);
    }

    public async Task<Result> SendMessageFrom(string accountId, string chatId, Message message)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IReadOnlyCollection<User>>> GetChatMembers(Guid chatId)
    {
        var chatResult = await _chatRepository.GetChatWithMembersById(chatId);
        
        return chatResult.IsFailure ? 
            Result.Failure<IReadOnlyCollection<User>>(chatResult.Error) 
            : Result.Success(chatResult.Value.Members);
    }

    public async Task<Result> AddChatMember(string accountId, string chatId)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveChatMember(string userName, string chatId)
    {
        throw new NotImplementedException();
    }
}