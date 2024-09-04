using CSharpFunctionalExtensions;
using ToochiChat.Domain.Constants;

namespace ToochiChat.Domain.Models.Chatting;

public sealed class Message
{
    private readonly List<Reaction> _reactions = new();

    public ulong Id { get; }
    public Guid SenderId { get; }
    public MessageContent Content { get; private set; }
    public IReadOnlyList<Reaction> Reactions => _reactions;
    public DateTime UpdateDate { get; private set; }
    public DateTime CreationDate { get; }

    private Message(ulong id, Guid senderId, MessageContent content, DateTime creationDate, DateTime updateDate)
    {
        Id = id;
        SenderId = senderId;
        Content = content;
        UpdateDate = updateDate;
        CreationDate = creationDate;
    }

    public static Result<Message> Create(ulong id, Guid senderId, MessageContent content,
        DateTime creationDate, DateTime updateDate, List<Reaction>? reactions = null)
    {
        if (id <= default(ulong)) return Result.Failure<Message>($"{nameof(id)} should be gt {default(ulong)}");

        var message = new Message(id, senderId, content, creationDate, updateDate);
        
        if (reactions != null)
            message.AddReactions(reactions);
        
        return Result.Success(message);
    }
    
    public static Result<Message> CreateNew(Guid senderId, MessageContent content,
        DateTime creationDate)
    {
        return Result.Success(new Message(0, senderId, content, creationDate, creationDate));
    }
    
    public Result UpdateContent(string? newText, List<FileInfo>? files = null)
    {
        var contentResult = MessageContent.Create(newText, files);
        if (contentResult.IsFailure)
            return Result.Failure<Message>(contentResult.Error);
        
        Content = contentResult.Value;
        UpdateDate = DateTime.Now;

        return Result.Success();
    }

    public Result AddReaction(Reaction reaction)
    {
        _reactions.Add(reaction);

        return Result.Success();
    }
    
    public Result AddReactions(List<Reaction> reactions)
    {
        _reactions.AddRange(reactions);

        return Result.Success();
    }
}