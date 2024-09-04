using CSharpFunctionalExtensions;
using MongoDB.Driver;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models.Chatting;
using ToochiChat.Persistence.Mongo.Entities;

namespace ToochiChat.Persistence.Mongo.Repositories;

internal sealed class MessageRepository : IMessageRepository
{
    private readonly IMongoCollection<MessageEntity> _messagesCollection;

    public MessageRepository(IMongoCollection<MessageEntity> messagesCollection)
    {
        _messagesCollection = messagesCollection;
    }

    public async Task<Result> Create(Message message)
    {
        // TODO: map from message
        var messageEntity = new MessageEntity();

        await _messagesCollection.InsertOneAsync(messageEntity);
        
        return Result.Success("Message was successfully created");
    }

    public async Task<Result<Message>> Get(Guid chatId, ulong messageId)
    {
        var message = 
            await _messagesCollection
            .Find(message => message.ChatId == chatId && message.Id == messageId)
            .FirstOrDefaultAsync();

        // TODO: map entity to domain

        throw new NotImplementedException();
    }

    public async Task<Result<List<Message>>> GetRange(Guid chatId, ulong offset, ulong count)
    {
        try
        {
            var messages =
                await _messagesCollection
                    .Find(message => message.ChatId == chatId)
                    .SortBy(m => m.Id)
                    .Skip((int)offset)
                    .Limit((int)count)
                    .ToListAsync();
        }
        catch (InvalidCastException e)
        {
            return Result.Failure<List<Message>>("invalid range");
        }
        
        // TODO: map entity to domain
        
        throw new NotImplementedException();
    }

    public async Task<Result<List<Message>>> GetPage(Guid chatId, int pageNumber, int messagesPerPage)
    {
        var messages = 
            await _messagesCollection
                .Find(message => message.ChatId == chatId)
                .SortBy(m => m.Id)
                .Skip(pageNumber == 0 ? 0 : (pageNumber - 1) * messagesPerPage)
                .Limit(messagesPerPage)
                .ToListAsync();
        
        // TODO: map entity to domain

        throw new NotImplementedException();
    }

    public async Task<Result<Message>> Update(Message message)
    {
        // TODO: map from message
        var messageEntity = new MessageEntity();

        var replaceResult = await _messagesCollection.ReplaceOneAsync(msg =>
            msg.ChatId == messageEntity.ChatId && msg.Id == messageEntity.Id, messageEntity);
        
        return !replaceResult.IsAcknowledged 
            ? Result.Failure<Message>("Cannot update the message") 
            : Result.Success(message);
    }

    public async Task<Result> Delete(Guid chatId, ulong messageId)
    {
        var deleteResult = 
            await _messagesCollection
                .DeleteOneAsync(message => message.ChatId == chatId && message.Id == messageId);
        
        return !deleteResult.IsAcknowledged 
            ? Result.Failure("Cannot delete the message") 
            : Result.Success();
    }

    public async Task<Result> Delete(Guid chatId, ulong[] messageIds)
    {
        var filter = Builders<MessageEntity>.Filter.And(
            Builders<MessageEntity>.Filter.Eq(message => message.ChatId, chatId),
            Builders<MessageEntity>.Filter.In(message => message.Id, messageIds));

        var deleteResult = await _messagesCollection.DeleteManyAsync(filter);
        
        return !deleteResult.IsAcknowledged 
            ? Result.Failure("Cannot delete the message") 
            : Result.Success($"{deleteResult.DeletedCount} messages was deleted");
    }

    public async Task<Result> DeleteAll(Guid chatId)
    {
        var filter = Builders<MessageEntity>.Filter.Eq(message => message.ChatId, chatId);
        
        var deleteResult = await _messagesCollection.DeleteManyAsync(filter);
        
        return !deleteResult.IsAcknowledged 
            ? Result.Failure("Cannot delete the message") 
            : Result.Success($"{deleteResult.DeletedCount} messages was deleted");
    }

    public async Task<Result> ReactToMessage(Guid chatId, ulong messageId, Reaction reaction)
    {
        // TODO: map reaction to reaction entity to pass it as a value 
        var update = Builders<MessageEntity>.Update.Push(message => message.Reactions, new ReactionEntity());
        
        var updateResult = 
            await _messagesCollection
                .UpdateOneAsync(message => message.ChatId == chatId && message.Id == messageId, update);

        return !updateResult.IsAcknowledged 
            ? Result.Failure("Cannot react to the message") 
            : Result.Success();
    }

    public async Task<Result> RemoveReactionFromMessage(Guid chatId, ulong messageId, Reaction reaction)
    {
        // TODO: map reaction to reaction entity to pass it as a value 
        var update = Builders<MessageEntity>.Update.Pull(message => message.Reactions, new ReactionEntity());

        var updateResult =
            await _messagesCollection.UpdateOneAsync(message => message.ChatId == chatId && message.Id == messageId,
                update);
        
        return !updateResult.IsAcknowledged 
            ? Result.Failure("Cannot remove reaction from the message") 
            : Result.Success();
    }
}