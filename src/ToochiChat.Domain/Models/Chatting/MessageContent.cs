using CSharpFunctionalExtensions;
using ToochiChat.Domain.Constants;

namespace ToochiChat.Domain.Models.Chatting;

public sealed class MessageContent
{
    private readonly List<FileInfo>? _files;
    
    public string? Text { get; }
    public IReadOnlyList<FileInfo>? Files => _files;
    
    private MessageContent(string? text, List<FileInfo>? files = null)
    {
        Text = text;
        _files = files;
    }

    public static Result<MessageContent> Create(string? text, List<FileInfo>? files = null)
    {
        if (String.IsNullOrEmpty(text) && (files is null || files.Count == 0))
            return Result.Failure<MessageContent>("Message is null");
        
        if (text?.Length > ChatConstants.MaxMessageTextLength)
            return Result.Failure<MessageContent>($"Message length should be lt {ChatConstants.MaxMessageTextLength}");
        
        if (files?.Count > ChatConstants.MaxFilesInMessageCount)
            return Result.Failure<MessageContent>("Too many files in message");
        
        return Result.Success(new MessageContent(text, files));
    }
}