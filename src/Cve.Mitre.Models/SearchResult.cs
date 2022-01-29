namespace Cve.Mitre.Models;

public class SearchResult
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Uri? Link { get; set; }
}