using System.Globalization;
using System.Net;

namespace TestEffectiveMobile.FilterHandlers;

public static class FilterIp
{
    public static List<string> FilterIpAddresses(List<string> ipAddressesWithMasks, string[] requestTimes,
            string addressStart = null, int? addressMask = null, string timeStart = null, string timeEnd = null)
        {
            if (ipAddressesWithMasks.Count != requestTimes.Length)
            {
                throw new ArgumentException("The count of IP addresses must match the count of request times.");
            }

            var filtered = ipAddressesWithMasks.Zip(requestTimes, (ipWithMask, time) => new { IpWithMask = ipWithMask, Time = time });

            if (!string.IsNullOrEmpty(addressStart))
            {
                var lowerBound = AddressToLong(IPAddress.Parse(addressStart));
                filtered = filtered.Where(x => AddressToLong(IPAddress.Parse(x.IpWithMask.Split('/')[0])) >= lowerBound);
            }

            if (addressMask.HasValue)
            {
                filtered = filtered.Where(x => 
                {
                    var mask = int.Parse(x.IpWithMask.Split('/')[1]);
                    return mask >= addressMask.Value;
                });
            }

            if (!string.IsNullOrEmpty(timeStart) && !string.IsNullOrEmpty(timeEnd))
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
            
            return filtered.Select(x => x.IpWithMask).ToList();
        }

        private static long AddressToLong(IPAddress ip)
        {
            var bytes = ip.GetAddressBytes();
            return bytes.Aggregate<byte, long>(0, (current, t) => (current << 8) + t);
        }
}