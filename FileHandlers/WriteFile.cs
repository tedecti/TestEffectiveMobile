namespace TestEffectiveMobile.FileHandlers;

public static class WriteFile
{
    private static void WriteNewFile(string path, string[] lines)
    {
        try
        {
            File.WriteAllLines(path, lines);
            Console.WriteLine("File has been written successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while writing to the file: {e.Message}");
            throw;
        }
    }

    public static void WriteIpCountsToFileWithTime(string inputPath, string outputPath)
    {
        try
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file does not exist.");
                return;
            }

            var lines = File.ReadAllLines(inputPath);

            var ipDetails = new Dictionary<string, List<string>>();

            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    if (!ipDetails.ContainsKey(parts[0]))
                    {
                        ipDetails[parts[0]] = new List<string> { parts[1] };
                    }
                    else
                    {
                        ipDetails[parts[0]].Add(parts[1]);
                    }
                }
            }
            
            var linesToWrite = ipDetails.Select(kvp =>
            {
                var ip = kvp.Key;
                var times = string.Join(", ", kvp.Value);
                var count = kvp.Value.Count;
                return $"{ip} [{times}] {count}";
            }).ToArray();

            WriteNewFile(outputPath, linesToWrite);
            Console.WriteLine("File has been written successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            throw;
        }
    }
}