using Cve.Mitre.Models.Enums;

namespace Cve.Mitre.Models.Extensions;

public static class UriExtensions
{
    /// <summary>
    /// Will return an url if for the provided id depending on the Url type provided. If no Id is provided will return null
    /// </summary>
    /// <param name="id">The id for which the url needs to be generated</param>
    /// <param name="type">The Url type that is requested</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException">Will be thrown if provided url type is not supported by the application</exception>
    public static Uri? UriFormat(this string? id, UrlType type)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        var cleanedId = id.ReplaceSpaces();

        var uriAsString = type switch
        {
            UrlType.SearchResult => UriConstants.SearchResultsUriFormat,
            UrlType.Vulnerability => UriConstants.VulnerabilityUriFormat,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "This Url type is not supported")
        };
        
        uriAsString = string.Format(uriAsString, cleanedId);
        
        return new Uri(uriAsString);
    }
}