using CheckersGame.Figures;
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

    public (Board board, float mark) GetBestMove(float depth)
    {
        (Board rBoard, float mark) best = GetBestAndRndMoves(countBestMoves).Select(board =>
        {
            var moves = board.board.Flip().GetBestAndRndMoves(countBestMoves).ToList();
            var amount = moves.Count;
            //в оригинале было так: (т.е. возвращалось ((board, mark), mark) )
            //return (board, mark: board.board.Flip().GetBestAndRndMoves(countBestMoves)
            return (board.board, mark: moves
            .Select(rBoard => depth > depthFactor ? rBoard.board.GetBestMove(depth / amount).mark : rBoard.mark)
            .DefaultIfEmpty(board.board.BlackP == 0 ? -1f : 0)
            .Max());
        })
            .MinBy(p => p.mark);
        //в оригинале:
        //return (best.board.rBoard, best.mark)
        return best;
    }


    IEnumerable<(Board board, float mark)> GetBestAndRndMoves(int count)
    {
        /*
         * у нас Moves() возвращает кортеж (board, hasKill)
         * Саша, я так понимаю, что нам этот hasKill нужен, чтобы понимать, продолжит ли ход игрок
         * но мы же можем уже после выбора лучшего хода определить, было ли убийство просто сравнив исходное положение противника
         * и его положение в выбранном лучшем ходе, тогда нам не нужно это тут таскать
        */
        SortedSet<(Board board, float mark, float rnd)> bestMoves = new(Comparer<(Board, float, float)>.Create((x,y) => x.Item2.CompareTo(y.Item2)));
        SortedSet<(Board board, float mark, float rnd)> rndMoves = new(Comparer<(Board, float, float)>.Create((x, y) => x.Item3.CompareTo(y.Item3)));

        var moves = Moves().Chunk(100).SelectMany(chunk => (chunk.Select(c => c.board).Zip(Evaluate(chunk.Select(c => c.board).ToArray()))));
        //в оригинале было так: (т.к. Moves() просто коллекцию досок возвращал)
        //var moves = Moves().Chunk(100).SelectMany(chunk => chunk.Zip(Evaluate(chunk)));

        foreach (var move in moves)
        {
            var rnd = Random.Shared.NextSingle();
            if (bestMoves.Count < count)
                bestMoves.Add((move.First, move.Second, rnd));
            else if(move.Second > bestMoves.Min.mark)
            {
                bestMoves.Remove(bestMoves.Min);
                bestMoves.Add((move.First, move.Second, rnd));
            }
            
            if (rndMoves.Count < count + 1)
                rndMoves.Add((move.First, move.Second, rnd));
            else if (rnd > rndMoves.Min.rnd)
            {
                rndMoves.Remove(rndMoves.Min);
                rndMoves.Add((move.First, move.Second, rnd));
            }
        }
        rndMoves.ExceptWith(bestMoves);
        return bestMoves.Select(m => (m.board,m.mark)).Concat(rndMoves.Select(m => (m.board, m.mark)).Take(1));
    }

    float[] Evaluate(Board[] moves)
    {
        //здесь вычисление оценок ходов (нейросеть)
        return moves.Select(m => Random.Shared.NextSingle()).ToArray(); //пока массив рандомных оценок
    }
}