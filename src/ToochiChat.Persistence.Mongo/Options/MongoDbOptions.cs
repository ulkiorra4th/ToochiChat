namespace ToochiChat.Persistence.Mongo.Options;

public sealed class MongoDbOptions
{
    public required string ConnectionString { get; init; }
    public required string DatabaseName { get; init; }
    public required string CollectionName { get; init; }
}