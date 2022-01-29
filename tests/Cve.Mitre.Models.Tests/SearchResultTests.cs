using Xunit;

namespace Cve.Mitre.Models.Tests;

public class SearchResultTests
{
    [Fact]
    public void ShouldContainValidUri()
    {
        const string expectedUri = "https://cve.mitre.org/cgi-bin/cvename.cgi?name=test";
        
        var searchResult = new SearchResult
        {
            Name = "test",
            Description = "Test Description"
        };
        
        Assert.NotNull(searchResult.VulnerabilityUri);
        Assert.Equal(expectedUri, searchResult.VulnerabilityUri!.AbsoluteUri);
    }

    [Fact]
    public void ShouldContainNullUri()
    {
        var searchResult = new SearchResult
        {
            Name = "",
            Description = "Test Description"
        };
        
        Assert.Null(searchResult.VulnerabilityUri);
    }
}