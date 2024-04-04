namespace TestEffectiveMobile.FileHandlers;

public static class ReadFile
{
    public static string[] GetIpAddresses(string path)
    {
        var ipAddresses = new List<string>();
        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist.");
                return Array.Empty<string>();
            }

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    ipAddresses.Add(parts[0]);
                }
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            return Array.Empty<string>();
        }

        return ipAddresses.ToArray();
    }

    public static string[] GetRequestTimes(string path)
    {
        var requestTimes = new List<string>();

        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist.");
                return Array.Empty<string>();
            }

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    requestTimes.Add(parts[1]);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            return Array.Empty<string>();
        }

        return requestTimes.ToArray();
    }
}