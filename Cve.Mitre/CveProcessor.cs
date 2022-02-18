using Cve.Mitre.Clients;
using Cve.Mitre.Exceptions;
using Cve.Mitre.Models;
using Cve.Mitre.Parsers;

namespace Cve.Mitre;

/// <summary>
/// This class is the main orchestrator for working in this library.
/// It will handle all the necessary requests needed for extracting the data from the cve.mitre.org website
/// </summary>
public class CveProcessor : ICveProcessor
{
    private readonly IParser<SearchResult> _searchResultParser;
    private readonly IParser<Vulnerability> _vulnerabilityParser;
    private readonly ICveHttpClient _cveHttpClient;

    /// <summary>
    /// </summary>
    /// <param name="searchResultParser">An instance of the IParser interface capable of parsing html webpages containing Search Results</param>
    /// <param name="vulnerabilityParser">An instance of the IParser interface capable of parsing html webpages containing Vulnerabilities</param>
    /// <param name="cveHttpClient">An instance of the ICveHttpClient interface capable of making requests to the cve.mitre.org website</param>
    public CveProcessor(IParser<SearchResult> searchResultParser, IParser<Vulnerability> vulnerabilityParser, ICveHttpClient cveHttpClient)
    {
        _searchResultParser = searchResultParser;
        _vulnerabilityParser = vulnerabilityParser;
        _cveHttpClient = cveHttpClient;
    }
    
    /// <summary>
    /// Makes a request to the cve.mitre.org webpage searching for vulnerabilities containing the provided key word and were created in the year that was specified.
    /// </summary>
    /// <param name="keyWord">The word for which the search will be made</param>
    /// <param name="year">The time period in which the vulnerability was created. If no value is given, the parameter will be initialized with the default value.</param>
    /// <returns>Returns a list of Search Results found on the website containing the specified key word filtered by year if given.</returns>
    /// <exception cref="ArgumentException">This error will be thrown if no url can be generated for the provided keyword</exception>
    /// <exception cref="HttpRequestException">Http request was not successful</exception>
    public async Task<List<SearchResult>> GetSearchResultsAsync(string keyWord, int? year = default)
    {
        var htmlPage = await _cveHttpClient.GetSearchResults(keyWord);
        var searchResults = _searchResultParser.Parse(htmlPage);

        if (year == default)
            return searchResults;
        
        searchResults = searchResults
            .Where(x => x.Name.StartsWith($"CVE-{year}"))
            .ToList();

        return searchResults;
    }

    /// <summary>
    /// Makes a request to the cve.mitre.org webpage searching for vulnerabilities containing the provided key word
    /// </summary>
    /// <param name="keyWord">The word for which the search will be made</param>
    /// <param name="year">The time period in which the vulnerability was created. If no value is given, the parameter will be initialized with the default value.</param>
    /// <returns>A list with all the vulnerabilities found from searching the provided key word and filtered by year if value is given.</returns>
    /// <exception cref="ArgumentException">This error will be thrown if no url can be generated for the provided keyword</exception>
    /// <exception cref="HttpRequestException">Http request was not successful</exception>
    /// <exception cref="InvalidCveIdException">This exception is thrown when the provided CveId is not found on the website</exception>
    public async Task<List<Vulnerability>> GetVulnerabilitiesAsync(string keyWord, int? year = default)
    {
        var searchResults = await GetSearchResultsAsync(keyWord, year);

        return await GetVulnerabilitiesAsync(searchResults);
    }

    /// <summary>
    /// Makes a request to the cve.mitre.org webpage extracting the vulnerabilities specified by the provided search result objects
    /// </summary>
    /// <param name="searchResults">List of search result objects containing the CveId of the vulnerabilities</param>
    /// <returns>A list of vulnerabilities from the provided search result objects</returns>
    /// <exception cref="ArgumentException">This error will be thrown if no url can be generated for the provided keyword</exception>
    /// <exception cref="HttpRequestException">Http request was not successful</exception>
    /// <exception cref="InvalidCveIdException">This exception is thrown when the provided CveId is not found on the website</exception>
    public async Task<List<Vulnerability>> GetVulnerabilitiesAsync(List<SearchResult> searchResults)
    {
        var vulnerabilities = new List<Vulnerability>();
        foreach (var searchResult in searchResults)
        {
            var vulnerability = await GetVulnerabilityAsync(searchResult);
            vulnerabilities.Add(vulnerability);
        }

        return vulnerabilities;
    }

    /// <summary>
    /// Makes a request to the cve.mitre.org webpage extracting the vulnerability with the specified CveId
    /// </summary>
    /// <param name="cveId">The CveId of the desired vulnerability</param>
    /// <returns>A vulnerability with the specified CveId</returns>
    /// <exception cref="ArgumentException">This error will be thrown if no url can be generated for the provided keyword</exception>
    /// <exception cref="HttpRequestException">Http request was not successful</exception>
    /// <exception cref="InvalidCveIdException">This exception is thrown when the provided CveId is not found on the website</exception>
    public async Task<Vulnerability> GetVulnerabilityByIdAsync(string cveId)
    {
        var vulnerabilityHtml = await _cveHttpClient.GetVulnerability(cveId);
        var vulnerabilitiesFromHtml = _vulnerabilityParser.Parse(vulnerabilityHtml);

        return vulnerabilitiesFromHtml.Single();
    }

    /// <summary>
    /// Makes a request to the cve.mitre.org webpage extracting the vulnerability with the specified CveId
    /// </summary>
    /// <param name="searchResult">The search result object containing the CveId of the desired vulnerability</param>>
    /// <returns>A vulnerability with the specified CveId</returns>
    /// <exception cref="ArgumentException">This error will be thrown if no url can be generated for the provided keyword</exception>
    /// <exception cref="HttpRequestException">Http request was not successful</exception>
    /// <exception cref="InvalidCveIdException">This exception is thrown when the provided CveId is not found on the website</exception>
    public async Task<Vulnerability> GetVulnerabilityAsync(SearchResult searchResult)
    {
        return await GetVulnerabilityByIdAsync(searchResult.Name);
    }
}