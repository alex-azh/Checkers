using System.Numerics;

public record Doska(uint WhiteP, uint WhiteD, uint BlackP, uint BlackD)
{
    public uint Whites => WhiteP | WhiteD;
    public uint Blacks => BlackP | BlackD;
    public uint All => Whites | Blacks;
    public IEnumerable<Doska> WhitePVariants()
    {
        foreach (uint position in Indexes(WhiteP))
        {
            // ход пешки
            uint maska = Masks.Forward[position] & ~All;
            foreach (var hod in Indexes(maska))
            {
                yield return new(
                    WhiteP & ~position | hod,
                    WhiteD,
                    BlackP,
                    BlackD);
            }
        }
    }
    public IEnumerable<Doska> WhitePKillVariants()
    {
        foreach (uint position in Indexes(WhiteP))
        {
            // рубит пешка
            if (Masks.ForwardKillStruct.TryGetValue(position, out IHod? maska))
            {
                foreach ((uint hod, uint kill) in maska.Variants())
                {
                    if ((kill & Blacks) != 0 && (hod & ~All) != 0)
                    {
                        yield return new(
                            WhiteP & ~position | hod,
                            WhiteD,
                            BlackP & ~kill,
                            BlackD & ~kill);
                    }
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
