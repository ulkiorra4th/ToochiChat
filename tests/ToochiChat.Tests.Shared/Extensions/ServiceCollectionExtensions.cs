using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ToochiChat.Tests.Shared.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IConfiguration AddConfiguration(this IServiceCollection services, string fileName)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)!.FullName)
            .AddJsonFile(fileName)
            .Build();
        
        services.AddSingleton<IConfiguration>(configuration);
        return configuration;
    }
}