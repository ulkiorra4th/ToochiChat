using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.Application.Interfaces.Persistence;

public interface IChatRepository
{
    Task<Result<Chat>> GetChatById(Guid id);
    Task<Result<Chat>> GetChatWithMembersById(Guid id);
    Task<Result<Guid>> Create(Chat chat);
    Task<Result> UpdateChatById(Guid id, Chat updatedChat);
    Task<Result> AddMessageToChat(Message message);
    Task<Result> DeleteChatById(Guid id);
    Task<Result<List<Chat>>> GetAll();
}