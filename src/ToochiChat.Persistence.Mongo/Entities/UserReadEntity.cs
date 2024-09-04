namespace ToochiChat.Persistence.Mongo.Entities;

internal class UserReadEntity
{
    public UserEntity? User { get; init; }
    public DateTime ReadDate { get; init; }
}