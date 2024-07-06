using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.Application.Interfaces.Persistence;

public interface IChatRepository
{
    Task<Result<Chat>> GetChatById(string id);
    Task<Result<Chat>> GetChatWithMembersById(string id);
    Task<Result<string>> Create(Chat chat);
    Task<Result> UpdateChatById(string id, Chat updatedChat);
    Task<Result> AddMessageToChat(Message message);
    Task<Result> DeleteChatById(string id);
    Task<Result<List<Chat>>> GetAll();
}