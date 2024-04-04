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

    public static void WriteIpCountsToFile(string inputPath, string outputPath)
    {
        try
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file does not exist.");
                return;
            }
            
            var ipAddresses = ReadFile.GetIpAddresses(inputPath);
            
            var ipCounts = ipAddresses.GroupBy(ip => ip)
                .ToDictionary(group => group.Key, group => group.Count());
            
            var linesToWrite = ipCounts.Select(kvp => $"{kvp.Key} {kvp.Value}").ToArray();
            
            WriteNewFile(outputPath, linesToWrite);
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            throw;
        }
    }
}