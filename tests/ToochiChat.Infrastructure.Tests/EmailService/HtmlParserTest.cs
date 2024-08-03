using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Infrastructure.EmailService.Interfaces;
using ToochiChat.Infrastructure.Tests.Extensions;

namespace ToochiChat.Infrastructure.Tests.EmailService;

public class HtmlParserTest
{
    private readonly IHtmlParser _htmlParser;
    private readonly IConfiguration _configuration;
    
    public HtmlParserTest()
    {
        _htmlParser = Services.Provider.GetRequiredService<IHtmlParser>();
        _configuration = Services.Provider.GetRequiredService<IConfiguration>();
    }

    [Fact]
    public void FirstParseArgumentExceptionTest()
    {
        Assert.Throws<ArgumentException>(() => 
            _htmlParser.Parse(_configuration.GetEmailHtmlFilePath()!, String.Empty));
    }
    
    [Fact]
    public void SecondParseArgumentExceptionTest()
    {
        Assert.Throws<ArgumentException>(() => 
            _htmlParser.Parse(_configuration.GetEmailHtmlFilePath()!, null!));
    }

    [Fact]
    public void ParseFileNotFoundExceptionTest()
    {
        Assert.Throws<FileNotFoundException>(() => 
            _htmlParser.Parse("some_wrong_path", String.Empty));
    }
}