namespace TripManager.Common.Exceptions.Application;

public class ApplicationValidationException : AppException
{
    public ApplicationValidationException(string message)
        : base(message)
    {
    }

    public ApplicationValidationException(string messageFormat, params object[] args)
        : base(messageFormat, args)
    {
    }
}