namespace Muvids.Application.Exceptions;

public class ConflictBookingException : ApplicationException
{
    public ConflictBookingException(string name)
        : base($"{name}")
    {
    }
}
