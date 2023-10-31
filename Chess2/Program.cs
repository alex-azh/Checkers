using System.Numerics;
Test2();
void Test2()
{
    var doska = new Doska(0b0000000000000000000000000001111, 0, 0b11111111111100000000000011000000, 0);
    var joinedAll = string.Join(", ", Doska.Indexes(doska.All).Select(BitOperations.Log2));
    Console.WriteLine("All:\n{0}\n", joinedAll);
    foreach (var d in doska.WhitePVariants())
    {
        var oldPos = BitOperations.Log2(doska.Whites & ~d.Whites);
        var newPos = BitOperations.Log2(d.Whites & ~doska.Whites);
        Console.WriteLine($"Ход: {oldPos}-->{newPos}");
    }

}
void Test1()
{
    var doska = new Doska(0b00000000000000000000111111111111, 0, 0b11111111111100000000000000000000, 0);
    var joinedAll = string.Join(", ", Doska.Indexes(doska.All).Select(BitOperations.Log2));
    Console.WriteLine("All:\n{0}\n", joinedAll);
    foreach (var d in doska.WhitePVariants())
    {
        var oldPos = BitOperations.Log2(doska.Whites & ~d.Whites);
        var newPos = BitOperations.Log2(d.Whites & ~doska.Whites);
        Console.WriteLine($"Ход: {oldPos}-->{newPos}");
    }

}

public record Doska(uint WhiteP, uint WhiteD, uint BlackP, uint BlackD)
{
    public uint Whites => WhiteP | WhiteD;
    public uint Blacks => BlackP | BlackD;
    public uint All => Whites | Blacks;
    public IEnumerable<Doska> WhitePVariants()
    {
        foreach (uint position in Indexes(WhiteP))
        {
            Func<uint, uint, uint, (uint step, IKill kill)>[] steps = Masks.P[position];
            foreach (var func in steps)
            {
                (uint step, IKill kill) = func(position, Blacks, All);
                // получить Doska для ходьбы для текущего step
                if (step != 0)
                {
                    yield return new(
                    WhiteP & ~position | step,
                    WhiteD,
                    BlackP,
                    BlackD);
                }
                if (kill.IsAvailable)
                {
                    // получить Doska для рубки для текущего kill
                    yield return new(
                        WhiteP & ~position | kill.MaskaVstavki,
                        WhiteD,
                        BlackP & ~kill.DeletedFigure,
                        BlackD & ~kill.DeletedFigure);
                }
            }
        }
        foreach (uint position in Indexes(WhiteD))
        {
            Func<uint, uint, uint, (uint step, IKill kill)>[] steps = Masks.P[position];
            foreach (var func in steps)
            {
                (uint step, IKill kill) = func(position, Blacks, All);
                // получить Doska для ходьбы для текущего step
                yield return new(
                    WhiteP,
                    WhiteD & ~position | step,
                    BlackP,
                    BlackD);
                if (kill.IsAvailable)
                {
                    // получить Doska для рубки для текущего kill
                    yield return new(
                        WhiteP,
                        WhiteD & ~position | kill.MaskaVstavki,
                        BlackP & ~kill.DeletedFigure,
                        BlackD & ~kill.DeletedFigure);
                }
            }
        }
    }
    public IEnumerable<Doska> WhiteDVariants()
    {
        foreach (uint position in Indexes(WhiteD))
        {
            Func<uint, uint, uint, (uint step, IKill kill)>[] steps = Masks.P[position];
            foreach (var func in steps)
            {
                (uint step, IKill kill) = func(position, Blacks, All);
                // получить Doska для ходьбы для текущего step
                yield return new(
                    WhiteP,
                    WhiteD & ~position | step,
                    BlackP,
                    BlackD);
                if (kill.IsAvailable)
                {
                    // получить Doska для рубки для текущего kill
                    yield return new(
                        WhiteP,
                        WhiteD & ~position | kill.MaskaVstavki,
                        BlackP & ~kill.DeletedFigure,
                        BlackD & ~kill.DeletedFigure);
                }
            }
        }
    }

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
