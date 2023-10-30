Test();

#region Tests
void Test()
{
    var doska = new Doska(
    WhiteP: 0b00000000000000000000000000000001,
    WhiteD: 0b0,
    BlackP: 0,
    BlackD: 512);
    // x, sopernik, all
    Func<uint, uint, uint, uint> hod1 = (uint x, uint sopernik, uint all) =>
    {
        var x4 = x << 4;
        return (((x4 & sopernik) << 5) | x4) & ~all;
    };
    var result = hod1(0b00000000000000000000000000000001, doska.Blacks, doska.All);
    Console.WriteLine(result);
}
#endregion

public record Doska(uint WhiteP, uint WhiteD, uint BlackP, uint BlackD)
{
    public IEnumerable<Doska> WhitePVariants()
    {
        foreach (uint position in Indexes(WhiteP))
        {
            Func<uint, uint, uint, (uint step, IKill kill)>[] steps = Masks.P[position];
            foreach (var func in steps)
            {
                (uint step, IKill kill) res = func(position, Blacks, All);
                // получить Doska для ходьбы для текущего step

                // получить Doska для рубки для текущего kill
                uint blackP = BlackP & ~res.kill.DeletedFigure,
                    blackD = BlackD & ~res.kill.DeletedFigure,
                    whiteP = (WhiteP & res.kill.IndexHoda) & ~position;
                yield return new(whiteP, WhiteD, blackP, blackD);
            }
        }
        // TODO: то же самое - для дамок.
    }
    public uint Whites => WhiteP | WhiteD;
    public uint Blacks => BlackP | BlackD;
    public uint All => Whites | Blacks;

    public static IEnumerable<uint> Indexes(uint x)
    {
        while (x > 0)
        {
            var res = x & ~(x - 1);
            yield return res;
            x ^= res;
        }
    }
}
