using Cve.Mitre.Models.Enums;

namespace Cve.Mitre.Models.Extensions;

public static class UriExtensions
{
    public static Uri? UriFormat(this string? id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        var cleanedId = id.ReplaceSpaces();

        var uriAsString = UriConstants.VulnerabilityUriFormat;
        uriAsString = string.Format(uriAsString, cleanedId);
        
        return new Uri(uriAsString);
    }
}