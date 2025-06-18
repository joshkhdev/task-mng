namespace TaskManager.Utils.Exceptions;

public class RestConflictException : RestException
{
    public RestConflictException(string reason) : base(reason)
    {
    }
}