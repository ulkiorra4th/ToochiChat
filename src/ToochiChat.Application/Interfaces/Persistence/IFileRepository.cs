using CSharpFunctionalExtensions;

namespace ToochiChat.Application.Interfaces.Persistence;

public interface IFileRepository
{
    Task SaveFile(string fileName, byte[] data);
    Result<string> GetFile(string fileName);
    Task<Result> DeleteFile(string fileName);
}