using System.Numerics;

namespace Chess2;
public record CheckersBoard(uint WhiteP, uint WhiteD, uint BlackP, uint BlackD)
{
    public uint Whites => WhiteP | WhiteD;
    public uint Blacks => BlackP | BlackD;
    public uint All => Whites | Blacks;
    public readonly static Func<CheckersBoard, CheckersBoard, (int pos, int hod)> StepOutput = (prev, next) =>
    {
        int oldPos = BitOperations.Log2(prev.Whites & ~next.Whites);
        int newPos = BitOperations.Log2(next.Whites & ~prev.Whites);
        return (oldPos, newPos);
    };
    public readonly static Func<CheckersBoard, CheckersBoard, (int pos, int hod, int dead)> KillOutput = (prev, next) =>
    {
        int oldPos = BitOperations.Log2(prev.Whites & ~next.Whites);
        int newPos = BitOperations.Log2(next.Whites & ~prev.Whites);
        int dead = BitOperations.Log2(prev.Blacks & ~next.Blacks);
        return (oldPos, newPos, dead);
    };

    public CheckersBoard Reverse() => new(UintHelper.Reverse2(BlackP), UintHelper.Reverse2(BlackD), UintHelper.Reverse2(WhiteP), UintHelper.Reverse2(WhiteD));

    public IEnumerable<CheckersBoard> Variants()
    {
        foreach (var position in UintHelper.Moves(WhiteP))
        {
            var variantsSteps = Masks.Steps[position];
            #region steps
            var steps = variantsSteps & ~All;
            foreach (var step in UintHelper.Moves(steps))
            {
                yield return new(
                    WhiteP & ~position | step,
                    WhiteD,
                    BlackP,
                    BlackD);
            }
            #endregion

            #region kills
            foreach (var kill in UintHelper.Moves(variantsSteps & Blacks))
            {
                var k = kill | position;
                if (Masks.StepsByDirection.TryGetValue(k, out uint hod))
                {
                    hod &= ~All;
                    foreach (var step in UintHelper.Moves(hod))
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
    //[Obsolete]
    //public IEnumerable<CheckerBoard> Variants_old()
    //{
    //    return WhitePVariants_old().Concat(WhitePKills_old());
    //}
    //[Obsolete]
    //public IEnumerable<CheckerBoard> WhitePVariants_old()
    //{
    //    foreach (uint position in UintHelper.Indexes(WhiteP))
    //    {
    //        // ход пешки
    //        uint maska = Masks.Forward[position] & ~All;
    //        foreach (var hod in UintHelper.Indexes(maska))
    //        {
    //            yield return new(
    //                WhiteP & ~position | hod,
    //                WhiteD,
    //                BlackP,
    //                BlackD);
    //        }
    //    }
    //}
    //[Obsolete]
    //public IEnumerable<CheckerBoard> WhitePKills_old()
    //{
    //    foreach (uint position in UintHelper.Indexes(WhiteP))
    //    {
    //        if (Masks.ForwardKill.TryGetValue(position, out uint maskaHodov))
    //        {
    //            maskaHodov &= ~All;
    //            foreach (var hod in UintHelper.Indexes(maskaHodov))
    //            {
    //                int logPos = BitOperations.Log2(position),
    //                    delta = BitOperations.Log2(hod) - logPos,
    //                    ryad = logPos / 4;
    //                Func<uint, int, uint> funcForSearchDeletedPos = Masks.Delta[delta];
    //                uint deletedPosition = funcForSearchDeletedPos(position, ryad);
    //                if ((deletedPosition & Blacks) != 0)
    //                {
    //                    yield return new(
    //                        WhiteP & ~position | hod,
    //                        WhiteD,
    //                        BlackP & ~deletedPosition,
    //                        BlackD & ~deletedPosition
    //                        );
    //                }
    //            }
    //        }
    //    }
    //}
    #endregion
}