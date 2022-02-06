namespace Cve.Mitre.Parsers;

public interface IParser<T> where T : class
{
    List<T> Parse(string htmlPage);
}