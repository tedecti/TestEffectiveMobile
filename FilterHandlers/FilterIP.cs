using System.Globalization;
using System.Net;

namespace TestEffectiveMobile.FilterHandlers;

public static class FilterIp
{
    public static List<string> FilterIpAddresses(List<string> ipAddresses, string[] requestTimes, string addressStart = null, int? addressMask = null, string timeStart = null, string timeEnd = null)
    {
        if (ipAddresses.Count != requestTimes.Length)
        {
            throw new ArgumentException("The count of IP addresses must match the count of request times.");
        }

        var filtered = ipAddresses.Zip(requestTimes, (ip, time) => new { Ip = ip, Time = time });

        if (!string.IsNullOrEmpty(addressStart))
        {
            var lowerBound = AddressToLong(IPAddress.Parse(addressStart));
            filtered = filtered.Where(x => AddressToLong(IPAddress.Parse(x.Ip)) >= lowerBound);

            if (addressMask.HasValue)
            {
                var upperBound = lowerBound + addressMask.Value;
                filtered = filtered.Where(x => AddressToLong(IPAddress.Parse(x.Ip)) <= upperBound);
            }
        }

        if (string.IsNullOrEmpty(timeStart) || string.IsNullOrEmpty(timeEnd))
            return filtered.Select(x => x.Ip).ToList();
        {
            var startTime = DateTime.ParseExact(timeStart, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            var endTime = DateTime.ParseExact(timeEnd, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            
            filtered = filtered.Where(x => 
            {
                var timePart = x.Time.Split(' ')[0];
                var currentTime = DateTime.ParseExact(timePart, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                return currentTime >= startTime && currentTime <= endTime;
            });
        }

        return filtered.Select(x => x.Ip).ToList();
    }

    private static long AddressToLong(IPAddress ip)
    {
        var bytes = ip.GetAddressBytes();
        return bytes.Aggregate<byte, long>(0, (current, t) => (current << 8) + t);
    }
}
