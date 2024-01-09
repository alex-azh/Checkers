using System.Numerics;

namespace Chess2;
public record CheckersBoard(uint WhiteP, uint WhiteD, uint BlackP, uint BlackD)
{
    public static readonly uint MaskaD = UintHelper.CreateNumber(31, 30, 29, 28);
    public bool ImPlayer { get; set; }
    public uint Whites => WhiteP | WhiteD;
    public uint Blacks => BlackP | BlackD;
    public uint All => Whites | Blacks;
    public bool ImWin => Whites != 0 && Blacks == 0;
    public bool CanContinue => Whites != 0 && Blacks != 0;
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

    public CheckersBoard Reverse() => new(UintHelper.Reverse(BlackP), UintHelper.Reverse(BlackD), UintHelper.Reverse(WhiteP), UintHelper.Reverse(WhiteD));
    public bool[] BoolArray() => UintHelper.GetBoolArray(this);
    public IEnumerable<CheckersBoard> Variants()
    {
        foreach (var position in UintHelper.Moves(WhiteP))
        {
            var variantsSteps = Masks.Steps[position];
            #region steps 
            // variantsSteps & ~All чтобы не было другой 
            foreach (var step in UintHelper.Moves(variantsSteps & ~All))
            {
                yield return new(
                    WhiteP & ~position | (step & MaskaD),
                    WhiteD | (step & MaskaD),
                    BlackP,
                    BlackD);
            }
            #endregion

            #region kills
            foreach (var kill in UintHelper.Moves(variantsSteps & Blacks))
            {
                if (Masks.StepsByDirection.TryGetValue(kill | position, out uint hod))
                {
                    foreach (var step in UintHelper.Moves(hod & ~All))
                    {
                        yield return new(
                            WhiteP & ~position | (step & MaskaD),
                            WhiteD | (step & MaskaD),
                            BlackP & ~kill,
                            BlackD & ~kill
                            );
                    }
                }
            }
            #endregion
        }
        // TODO: сделать ходы дамок
    }
}