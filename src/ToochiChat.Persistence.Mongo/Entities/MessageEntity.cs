namespace ToochiChat.Persistence.Mongo.Entities;

internal sealed class MessageEntity
{
    public ulong Id { get; init; }
    public Guid ChatId { get; init; }
    public UserEntity? Sender { get; init; }
    public ContentEntity? Content { get; set; }
    public DateTime CreationDate { get; init; }
    public List<ReactionEntity>? Reactions { get; } = new();
    public List<UserReadEntity>? WasReadBy { get; } = new();
    public bool WasChanged { get; init; }
    public short SendingOption { get; init; }
    public short Format { get; init; }
}