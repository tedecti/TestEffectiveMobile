namespace TestEffectiveMobile.Exceptions;

public class InvalidFileException : Exception
{
    public InvalidFileException() : base("Invalid file")
    {
        throw new FileNotFoundException();
    }
}