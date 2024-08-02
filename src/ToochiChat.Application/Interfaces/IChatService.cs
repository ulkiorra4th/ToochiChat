using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.Application.Interfaces;

public interface IChatService
{
   Task<Result<Chat>> GetChatById(Guid id);
   Task<Result> SendMessageFrom(string accountId, string chatId, Message message);
   Task<Result<IReadOnlyCollection<User>>> GetChatMembers(Guid chatId);
   Task<Result> AddChatMember(string accountId, string chatId);
}