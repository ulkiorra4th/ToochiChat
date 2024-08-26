using CSharpFunctionalExtensions;
using ToochiChat.Domain.Constants;

namespace ToochiChat.Domain.Models.Chatting;

public sealed class Message
{
    public int Id { get; }
    public Guid SenderId { get; }
    public string Content { get; private set; }
    public DateTime CreationDate { get; }

    private Message(int id, Guid senderId, string content, DateTime creationDate)
    {
        Id = id;
        SenderId = senderId;
        Content = content;
        CreationDate = creationDate;
    }

    public static Result<Message> Create(int id, Guid senderId, string content, DateTime creationDate)
    {
        if (id <= default(int)) return Result.Failure<Message>($"{nameof(id)} should be gt {default(int)}");
        if (content.Length is > ChatConstants.MaxMessageLength or <= default(int)) 
            return Result.Failure<Message>($"{nameof(content)} length should be lt {ChatConstants.MaxMessageLength}");

        return Result.Success(new Message(id, senderId, content, creationDate));
    }
    
    public static Result<Message> CreateNew(Guid senderId, string content, DateTime creationDate)
    {
        if (content.Length is > ChatConstants.MaxMessageLength or <= default(int)) 
            return Result.Failure<Message>($"{nameof(content)} length should be lt {ChatConstants.MaxMessageLength}");

        return Result.Success(new Message(-1, senderId, content, creationDate));
    }
    
    public Result UpdateContent(string newContent)
    {
        if (newContent.Length is > ChatConstants.MaxMessageLength or <= default(int)) 
            return Result.Failure<Message>($"{nameof(newContent)} length should be lt {ChatConstants.MaxMessageLength}");

        Content = newContent;
        return Result.Success();
    }
}