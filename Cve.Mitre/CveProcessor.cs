using Cve.Mitre.Clients;
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
    /// Makes a request to the cve.mitre.org webpage searching for vulnerabilities containing the provided key word.
    /// </summary>
    /// <param name="keyWord">The word for which the search will be made</param>
    /// <returns>Returns a list of Search Results found on the website containing the specified key word</returns>
    public async Task<List<SearchResult>> GetSearchResults(string keyWord)
    {
        var htmlPage = await _cveHttpClient.GetSearchResults(keyWord);
        var searchResults = _searchResultParser.Parse(htmlPage);

        return searchResults;
    }

    /// <summary>
    /// Makes a request to the cve.mitre.org webpage searching for vulnerabilities containing the provided key word and were created in the year that was specified.
    /// </summary>
    /// <param name="keyWord">The word for which the search will be made</param>
    /// <param name="year">The time period in which the vulnerability was created</param>
    /// <returns>Returns a list of Search Results found on the website containing the specified key word filtered by year</returns>
    public async Task<List<SearchResult>> GetSearchResults(string keyWord, int year)
    {
        var searchResults = await GetSearchResults(keyWord);

        searchResults = searchResults
            .Where(x => x.Name.StartsWith($"CVE-{year}"))
            .ToList();

        return searchResults;
    }

    public List<Vulnerability> GetVulnerabilities(string keyWord)
    {
        throw new NotImplementedException();
    }

    public List<Vulnerability> GetVulnerabilities(string keyWord, int year)
    {
        throw new NotImplementedException();
    }

    public List<Vulnerability> GetVulnerabilities(List<SearchResult> searchResults)
    {
        throw new NotImplementedException();
    }

    public Vulnerability GetVulnerabilityById(string cveId)
    {
        throw new NotImplementedException();
    }

    public Vulnerability GetVulnerability(SearchResult searchResult)
    {
        throw new NotImplementedException();
    }
}