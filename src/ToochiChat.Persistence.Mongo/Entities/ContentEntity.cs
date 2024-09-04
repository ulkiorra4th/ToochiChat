namespace ToochiChat.Persistence.Mongo.Entities;

internal sealed class ContentEntity
{
    public string? Text { get; init; }
    public List<string>? Files { get; init; }
}