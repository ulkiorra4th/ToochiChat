using ToochiChat.Domain.Models.Chatting;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Mongo.Entities;

namespace ToochiChat.Persistence.Mongo.Mappers;

internal sealed class ReactionMapper : IMapper<Reaction, ReactionEntity>, ICollectionMapper<Reaction, ReactionEntity>
{
    public Reaction MapFrom(ReactionEntity other)
    {
        throw new NotImplementedException();
    }

    public ReactionEntity MapFrom(Reaction other)
    {
        throw new NotImplementedException();
    }

    public ICollection<Reaction> MapFrom(ICollection<ReactionEntity> other)
    {
        throw new NotImplementedException();
    }

    public ICollection<ReactionEntity> MapFrom(ICollection<Reaction> other)
    {
        throw new NotImplementedException();
    }
}