using CSharpFunctionalExtensions;
using ToochiChat.Application.Interfaces;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models.Chatting;

namespace ToochiChat.Application.Services;

internal sealed class MessageService : IMessageService
{
    private readonly IFileService _fileService;
    private readonly IMessageRepository _messageRepository;

    public MessageService(IFileService fileService, IMessageRepository messageRepository)
    {
        _fileService = fileService;
        _messageRepository = messageRepository;
    }

    public async Task<Result> Create(Message message)
    {
        if (message.Content.Files is null) return await _messageRepository.Create(message);
        
        foreach (var file in message.Content.Files)
        {
            var saveFileResult = await _fileService.SaveFile(file.Type, file.Data?.ToArray());
            if (saveFileResult.IsFailure) return Result.Failure(saveFileResult.Error);

            file.Rename(saveFileResult.Value);
        }

        return await _messageRepository.Create(message);
    }

    public async Task<Result<Message>> Get(Guid chatId, ulong messageId)
    {
        return await _messageRepository.Get(chatId, messageId);
    }

    public async Task<Result<List<Message>>> GetRange(Guid chatId, ulong offset, ulong count)
    {
        return await _messageRepository.GetRange(chatId, offset, count);
    }

    public async Task<Result<List<Message>>> GetPage(Guid chatId, int pageNumber, int messagesPerPage)
    {
        return await _messageRepository.GetPage(chatId, pageNumber, messagesPerPage);
    }

    public async Task<Result<Message>> Update(Message message)
    {
        return await _messageRepository.Update(message);
    }

    public async Task<Result> Delete(Guid chatId, ulong messageId)
    {
        return await _messageRepository.Delete(chatId, messageId);
    }

    public async Task<Result> Delete(Guid chatId, ulong[] messageIds)
    {
        return await _messageRepository.Delete(chatId, messageIds);
    }

    public async Task<Result> DeleteAll(Guid chatId)
    {
        return await _messageRepository.DeleteAll(chatId);
    }

    public async Task<Result> ReactToMessage(Guid chatId, ulong messageId, Reaction reaction)
    {
        return await _messageRepository.ReactToMessage(chatId, messageId, reaction);
    }

    public async Task<Result> RemoveReactionFromMessage(Guid chatId, ulong messageId, Reaction reaction)
    {
        return await _messageRepository.RemoveReactionFromMessage(chatId, messageId, reaction);
    }
}