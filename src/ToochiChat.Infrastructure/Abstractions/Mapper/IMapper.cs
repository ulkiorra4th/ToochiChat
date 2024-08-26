namespace ToochiChat.Infrastructure.Abstractions.Mapper;

public interface IMapper<TFirst, TSecond>
{
    TFirst MapFrom(TSecond other);
    TSecond MapFrom(TFirst other);
}