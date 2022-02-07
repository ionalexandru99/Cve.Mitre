using Cve.Mitre.Models.Enums;
using Cve.Mitre.Models.Extensions;

namespace Cve.Mitre.Models;

public class SearchResult
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Uri? VulnerabilityUri => Name.UriFormat(UrlType.Vulnerability);
}