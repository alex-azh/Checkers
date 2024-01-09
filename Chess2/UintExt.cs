namespace CheckersGame;

public static class UintExt
{
    public static IEnumerable<uint> ExtractOnes(this uint board)
    {
        while (board > 0)
        {
            uint res = board & ~(board - 1);
            yield return res;
            board ^= res;
        }
    }
    public static uint Reverse(this uint x)
    {
        x = (x & 0x55555555) << 1 | (x >>> 1) & 0x55555555;  // Четные и нечетные биты поменялись местами.
        x = (x & 0x33333333) << 2 | (x >>> 2) & 0x33333333;  // Биты "перетасовываются" группами по два.
        x = (x & 0x0F0F0F0F) << 4 | (x >>> 4) & 0x0F0F0F0F;  // Биты "перетасовываются" группами по четыре.
        x = (x & 0x00FF00FF) << 8 | (x >>> 8) & 0x00FF00FF;  // Биты "перетасовываются" группами по восемь.
        x = (x & 0x0000FFFF) << 16 | (x >>> 16) & 0x0000FFFF;  // Биты "перетасовываются" группами по 16.
        return x;
    }
}
