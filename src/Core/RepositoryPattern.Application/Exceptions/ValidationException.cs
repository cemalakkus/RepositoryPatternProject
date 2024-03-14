namespace RepositoryPattern.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : this("validation error occured") { }
    public ValidationException(string message) : base(message) { }
    public ValidationException(Exception ex):this(ex.Message) { }
}
