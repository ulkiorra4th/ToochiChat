using CSharpFunctionalExtensions;
using ToochiChat.Domain.Models.Chatting.Enums;

namespace ToochiChat.Domain.Models.Chatting;

public sealed class Reaction
{
    public User Author { get; }
    public DateTime ReactionDate { get; }
    public ReactionType Type { get; }
    public int ReactionId => (int)Type;

    private Reaction(DateTime reactionDate, User author, ReactionType reactionType)
    {
        ReactionDate = reactionDate;
        Author = author;
        Type = reactionType;
    }

    public static Result<Reaction> Create(ReactionType reactionType, User author, DateTime reactionDate)
    {
        return reactionDate > DateTime.Now 
            ? Result.Failure<Reaction>("invalid data") 
            : Result.Success(new Reaction(reactionDate, author, reactionType));
    }
}