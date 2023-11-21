using System;
using System.Collections;

namespace Chess2;
public static class UintHelper
{
    public static IEnumerable<uint> Indexes(uint x)
    {
        while (x > 0)
        {
            var res = x & ~(x - 1);
            yield return res;
            x ^= res;
        }
    }
    public static uint CreateNumber(params int[] numbers)
    {
        uint result = 0;
        foreach (var x in numbers)
        {
            result |= 1u << x;
        }
        return result;
    }
    public static uint Reverse1(uint x)
    {
        x = ((x & 0x55555555) << 1) | ((x >>> 1) & 0x55555555);  // Четные и нечетные биты поменялись местами.
        x = ((x & 0x33333333) << 2) | ((x >>> 2) & 0x33333333);  // Биты "перетасовываются" группами по два.
        x = ((x & 0x0F0F0F0F) << 4) | ((x >>> 4) & 0x0F0F0F0F);  // Биты "перетасовываются" группами по четыре.
        x = ((x & 0x00FF00FF) << 8) | ((x >>> 8) & 0x00FF00FF);  // Биты "перетасовываются" группами по восемь.
        x = ((x & 0x0000FFFF) << 16) | ((x >>> 16) & 0x0000FFFF);  // Биты "перетасовываются" группами по 16.
        return x;
    }
    public static uint Reverse2(uint x)
    {
        uint result = 0;
        IEnumerable<uint> items = Indexes(x);
        foreach (var item in items)
        {
            int index = (int)Math.Log2(item);
            result |= 1u << (31 - index);
        }
        return result;
    }

    public static Array GetBitArray(IEnumerable<uint> data)
    {
        bool[] result = new bool[128];
        int index = 0;
        foreach (uint item in data)
        {
            BitArray ba = new BitArray(BitConverter.GetBytes(item));
            //ba.CopyTo(result, index);
            ba.Cast<bool>().Reverse().ToArray().CopyTo(result, index);
            index += 32;
        }
        return result;
    } 
}