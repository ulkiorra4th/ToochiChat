using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Persistence.FileSystem.Options;
using ToochiChat.Persistence.FileSystem.Repositories;

namespace ToochiChat.Persistence.FileSystem.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFilesRepository(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<FileRepositoryOptions>(configuration
            .GetSection(nameof(FileRepositoryOptions)) ?? throw new Exception("Invalid config file"));

        return services.AddSingleton<IFileRepository, FileRepository>();
    }
}