using Cve.Mitre.Models.Enums;

namespace Cve.Mitre.Models.Extensions;

public static class UriExtensions
{
    public static Uri? FormatUri(this string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        var uriAsString = UriConstants.VulnerabilityUriFormat;
        uriAsString = string.Format(uriAsString, id);
        
        return new Uri(uriAsString);
    }
}