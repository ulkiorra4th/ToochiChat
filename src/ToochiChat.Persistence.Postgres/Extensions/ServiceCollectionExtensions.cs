using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Auth;
using ToochiChat.Domain.Models.Chatting;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Postgres.Connection;
using ToochiChat.Persistence.Postgres.Entities;
using ToochiChat.Persistence.Postgres.Mappers;
using ToochiChat.Persistence.Postgres.Repositories;

namespace ToochiChat.Persistence.Postgres.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContextAndRepositories(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>();

        services.AddSingleton<IAccountRepository, AccountRepository>();
        services.AddSingleton<IChatRepository, ChatRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddPersistenceMappers(this IServiceCollection services)
    {
        services.AddSingleton<IMapper<User, UserEntity>, UserMapper>();
        services.AddSingleton<ICollectionMapper<User, UserEntity>, UserMapper>();
        
        services.AddSingleton<IMapper<Message, MessageEntity>, MessageMapper>();
        services.AddSingleton<ICollectionMapper<Message, MessageEntity>, MessageMapper>();
        
        services.AddSingleton<IMapper<Chat, ChatEntity>, ChatMapper>();
        services.AddSingleton<ICollectionMapper<Chat, ChatEntity>, ChatMapper>();
        
        services.AddSingleton<IMapper<Account, AccountEntity>, AccountMapper>();
        services.AddSingleton<ICollectionMapper<Account, AccountEntity>, AccountMapper>();

        return services;
    }
}