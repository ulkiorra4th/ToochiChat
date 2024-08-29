using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ToochiChat.Application.Interfaces;
using ToochiChat.Persistence.FileSystem.Options;
using ToochiChat.Tests.Shared.Options;

namespace ToochiChat.Application.Tests.ServicesTests;

public sealed class FileServiceTest
{
    private static readonly TestInputDataOptions TestInputDataOptions;
    
    private readonly IFileService _fileService;
    private readonly FileRepositoryOptions _fileRepositoryOptions;

    static FileServiceTest()
    {
        TestInputDataOptions = Services.Provider.GetRequiredService<IOptions<TestInputDataOptions>>().Value;
    }
    
    public FileServiceTest()
    {
        _fileService = Services.Provider.GetRequiredService<IFileService>();
        _fileRepositoryOptions = Services.Provider.GetRequiredService<IOptions<FileRepositoryOptions>>().Value;
    }

    #region Tests

    [Theory]
    [MemberData(nameof(FilesData))]
    public async Task SaveAndDeleteFileTest(string contentType, byte[] data)
    {
        var fileNameResult = await _fileService.SaveFile(contentType, data);
        Assert.True(fileNameResult.IsSuccess);

        GetFileTest(fileNameResult.Value);

        await DeleteFileTest(fileNameResult.Value);

        // // deleting created files
        // foreach (var directory in  Directory.EnumerateDirectories(_fileRepositoryOptions.BasePath))
        // {
        //     foreach (var innerDirectory in Directory.EnumerateDirectories(directory))
        //     {
        //         foreach (var filePath in Directory.EnumerateFiles(innerDirectory))
        //         {
        //             File.Delete(filePath);
        //         }
        //         
        //         Directory.Delete(innerDirectory);
        //     }
        //     
        //     Directory.Delete(directory);
        // }
    }

    private void GetFileTest(string fileName)
    {
        var getFileResult = _fileService.GetFile(fileName);
        Assert.True(getFileResult.IsSuccess);
    }
    
    private async Task DeleteFileTest(string fileName)
    {
        var deleteFileResult = await _fileService.DeleteFile(fileName);
        Assert.True(deleteFileResult.IsSuccess);
    }

    [Theory]
    [MemberData(nameof(FilesData))]
    public async Task UpdateFileTest(string contentType, byte[] data, byte[] newData)
    {
        var fileNameResult = await _fileService.SaveFile(contentType, data);
        Assert.True(fileNameResult.IsSuccess);

        var getFileResult = _fileService.GetFile(fileNameResult.Value);
        Assert.True(getFileResult.IsSuccess);
        
        // TODO: complete test
    }

    #endregion

    #region Data

    public static IEnumerable<object[]> FilesData()
    {
        var filePaths = Directory.EnumerateFiles(TestInputDataOptions.FilesDirectory!);

        foreach (var filePath in filePaths)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            { 
                var buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);

                yield return new object[] { "jpg", buffer };
            }
        }
    }

    #endregion
}