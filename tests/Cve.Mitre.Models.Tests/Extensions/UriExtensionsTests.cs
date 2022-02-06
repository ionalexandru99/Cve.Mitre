using System.Runtime.InteropServices;
using Cve.Mitre.Models.Enums;
using Cve.Mitre.Models.Extensions;
using Xunit;

namespace Cve.Mitre.Models.Tests.Extensions;

public class UriExtensionsTests
{
    [Theory]
    [InlineData("1234", "https://cve.mitre.org/cgi-bin/cvename.cgi?name=1234")]
    [InlineData("Test", "https://cve.mitre.org/cgi-bin/cvename.cgi?name=Test")]
    [InlineData("test test", "https://cve.mitre.org/cgi-bin/cvename.cgi?name=test%20test")]
    [InlineData("     test     ", "https://cve.mitre.org/cgi-bin/cvename.cgi?name=test")]
    [InlineData("    test test   ", "https://cve.mitre.org/cgi-bin/cvename.cgi?name=test%20test")]
    public void ShouldReturnValidUri(string value, string expectedValue)
    {
        var result = value.UriFormat(UrlType.Vulnerability);
        
        Assert.NotNull(result);
        Assert.Equal(expectedValue, result!.AbsoluteUri);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void ShouldReturnDefaultUri(string? id)
    {
        var result = id.UriFormat(UrlType.Vulnerability);
        Assert.Null(result);
    }
}