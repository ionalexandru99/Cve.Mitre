using Cve.Mitre.Exceptions;
using Cve.Mitre.Models.Enums;
using Cve.Mitre.Models.Extensions;

namespace Cve.Mitre.Clients;

public class CveHttpClient : ICveHttpClient
{
    /// <summary>
    /// Returns the html page containing all the vulnerabilities found using the given keyword
    /// </summary>
    /// <param name="keyWord">The value for which the search is made</param>
    /// <returns>String containing html code with result vulnerabilities</returns>
    /// <exception cref="ArgumentException">This error will be thrown if no url can be generated for the provided keyword</exception>
    /// <exception cref="HttpRequestException">Http request was not successful</exception>
    public async Task<string> GetSearchResults(string keyWord)
    {
        var url = keyWord.UriFormat(UrlType.SearchResult);
        if (url == default)
        {
            throw new ArgumentException("Could not generate url from the provided keyword");
        }

        var client = new HttpClient();
        
        var result = await client.GetAsync(url);

        result.EnsureSuccessStatusCode();

        return await result.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Returns the html page containing the data of the vulnerability requested
    /// </summary>
    /// <param name="cveId">The value for which the request is made</param>
    /// <returns>String containing html code with result data</returns>
    /// <exception cref="ArgumentException">This error will be thrown if no url can be generated for the provided keyword</exception>
    /// <exception cref="HttpRequestException">Http request was not successful</exception>
    /// <exception cref="InvalidCveIdException">This exception is thrown when the provided CveId is not found on the website</exception>
    public async Task<string> GetVulnerability(string cveId)
    {
        var url = cveId.UriFormat(UrlType.Vulnerability);
        if (url == default)
        {
            throw new ArgumentException("Could not generate url from the provided id");
        }

        var client = new HttpClient();
        
        var result = await client.GetAsync(url);

        result.EnsureSuccessStatusCode();

        var resultBody = await result.Content.ReadAsStringAsync();

        if (resultBody.Contains($"'{cveId}' is a malformed CVE-ID") || 
            resultBody.Contains($"Couldn't find '{cveId}'"))
        {
            throw new InvalidCveIdException("The specified CveId could not be found on the website");
        }
        
        return await result.Content.ReadAsStringAsync();
    }
}