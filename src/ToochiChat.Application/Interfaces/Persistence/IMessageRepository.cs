using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.Application.Interfaces.Persistence;

public interface IMessageRepository
{
    Task<Result> Create(Message message);
    Task<Result<Message>> Get(Guid chatId, ulong messageId);
    Task<Result<List<Message>>> GetRange(Guid chatId, ulong offset, ulong count);
    Task<Result<List<Message>>> GetPage(Guid chatId, int pageNumber, int messagesPerPage);
    Task<Result<Message>> Update(Message message);
    Task<Result> Delete(Guid chatId, ulong messageId);
    Task<Result> Delete(Guid chatId, ulong[] messageIds);
    Task<Result> DeleteAll(Guid chatId);
    Task<Result> ReactToMessage(Guid chatId, ulong messageId, Reaction reaction);
    Task<Result> RemoveReactionFromMessage(Guid chatId, ulong messageId, Reaction reaction);
}