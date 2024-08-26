using CSharpFunctionalExtensions;

namespace ToochiChat.Application.Interfaces;

public interface IFileService
{
    /// <summary>
    /// Saves file
    /// </summary>
    /// <param name="contentType">Type of the file</param>
    /// <param name="data">All file's bytes</param>
    /// <returns>The name of the saved file</returns>
    Task<Result<string>> SaveFile(string contentType, byte[] data);
    
    /// <summary>
    /// Gets the file by name
    /// </summary>
    /// <param name="fileName">Name of the file</param>
    /// <returns>MemoryStream</returns>
    Task<Result<MemoryStream>> GetFile(string fileName);

    /// <summary>
    /// Deletes the file
    /// </summary>
    /// <param name="fileName">Name of the file</param>
    /// <returns>Result</returns>
    Task<Result> DeleteFile(string fileName);

    /// <summary>
    /// Deletes the previous file and saves new file
    /// </summary>
    /// <param name="fileName">Previous file name</param>
    /// <param name="data">All file's bytes</param>
    /// <returns>New file's name</returns>
    Task<Result<string>> UpdateFile(string fileName, byte[] data);
}