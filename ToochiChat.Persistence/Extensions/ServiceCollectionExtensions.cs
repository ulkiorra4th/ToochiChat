using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Domain.Models;
using ToochiChat.Domain.Models.Auth;
using ToochiChat.Domain.Models.Chatting;
using ToochiChat.Infrastructure.Abstractions.Mapper;
using ToochiChat.Persistence.Connection;
using ToochiChat.Persistence.Entities;
using ToochiChat.Persistence.Mappers;
using ToochiChat.Persistence.Repositories;

namespace ToochiChat.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContextAndRepositories(this IServiceCollection services, 
        string connectionString)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

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