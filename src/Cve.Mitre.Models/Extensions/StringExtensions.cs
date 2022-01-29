namespace Cve.Mitre.Models.Extensions;

public static class StringExtensions
{
    private const string SpaceString = " ";
    private const string UriSpaceString = "%20";
    
    public static string ReplaceSpaces(this string value)
    {
        return value.Trim().Replace(SpaceString, UriSpaceString);
    }
}