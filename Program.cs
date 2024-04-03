using TestEffectiveMobile.Exceptions;

namespace TestEffectiveMobile;

internal static class Program
{
    public static void Main(string[] args)
    {
        var parameters = ParseArguments(args);
        //TODO: попробовать улучшить колбасу
        if (!parameters.ContainsKey("file-log") || !parameters.ContainsKey("file-output"))
        {
            Console.WriteLine("Usage: --file-log=<path> --file-output=<path> [--address-start=<IP>] [--address-mask=<mask>]");
            return;
        }
        var fileLog = parameters["file-log"];
        var fileOutput = parameters["file-output"];
        var addressStart = parameters.ContainsKey("address-start") ? parameters["address-start"] : null;
        var addressMask = parameters.ContainsKey("address-mask") ? parameters["address-mask"] : null;
    }

    static Dictionary<string, string> ParseArguments(string[] args)
    {
        var parameters = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var arg in args)
        {
            if (arg.StartsWith("--"))
            {
                var keyValue = arg.TrimStart('-').Split('=');
                if (keyValue.Length == 2)
                {
                    parameters[keyValue[0]] = keyValue[1];
                }
                else
                {
                    throw new InvalidCallException();
                }
            }
        }
        return parameters;
    }
}

