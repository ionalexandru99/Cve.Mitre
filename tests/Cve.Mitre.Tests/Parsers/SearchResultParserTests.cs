using System.IO;
using System.Reflection;
using Cve.Mitre.Exceptions;
using Cve.Mitre.Parsers;
using Xunit;

namespace Cve.Mitre.Tests.Parsers;

public class SearchResultParserTests
{
    private const string ValidHtml = "Cve.Mitre.Tests.Resources.SearchResults.html";
    private const string InvalidHtml = "Cve.Mitre.Tests.Resources.InvalidSearchResults.html";

    [Fact]
    public void ShouldReturnListWithAllSearchResults()
    {
        var parser = new SearchResultParser();
        var htmlContent = GetHtmlString(ValidHtml);

        const int expectedResultCount = 9;

        var result = parser.Parse(htmlContent);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(expectedResultCount, result.Count);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    [InlineData("<html><head></head><body></body></html>")]
    public void ShouldReturnEmptyList(string htmlContent)
    {
        var parser = new SearchResultParser();

        var result = parser.Parse(htmlContent);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ShouldThrowErrorIfInvalidData()
    {
        var parser = new SearchResultParser();
        var htmlContent = GetHtmlString(InvalidHtml);

        Assert.Throws<HtmlDataException>(() => parser.Parse(htmlContent));
    }

    private static string GetHtmlString(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();

        using var stream = assembly.GetManifestResourceStream(resourceName);

        if (stream == default)
        {
            return string.Empty;
        }

        using var reader = new StreamReader(stream);
        var result = reader.ReadToEnd();

        return result;
    }
}