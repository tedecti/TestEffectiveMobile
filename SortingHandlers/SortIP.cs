namespace TestEffectiveMobile.SortingHandlers;

public static class SortIp
{
    public static void SortIpAddress(string[] arr)
    {
        Array.Sort(arr, Comparator.IpComparator);
    }
}