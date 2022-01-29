using Cve.Mitre.Models.Extensions;
using Xunit;

namespace Cve.Mitre.Models.Tests.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData("   ", "")]
    [InlineData("test", "test")]
    [InlineData("   test   ", "test")]
    [InlineData("test Test", "test%20Test")]
    [InlineData("    test  test   ", "test%20%20test")]
    public void ShouldReturnStringWithoutSpaces(string value, string expectedValue)
    {
        var result = value.ReplaceSpaces();
        
        Assert.Equal(expectedValue, result);
    }
}