using Cve.Mitre.Models;

namespace Cve.Mitre;

public interface ICveProcessor
{
    Task<List<SearchResult>> GetSearchResults(string keyWord);
    Task<List<SearchResult>> GetSearchResults(string keyWord, int year);
    List<Vulnerability> GetVulnerabilities(string keyWord);
    List<Vulnerability> GetVulnerabilities(string keyWord, int year);
    List<Vulnerability> GetVulnerabilities(List<SearchResult> searchResults);
    Vulnerability GetVulnerabilityById(string cveId);
    Vulnerability GetVulnerability(SearchResult searchResult);
}