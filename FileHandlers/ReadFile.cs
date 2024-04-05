namespace TestEffectiveMobile.FileHandlers;

public static class ReadFile
{
    public static List<string> GetIpAddresses(string path)
    {
        var ipAddresses = new List<string>();
        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist.");
                return new List<string>();
            }

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    var ipPart = parts[0].Split('/')[0];
                    ipAddresses.Add(ipPart);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while reading the file: {e.Message}");
            return new List<string>();
        }

        return ipAddresses;
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
                var parts = line.Split(new[] { ':' }, 3);
                if (parts.Length == 3)
                {
                    requestTimes.Add(parts[1].Trim() + ":" + parts[2].Trim());
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

    public static List<int> GetSubnetMasks(string path)
    {
        var subnetMasks = new List<int>();

        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("File does not exist.");
                return new List<int>();
            }
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { '/', ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 2) continue;
                if (int.TryParse(parts[1], out var mask))
                {
                    subnetMasks.Add(mask);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while reading the file: {e.Message}");
        }

        return subnetMasks;
    }
    public static List<string> GetIpAddressesWithSubnetMasks(string path)
    {
        var ipAddressesWithMasks = new List<string>();

        try
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("The specified file does not exist.");
                return ipAddressesWithMasks;
            }
            
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length >= 2)
                {
                    ipAddressesWithMasks.Add(parts[0].Trim());
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while reading the file: {e.Message}");
        }

        return ipAddressesWithMasks;
    }
}