using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Application.Auth.Interfaces;
using ToochiChat.Application.Auth.Services;
using ToochiChat.Application.Interfaces;
using ToochiChat.Application.Services;

namespace ToochiChat.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<IAccountService, AccountService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<IChatService, ChatService>();
        services.AddSingleton<IMessageService, MessageService>();
        services.AddSingleton<IFileService, FileService>();

        return services;
    }
}