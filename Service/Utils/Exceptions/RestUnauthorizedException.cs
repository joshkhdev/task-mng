namespace TaskManager.Utils.Exceptions;

public class RestUnauthorizedException : RestException
{
    public RestUnauthorizedException(string reason) : base(reason)
    {
    }
}