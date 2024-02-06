using CheckersGame.Figures;
using System.Numerics;

namespace CheckersGame;
public record Board(uint WhiteP, uint WhiteD, uint BlackP, uint BlackD)
{
    public Board(int[] whiteP, int[] whiteD, int[] blackP, int[] blackD) : this(
        UintHelper.CreateNumber(whiteP),
        UintHelper.CreateNumber(whiteD),
        UintHelper.CreateNumber(blackP),
        UintHelper.CreateNumber(blackD))
    { }
    public static Board NewBoard() => new(0b00_00000_00000_00000_00011_11111_11111, 0, 0b11_11111_11111_00000_00000_00000_00000, 0);
    public uint Whites => WhiteP | WhiteD;
    public uint Blacks => BlackP | BlackD;
    public uint All => Whites | Blacks;
    public float Mark { get; set; }

    public IEnumerable<Board> Moves()
    {
        if (Checkers.Kills(this).Any() || Queen.Kills(this).Any())
        {
            foreach (Board killBoard in Checkers.Kills(this).Concat(Queen.Kills(this)))
            {
                // по каждой рубке надо получить всевозможные ситуации
                Stack<Board> stack = [];
                stack.Push(killBoard);
                Board prev = this;
                //int lastPos = WhoMovedWhites(this, killBoard).toPos;
                while (stack.Count > 0)
                {
                    Board poped = stack.Pop();
                    IEnumerable<Board> killsBoardsFromLastPos =
                        // concat с Queens потому что пешка могла стать дамкой
                        from k in Checkers.Kills(poped).Concat(Queen.Kills(poped))
                            //where WhoMovedWhites(poped, k).fromPos == WhoMovedWhites(prev, poped).fromPos
                        where WhoMovedWhites(poped, k).fromPos == WhoMovedWhites(prev, poped).toPos
                        select k;
                    if (killsBoardsFromLastPos.Any())
                    {
                        foreach (var k in killsBoardsFromLastPos)
                        {
                            stack.Push(k);
                        }
                        prev = poped;
                    }
                    else
                    {
                        yield return poped;
                    }
                }
            }
        }
        else
        {
            foreach (Board b in Checkers.Moves(this))
            {
                yield return b;
            }
            foreach (Board b in Queen.Moves(this))
            {
                yield return b;
            }
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
    public float[] FloatArray()
    {
        float[] result = new float[128];
        for (int i = 0; i < 32; ++i)
        {
            result[i] = WhiteP >> i & 0x01;
            result[i + 32] = WhiteD >> i & 0x01;
            result[i + 64] = BlackP >> i & 0x01;
            result[i + 96] = BlackD >> i & 0x01;
        }
        return result;
    }
}