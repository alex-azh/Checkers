using System.Numerics;

namespace Chess2;
public record CheckerBoard(uint WhiteP, uint WhiteD, uint BlackP, uint BlackD)
{
    public uint Whites => WhiteP | WhiteD;
    public uint Blacks => BlackP | BlackD;
    public uint All => Whites | Blacks;
    public static Func<CheckerBoard, CheckerBoard, (int pos, int hod)> StepOutput = (prev, next) =>
    {
        int oldPos = BitOperations.Log2(prev.Whites & ~next.Whites);
        int newPos = BitOperations.Log2(next.Whites & ~prev.Whites);
        return (oldPos, newPos);
    };
    public static Func<CheckerBoard, CheckerBoard, (int pos, int hod, int dead)> KillOutput = (prev, next) =>
    {
        int oldPos = BitOperations.Log2(prev.Whites & ~next.Whites);
        int newPos = BitOperations.Log2(next.Whites & ~prev.Whites);
        int dead = BitOperations.Log2(prev.Blacks & ~next.Blacks);
        return (oldPos, newPos, dead);
    };
   
    public IEnumerable<CheckerBoard> VariantsP()
    {
        foreach (var position in UintHelper.Indexes(WhiteP))
        {
            var variantsSteps = Masks.Forward[position];
            #region steps
            var steps = variantsSteps & ~All;
            foreach (var step in UintHelper.Indexes(steps))
            {
                yield return new(
                    WhiteP & ~position | step,
                    WhiteD,
                    BlackP,
                    BlackD);
            }
            #endregion

            #region kills
            foreach (var kill in UintHelper.Indexes(variantsSteps & Blacks))
            {
                var k = kill | position;
                if (Masks.ForwardKillStatic.TryGetValue(k, out uint hod))
                {
                    hod &= ~All;
                    foreach (var step in UintHelper.Indexes(hod))
                    {
                        yield return new(
                            WhiteP & ~position | step,
                            WhiteD,
                            BlackP & ~kill,
                            BlackD & ~kill
                            );
                    }
                }
            }
            #endregion
        }
    }
    // TODO: сделать VariantsD();
    #region Obsolete
    [Obsolete]
    public IEnumerable<CheckerBoard> Variants()
    {
        return WhitePVariants().Concat(WhitePKills());
    }
    [Obsolete]
    public (int, int, int) GetHod(int number) => KillOutput(this, Variants().ElementAt(number));
    [Obsolete]
    public IEnumerable<CheckerBoard> WhitePVariants()
    {
        foreach (uint position in UintHelper.Indexes(WhiteP))
        {
            // ход пешки
            uint maska = Masks.Forward[position] & ~All;
            foreach (var hod in UintHelper.Indexes(maska))
            {
                yield return new(
                    WhiteP & ~position | hod,
                    WhiteD,
                    BlackP,
                    BlackD);
            }
        }
    }
    [Obsolete]
    public IEnumerable<CheckerBoard> WhitePKills()
    {
        foreach (uint position in UintHelper.Indexes(WhiteP))
        {
            if (Masks.ForwardKill.TryGetValue(position, out uint maskaHodov))
            {
                maskaHodov &= ~All;
                foreach (var hod in UintHelper.Indexes(maskaHodov))
                {
                    int logPos = BitOperations.Log2(position),
                        delta = BitOperations.Log2(hod) - logPos,
                        ryad = logPos / 4;
                    Func<uint, int, uint> funcForSearchDeletedPos = Masks.Delta[delta];
                    uint deletedPosition = funcForSearchDeletedPos(position, ryad);
                    if ((deletedPosition & Blacks) != 0)
                    {
                        yield return new(
                            WhiteP & ~position | hod,
                            WhiteD,
                            BlackP & ~deletedPosition,
                            BlackD & ~deletedPosition
                            );
                    }
                }
            }
        }
    }
    #endregion
}