namespace csharp_gestore_eventi.Exceptions;

public class EventOverException : Exception
{
    public EventOverException()
    {
    }

    public EventOverException(string message)
        : base(message)
    {
    }

    public EventOverException(string message, Exception inner)
        : base(message, inner)
    {
    }
}