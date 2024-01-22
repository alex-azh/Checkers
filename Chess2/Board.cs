using CheckersGame.Figures;
using CheckersGame.GameSpace;
using System.Numerics;

namespace CheckersGame;
public record Board(uint WhiteP, uint WhiteD, uint BlackP, uint BlackD)
{
    const int countBestMoves = 3;
    const float depthFactor = 0.0001f;
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
    public IEnumerable<Board> Moves()
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
    public IEnumerable<(Board board, float mark)> GetBestAndRndMoves(IEvaluater evaluater)
    {
        SortedSet<(Board board, float mark, float rnd)> bestMoves = new(Comparer<(Board, float, float)>.Create((x, y) => x.Item2.CompareTo(y.Item2)));
        SortedSet<(Board board, float mark, float rnd)> rndMoves = new(Comparer<(Board, float, float)>.Create((x, y) => x.Item3.CompareTo(y.Item3)));

        IEnumerable<(Board Board, float Mark)> moves = Moves().Chunk(100).SelectMany(chunk => chunk.Zip(evaluater.Evaluate(chunk)));
        //в оригинале было так: (т.к. Moves() просто коллекцию досок возвращал)
        //var moves = Moves().Chunk(100).SelectMany(chunk => chunk.Zip(Evaluate(chunk)));

        foreach (var move in moves)
        {
            var rnd = Random.Shared.NextSingle();
            if (bestMoves.Count < evaluater.CountTakedBestMoves)
                bestMoves.Add((move.Board, move.Mark, rnd));
            else if (move.Mark > bestMoves.Min.mark)
            {
                bestMoves.Remove(bestMoves.Min);
                bestMoves.Add((move.Board, move.Mark, rnd));
            }

            if (rndMoves.Count < evaluater.CountTakedBestMoves + 1)
                rndMoves.Add((move.Board, move.Mark, rnd));
            else if (rnd > rndMoves.Min.rnd)
            {
                rndMoves.Remove(rndMoves.Min);
                rndMoves.Add((move.Board, move.Mark, rnd));
            }
        }
        rndMoves.ExceptWith(bestMoves);
        return bestMoves.Select(m => (m.board, m.mark)).Concat(rndMoves.Select(m => (m.board, m.mark)).Take(1));
    }
}