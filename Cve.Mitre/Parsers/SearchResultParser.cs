using Cve.Mitre.Exceptions;
using Cve.Mitre.Models;

namespace Cve.Mitre.Parsers;

public class SearchResultParser : IParser<SearchResult>
{
    private const string ContentHeaderDelimiter = @"<h2>Search Results</h2>";
    private const string ContentFooterDelimiter = "<div class=\"backtop noprint\"><a href=\"#top\">Back to top</a></div>"; //TODO check if bug is here

    private const string TableHeaderDelimiter = "</thead>";

    private const string TableRowEnd = "</tr>";
    private const string TableRowStart = "<tr>";

    private const string TableDataEnd = "</td>";
    
    private const string DescriptionStart = "<td valign=\"top\">";
    private const string IdEnd = "</a>";
    private const string IdStart = ">";
    
    
    public List<SearchResult> Parse(string htmlPage)
    {
        if (string.IsNullOrWhiteSpace(htmlPage))
        {
            return new List<SearchResult>();
        }
        
        var tableHtml = ExtractTable(htmlPage);

        if (string.IsNullOrWhiteSpace(tableHtml))
        {
            return new List<SearchResult>();
        }
        
        var tableRows = ExtractTableRows(tableHtml).ToList();

        if (tableRows.Count == 0)
        {
            return new List<SearchResult>();
        }
        
        return tableRows
            .Where(x => x.Contains(TableDataEnd))
            .Select(ExtractSearchResult).ToList();
    }
    
    private static string ExtractTable(string html)
    {
        return html
            .Split(ContentHeaderDelimiter).Last()
            .Split(ContentFooterDelimiter).First()
            .Split(TableHeaderDelimiter).Last()
            .Trim();
    }

    private static IEnumerable<string> ExtractTableRows(string html)
    {
        if (!html.Contains(TableRowEnd))
        {
            return new List<string>();
        }
        
        var tableRows = html.Split(TableRowEnd).ToList();
        
        tableRows = tableRows
            .Select(x => x.Split(TableRowStart).Last())
            .ToList();
        return tableRows;
    }

    private static SearchResult ExtractSearchResult(string html)
    {
        var (idData, descriptionData) = DeconstructTableRow(html);

        var description = descriptionData
            .Split(DescriptionStart).Last()
            .Trim();

        var id = idData
            .Split(IdEnd).First()
            .Split(IdStart).Last()
            .Trim();
        
        return new SearchResult
        {
            Name = id,
            Description = description
        };
    }

    /// <summary>
    /// Returns a tuple containing the html parts of the provided parameter that could contain the SearchResult id and description
    /// </summary>
    /// <param name="html">A html string containing the table data that needs to be used for generating SearchResult objects</param>
    /// <returns></returns>
    /// <exception cref="HtmlDataException">Will be thrown if there are more or less than 2 table data blocks in the html string parameter</exception>
    private static (string, string) DeconstructTableRow(string html)
    {
        var data = html
            .Trim()
            .Split(TableDataEnd)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToArray();

        if (data.Length != 2)
        {
            throw new HtmlDataException($"Could not extract the data for generating the {nameof(SearchResult)} data type");
        }
        
        return (data[0], data[1]);
    }
}