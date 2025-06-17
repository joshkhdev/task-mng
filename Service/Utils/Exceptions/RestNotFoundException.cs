namespace TaskManager.Utils.Exceptions;

public class RestNotFoundException : RestException
{
    public RestNotFoundException(string reason) : base(reason)
    {
    }
}