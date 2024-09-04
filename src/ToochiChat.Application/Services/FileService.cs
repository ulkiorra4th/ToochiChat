using CSharpFunctionalExtensions;
using ToochiChat.Application.Interfaces;
using ToochiChat.Application.Interfaces.Persistence;

namespace ToochiChat.Application.Services;

internal sealed class FileService : IFileService
{
    private readonly IFileRepository _fileRepository;

    public FileService(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<Result<string>> SaveFile(string contentType, byte[]? data)
    {
        if (String.IsNullOrEmpty(contentType))
            return Result.Failure<string>("Empty file content type");

        if (data is null || data.Length == 0)
            return Result.Failure<string>("Empty file data");
        
        var currentDateTime = DateTime.Now;
        var fileName = $"{Guid.NewGuid().ToString().ToLower()} {currentDateTime.Date.ToShortDateString()} - " +
                       $"{currentDateTime.ToUniversalTime().ToShortTimeString().Replace(':', '-')}" +
                       $".{contentType.ToLower()}";

        await _fileRepository.SaveFile(fileName, data);
        return fileName;
    }

    public Result<string> GetFile(string fileName)
    {
        return String.IsNullOrEmpty(fileName)
            ? Result.Failure<string>("Empty file name")
            : _fileRepository.GetFile(fileName);
    }

    public async Task<Result> DeleteFile(string fileName)
    {
        return await _fileRepository.DeleteFile(fileName);
    }

    public async Task<Result<string>> UpdateFile(string fileName, byte[]? data)
    {
        if (String.IsNullOrEmpty(fileName))
            return Result.Failure<string>("Invalid file name");
        
        if (data is null || data.Length == 0)
            return Result.Failure<string>("Empty file data");

        var deleteFileResult = await DeleteFile(fileName);
        if (deleteFileResult.IsFailure)
            return Result.Failure<string>(deleteFileResult.Error);

        await _fileRepository.SaveFile(fileName, data);
        return Result.Success("File updated");
    }
}