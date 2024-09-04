namespace ToochiChat.Persistence.Mongo.Entities;

internal sealed class UserEntity
{
    public Guid Id { get; init; }
    public string? UserName { get; init; }
}