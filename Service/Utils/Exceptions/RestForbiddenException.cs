namespace TaskManager.Utils.Exceptions;

public class RestForbiddenException : RestException
{
    public RestForbiddenException(string reason) : base(reason)
    {
    }
}