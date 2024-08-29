using CSharpFunctionalExtensions;
using Microsoft.Extensions.Options;
using ToochiChat.Application.Interfaces.Infrastructure;
using ToochiChat.Application.Interfaces.Persistence;
using ToochiChat.Persistence.FileSystem.Options;

namespace ToochiChat.Persistence.FileSystem.Repositories;

internal sealed class FileRepository : IFileRepository
{
    private const int FirstFolderNameLength = 2;
    private const int SecondFolderNameLength = 4;
    
    private readonly IHashingService _hashingService;
    private readonly FileRepositoryOptions _options;

    public FileRepository(IHashingService hashingService, IOptions<FileRepositoryOptions> options)
    {
        _hashingService = hashingService;
        _options = options.Value;
    }

    public async Task SaveFile(string fileName, byte[] data)
    {
        var dir = GetFileDirectoryPath(fileName);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        var fullPath = $"{dir}/{fileName}";
        
        using (var fs = File.Create(fullPath))
        {
            await fs.WriteAsync(data, 0, data.Length);
        }
    }

    public Result<string> GetFile(string fileName)
    {
        var filePath = $"{GetFileDirectoryPath(fileName)}/{fileName}";
        return !File.Exists(filePath) 
            ? Result.Failure<string>($"File {fileName} doesn't exist") 
            : filePath;
    }

    public async Task<Result> DeleteFile(string fileName)
    {
        var dir = GetFileDirectoryPath(fileName);
        if (!Directory.Exists(dir)) 
            return Result.Failure($"File {fileName} doesn't exist");
        
        var fullPath = $"{dir}/{fileName}";
        
        await Task.Run(() => File.Delete(fullPath)); // TODO: think
        return Result.Success();
    }

    private string GetFileDirectoryPath(string fileName)
    {
        var fileNameHash = _hashingService.GetMD5Hash(fileName);
        
        return $"{_options.BasePath}/{fileNameHash.Substring(0, FirstFolderNameLength)}/" +
               $"{fileNameHash.Substring(0, SecondFolderNameLength)}";
    }
}