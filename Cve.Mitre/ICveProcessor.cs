using Cve.Mitre.Models;

namespace Cve.Mitre;

public interface ICveProcessor
{
    Task<List<SearchResult>> GetSearchResultsAsync(string keyWord, int? year = default);
    Task<List<Vulnerability>> GetVulnerabilitiesAsync(string keyWord, int? year = default);
    Task<List<Vulnerability>> GetVulnerabilitiesAsync(List<SearchResult> searchResults);
    Task<Vulnerability> GetVulnerabilityByIdAsync(string cveId);
    Task<Vulnerability> GetVulnerabilityAsync(SearchResult searchResult);
}