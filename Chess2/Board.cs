using CheckersGame.Figures;
using System.Numerics;

namespace CheckersGame;
public record Board(uint WhiteP, uint WhiteD, uint BlackP, uint BlackD)
{
    public static uint StartWhites => 0b00_00000_00000_00000_00011_11111_11111;
    public static uint StartBlacks => 0b11_11111_11111_00000_00000_00000_00000;
    public static Board NewBoard() => new(StartWhites, 0, StartBlacks, 0);
    public bool ImPlayer { get; set; }
    public uint Whites => WhiteP | WhiteD;
    public uint Blacks => BlackP | BlackD;
    public uint All => Whites | Blacks;

    /// <summary>
    /// Всевозможные ходы от текущего игрока <see cref="ImPlayer"/>.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<(Board board, bool hasKill)> Moves()
    {
        foreach (var result in Checkers.Moves(this))
        {
            yield return result;
        }
        foreach (var result in Queen.Moves(this))
        {
            yield return result;
        }
    }

    public static (int fromPos, int toPos, int deletedPos) WhoMovedWhites(Board prev, Board current) =>
        (BitOperations.Log2(prev.Whites & ~current.Whites),
        BitOperations.Log2(current.Whites & ~prev.Whites),
        BitOperations.Log2(prev.Blacks & ~current.Blacks));
    public static (int fromPos, int toPos, int deletedPos) WhoMovedBlacks(Board prev, Board current) =>
        (BitOperations.Log2(prev.Blacks & ~current.Blacks),
        BitOperations.Log2(current.Blacks & ~prev.Blacks),
        BitOperations.Log2(prev.Whites & ~current.Whites));

    public Board Flip() => new(BlackP.Reverse(), BlackD.Reverse(), WhiteP.Reverse(), WhiteD.Reverse());
    public bool[] BoolArray()
    {
        bool[] result = new bool[128];
        for (int i = 0; i < 32; ++i)
        {
            result[i] = (WhiteP >> i & 0x01) > 0;
            result[i + 32] = (WhiteD >> i & 0x01) > 0;
            result[i + 64] = (BlackP >> i & 0x01) > 0;
            result[i + 96] = (BlackD >> i & 0x01) > 0;
        }
        return result;
    }
}