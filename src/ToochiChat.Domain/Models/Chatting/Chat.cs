using CSharpFunctionalExtensions;
using ToochiChat.Domain.Constants;
using ToochiChat.Domain.Extensions;
using ToochiChat.Domain.Models.Auth;

namespace ToochiChat.Domain.Models.Chatting;

public sealed class Chat
{
    private readonly List<Message> _messages = new();
    private readonly List<User> _members = new();
    
    public Guid Id { get; }
    public User Owner { get; }
    public string Title { get; private set; }
    public DateTime CreationDate { get; }
    
    public IReadOnlyCollection<Message> Messages => _messages;
    public IReadOnlyCollection<User> Members => _members;

    private Chat(Guid id, string title, User owner, DateTime creationDate)
    {
        Id = id;
        Title = title;
        CreationDate = creationDate;
        Owner = owner;
    }

    public static Result<Chat> Create(Guid id, string title, User owner, DateTime creationDate)
    {
        if (title.Length is > ChatConstants.MaxTitleLength or <= default(int)) 
            return Result.Failure<Chat>($"{nameof(title)} should be lt {ChatConstants.MaxTitleLength}");
        
        if (creationDate.CalculateAge() < 0)
            return Result.Failure<Chat>($"invalid {nameof(creationDate)}");
        
        return Result.Success(new Chat(id, title, owner, creationDate));
    }

    public Result AddNewMessage(Message message)
    {
        _messages.Add(message);
        return Result.Success();
    }

    public Result AddMessages(ICollection<Message> messages)
    {
        foreach (var message in messages) AddNewMessage(message);
        return Result.Success();
    }
    
    public Result AddNewMember(User member)
    {
        _members.Add(member);
        return Result.Success();
    }

    public Result AddMembers(ICollection<User> members)
    {
        foreach (var member in members) AddNewMember(member);
        return Result.Success();
    }

    public Result UpdateTitle(string newTitle)
    {
        if (newTitle.Length is > ChatConstants.MaxTitleLength or <= default(int)) 
            return Result.Failure($"{nameof(newTitle)} should be lt {ChatConstants.MaxTitleLength}");
        
        Title = newTitle;
        return Result.Success();
    }
}