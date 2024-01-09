namespace Chess2;
public static class UintHelper
{
    public static IEnumerable<uint> Moves(uint figuresPositions)
    {
        while (figuresPositions > 0)
        {
            var res = figuresPositions & ~(figuresPositions - 1);
            yield return res;
            figuresPositions ^= res;
        }
    }
    public static uint CreateNumber(params int[] numbers)
    {
        uint result = 0;
        foreach (var index in numbers)
        {
            result |= 1u << index;
        }
        return result;
    }
    public static uint Reverse(uint x)
    {
        x = ((x & 0x55555555) << 1) | ((x >>> 1) & 0x55555555);  // Четные и нечетные биты поменялись местами.
        x = ((x & 0x33333333) << 2) | ((x >>> 2) & 0x33333333);  // Биты "перетасовываются" группами по два.
        x = ((x & 0x0F0F0F0F) << 4) | ((x >>> 4) & 0x0F0F0F0F);  // Биты "перетасовываются" группами по четыре.
        x = ((x & 0x00FF00FF) << 8) | ((x >>> 8) & 0x00FF00FF);  // Биты "перетасовываются" группами по восемь.
        x = ((x & 0x0000FFFF) << 16) | ((x >>> 16) & 0x0000FFFF);  // Биты "перетасовываются" группами по 16.
        return x;
    }
    public static bool[] GetBoolArray(CheckersBoard board)
    {
        bool[] result = new bool[128];
        for (int i = 0; i < 32; ++i)
        {
            result[i] = ((board.WhiteP >> i) & 0x01) > 0;
            result[i+32] = ((board.WhiteD >> i) & 0x01) > 0;
            result[i+64] = ((board.BlackP >> i) & 0x01) > 0;
            result[i+96] = ((board.BlackD >> i) & 0x01) > 0;
        }
        return result;
    }

    public static uint AllBlack = 0b11_11111_11111_00000_00000_00000_00000;
    public static uint AllWhite = 0b00_00000_00000_00000_00011_11111_11111;
}