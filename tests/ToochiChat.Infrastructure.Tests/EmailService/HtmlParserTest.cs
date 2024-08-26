using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToochiChat.Infrastructure.EmailService.Interfaces;
using ToochiChat.Infrastructure.Tests.Extensions;

namespace ToochiChat.Infrastructure.Tests.EmailService;

public class HtmlParserTest
{
    private readonly IHtmlParser _htmlParser;

    public HtmlParserTest()
    {
        _htmlParser = Services.Provider.GetRequiredService<IHtmlParser>();
    }
    
    [Fact]
    public void ParseFileNotFoundExceptionTest()
    {
        Assert.Throws<FileNotFoundException>(() => 
            _htmlParser.Parse("some_wrong_path"));
    }
}