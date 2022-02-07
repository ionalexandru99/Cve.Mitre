using System;
using System.Threading.Tasks;
using Cve.Mitre.Clients;
using Cve.Mitre.Exceptions;
using Xunit;

namespace Cve.Mitre.Integration.Tests.Clients;

public class CveHttpClientTests
{
    [Fact]
    public async Task ShouldThrowErrorIfKeyWordIsInvalid()
    {
        const string keyword = "";

        var cveHttpClient = new CveHttpClient();

        await Assert.ThrowsAsync<ArgumentException>(() => cveHttpClient.GetSearchResults(keyword));
    }
    
    [Fact]
    public async Task ShouldThrowErrorIfCveIdIsInvalid()
    {
        const string id = "";

        var cveHttpClient = new CveHttpClient();

        await Assert.ThrowsAsync<ArgumentException>(() => cveHttpClient.GetVulnerability(id));
    }

    [Fact]
    public async Task ShouldReturnValidSearchData()
    {
        const string keyword = "test";

        var cveHttpClient = new CveHttpClient();
        var result = await cveHttpClient.GetSearchResults(keyword);
        Assert.Contains("Search Results", result);
    }
    
    [Fact]
    public async Task ShouldReturnValidVulnerabilityData()
    {
        const string id = "CVE-2022-0238"; //This id should be on the website but it can also be taken down by the website owners

        var cveHttpClient = new CveHttpClient();
        var result = await cveHttpClient.GetVulnerability(id);
        Assert.Contains("Description", result);
    }

    [Fact]
    public async Task ShouldThrowErrorForInvalidId()
    {
        const string id = "CVE-2022-02";

        var cveHttpClient = new CveHttpClient();

        await Assert.ThrowsAsync<InvalidCveIdException>(() => cveHttpClient.GetVulnerability(id));
    }
}