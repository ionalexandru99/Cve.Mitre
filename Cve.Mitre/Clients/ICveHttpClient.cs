namespace Cve.Mitre.Clients;

public interface ICveHttpClient
{
    Task<string> GetSearchResults(string keyWord);
    Task<string> GetVulnerability(string cveId);
}