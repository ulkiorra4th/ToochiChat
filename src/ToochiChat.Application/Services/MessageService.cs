using ToochiChat.Application.Interfaces;
using ToochiChat.Application.Interfaces.Persistence;

namespace ToochiChat.Application.Services;

internal sealed class MessageService : IMessageService
{
    private readonly IFileRepository _fileRepository;

    public MessageService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }
}