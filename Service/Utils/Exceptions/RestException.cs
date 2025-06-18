namespace TaskManager.Utils.Exceptions;

public class RestException : Exception
{
    public string DebugMessage { get; }

    public RestException(string reason, string debugMessage = "") : base(reason)
    {
        DebugMessage = debugMessage;
    }
}