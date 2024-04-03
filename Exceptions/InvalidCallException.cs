namespace TestEffectiveMobile.Exceptions;

public class InvalidCallException : Exception
{
    public InvalidCallException() : base("Invalid call of custom operator")
    {
        throw new InvalidOperationException();
    }
}