using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Persistence.Mongo.Entities;
using ToochiChat.Persistence.Mongo.Options;
using ToochiChat.Persistence.Mongo.Repositories;

namespace ToochiChat.Persistence.Mongo.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));
        
        return services.AddSingleton<IMessageRepository>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoDbOptions>>().Value;
            
            var mongoClient = new MongoClient(options.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(options.DatabaseName);
            var messagesCollection = mongoDatabase.GetCollection<MessageEntity>(options.CollectionName);

            return new MessageRepository(messagesCollection);
        });
    }
}