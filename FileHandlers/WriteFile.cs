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
                var ipAddresses = ReadFile.GetIpAddressesWithSubnetMasks(inputPath);
                
                var requestTimes = ReadFile.GetRequestTimes(inputPath);
                
                var filteredAddressesWithMasks = FilterIp.FilterIpAddresses(ipAddresses, requestTimes, addressStart, addressMask, timeStart, timeEnd);
            
                var counts = filteredAddressesWithMasks
                    .GroupBy(ip => ip)
                    .Select(group =>
                    {
                        var ip = group.Key;
                        var index = ipAddresses.IndexOf(ip);
                       
            
                        var times = group.Select(g => requestTimes[ipAddresses.IndexOf(g)])
                            .Distinct()
                            .OrderBy(t => t)
                            .ToList();
                        var timeString = string.Join(", ", times);
                        return $"{ip} [{timeString}] {group.Count()}";
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