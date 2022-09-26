namespace csharp_gestore_eventi.Exceptions;

public class InvalidSeatsException : Exception
{
    public InvalidSeatsException()
    {
    }

    public InvalidSeatsException(string message)
        : base(message)
    {
    }

    public InvalidSeatsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}