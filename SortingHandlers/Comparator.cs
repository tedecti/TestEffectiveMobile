using static System.Int32;

namespace TestEffectiveMobile.SortingHandlers;

public static class Comparator
{
    public static int IpComparator(string a, string b)
    {
        var octetsA = a.Trim().Split('.');
        var octetsB = b.Trim().Split('.');
        
        // TODO: попробовать импрувнуть эту шнягу
        if (octetsA.SequenceEqual(octetsB))
        {
            return 0;
        }
        else if (Parse(octetsA[0]) > Parse(octetsB[0]))
        {
            return 1;
        }
        else if (Parse(octetsA[0]) < Parse(octetsB[0]))
        {
            return -1;
        }
        else if (Parse(octetsA[1]) > Parse(octetsB[1]))
        {
            return 1;
        }
        else if (Parse(octetsA[1]) < Parse(octetsB[1]))
        {
            return -1;
        }
        else if (Parse(octetsA[2]) > Parse(octetsB[2]))
        {
            return 1;
        }
        else if (Parse(octetsA[2]) < Parse(octetsB[2]))
        {
            return -1;
        }
        else if (Parse(octetsA[3]) > Parse(octetsB[3]))
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }
}