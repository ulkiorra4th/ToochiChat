namespace ToochiChat.Persistence.Mongo.Entities;

internal sealed class ReactionEntity
{
    public int ReactionId { get; init; }
    public UserEntity? Author { get; init; }
    public DateTime ReactionDate { get; init; }
}