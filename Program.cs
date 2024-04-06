using System.Text.Json;
using TestEffectiveMobile.Exceptions;
using TestEffectiveMobile.FileHandlers;

namespace TestEffectiveMobile;

internal static class Program
{
    public static void Main(string[] args)
    {
        const string configPath = @"public\config.json";
        if (!File.Exists(configPath)) return;
        var jsonString = File.ReadAllText(configPath);
        var config = JsonSerializer.Deserialize<Configuration>(jsonString);
        if (config != null)
        {
            if (string.IsNullOrEmpty(config.FileLog) || string.IsNullOrEmpty(config.FileOutput))
            {
                throw new InvalidConfigException();
            }
            WriteFile.WriteIp(config.FileLog, config.FileOutput, config.AddressStart, config.AddressMask, config.TimeStart, config.TimeEnd);
        }
        else
        {
            var parameters = ParseArguments(args);
            if (!parameters.ContainsKey("file-log") || !parameters.ContainsKey("file-output"))
            {
                Console.WriteLine(
                    "Usage: --file-log=<path> --file-output=<path> [--address-start=<IP>] [--address-mask=<mask>]");
                return;
            }

            var fileLog = parameters["file-log"];
            var fileOutput = parameters["file-output"];
            var addressStart = parameters.ContainsKey("address-start") ? parameters["address-start"] : null;
            var addressMask = int.Parse(parameters.ContainsKey("address-mask") ? parameters["address-mask"] : null);
            var timeStart = parameters["time-start"];
            var timeEnd = parameters["time-end"];

            WriteFile.WriteIp(fileLog, fileOutput, addressStart, addressMask, timeStart, timeEnd);
        }
    }

    private static Dictionary<string, string> ParseArguments(string[] args)
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

