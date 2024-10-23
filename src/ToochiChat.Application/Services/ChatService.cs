using CSharpFunctionalExtensions;
using ToochiChat.Application.Interfaces;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.Application.Services;

internal sealed class ChatService : IChatService
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

    public async Task<Result<List<Chat>>> GetRange(int offset, int count)
    {
        return await _chatRepository.GetRange(offset, count);
    }

    public async Task<Result<Guid>> Create(Chat chat)
    {
        return await _chatRepository.Create(chat);
    }

    public async Task<Result> UpdateChatById(Guid id, Chat updatedChat)
    {
        return await _chatRepository.UpdateChatById(id, updatedChat);
    }

    public async Task<Result> DeleteChatById(Guid id)
    {
        return await _chatRepository.DeleteChatById(id);
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