namespace TaskManager.Utils.Exceptions;

public class RestBadRequestException : RestException
{
    public RestBadRequestException(string reason) : base(reason)
    {
    }
}