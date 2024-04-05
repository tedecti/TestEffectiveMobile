using TestEffectiveMobile.FilterHandlers;

namespace TestEffectiveMobile.FileHandlers;

public static class WriteFile
{
    public static void WriteIp(string inputPath, string outputPath, string addressStart = null, int? addressMask = null, string timeStart = null, string timeEnd = null)
    {
        try
        {
            if (!File.Exists(inputPath))
            {
                Console.WriteLine("Input file does not exist.");
                return;
            }
            var ipAddresses = ReadFile.GetIpAddresses(inputPath);
            var requestTimes = ReadFile.GetRequestTimes(inputPath);

            var filteredAddresses = FilterIp.FilterIpAddresses(ipAddresses, requestTimes, addressStart, addressMask, timeStart, timeEnd);
                
            // Подготовка данных к записи: сгруппировать по IP и подсчитать количество, сохраняя временные метки
            var counts = filteredAddresses
                .GroupBy(ip => ip)
                .Select(group =>
                {
                    // Извлечение соответствующих временных меток для каждого IP-адреса
                    var times = group.Select(g => requestTimes[ipAddresses.IndexOf(g)])
                        .Distinct()
                        .OrderBy(t => t)
                        .ToList();
                    var timeString = string.Join(", ", times);
                    return $"{group.Key} [{timeString}] {group.Count()}";
                })
                .ToArray();

            File.WriteAllLines(outputPath, counts);
            Console.WriteLine("File has been written successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
            throw;
        }
    }

}