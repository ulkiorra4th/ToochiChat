using CSharpFunctionalExtensions;

namespace ToochiChat.Domain.Models;

public class FileInfo
{
    private readonly byte[]? _data = null;

    public string Name { get; private set; }
    public string Type { get; }
    public IReadOnlyCollection<byte>? Data => _data;

    private FileInfo(string name, string type)
    {
        Name = name;
        Type = type;
    }
    
    private FileInfo(string name, string type, byte[] data) : this(name, type)
    {
        _data = data;
    }

    public static Result<FileInfo> Create(string? name, string? type, byte[]? data)
    {
        if (String.IsNullOrEmpty(name)) return Result.Failure<FileInfo>("File's name is null");
        if (String.IsNullOrEmpty(type)) return Result.Failure<FileInfo>("Undefined file type");
        if (data is null || data.Length == 0) return Result.Failure<FileInfo>("Invalid file data");

        return  Result.Success(new FileInfo(name, type, data));
    }
    
    public static Result<FileInfo> CreateWithoutData(string? name, string? type)
    {
        if (String.IsNullOrEmpty(name)) return Result.Failure<FileInfo>("File's name is null");
        if (String.IsNullOrEmpty(type)) return Result.Failure<FileInfo>("Undefined file type");

        return  Result.Success(new FileInfo(name, type));
    }

    public Result Rename(string name)
    {
        if (String.IsNullOrEmpty(name))
            return Result.Failure("Empty file name");

        Name = name;
        
        return Result.Success();
    }
}