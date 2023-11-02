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
    public static uint CreateNumber(params short[] numbers)
    {
        uint result = 0;
        foreach (var x in numbers)
        {
            result |= 1u << x;
        }
        return result;
    }
}
