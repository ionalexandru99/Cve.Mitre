namespace Cve.Mitre.Exceptions;

public class InvalidCveIdException : Exception
{
    public InvalidCveIdException(string msg) : base(msg) { }
}