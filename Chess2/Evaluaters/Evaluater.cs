namespace CheckersGame.Evaluaters;

public class Evaluater(IPredictor model)
{
    IPredictor Model { get; init; } = model;
    public int CountTakedBestMoves { get; set; } = 3;
    public float DepthControl { get; set; } = 0.0001f;
    public float[] Evaluate(Board[] moves) => Model.Predict(moves.Select(x => x.BoolArray()).ToArray());

    public (Board board, float mark) GetBestMove(Board board, float depthSearch)
    {
        (Board rBoard, float mark) best = GetBestAndRndMoves(board).Select(board =>
        {
            var moves = GetBestAndRndMoves(board.board.Flip()).ToList();
            var amount = moves.Count;
            //в оригинале было так: (т.е. возвращалось ((board, mark), mark) )
            //return (board, mark: board.board.Flip().GetBestAndRndMoves(countBestMoves)
            return (board.board, mark: moves
            .Select(rBoard => depthSearch > DepthControl ? GetBestMove(rBoard.board, depthSearch / amount).mark : rBoard.mark)
            .DefaultIfEmpty(board.board.BlackP == 0 ? -1f : 0)
            .Max());
        })
       .MinBy(p => p.mark);
        //в оригинале:
        //return (best.board.rBoard, best.mark)
        return best;
    }
    IEnumerable<(Board board, float mark)> GetBestAndRndMoves(Board board)
    {
        SortedSet<(Board board, float mark, float rnd)> bestMoves = new(Comparer<(Board, float, float)>.Create((x, y) => x.Item2.CompareTo(y.Item2)));
        SortedSet<(Board board, float mark, float rnd)> rndMoves = new(Comparer<(Board, float, float)>.Create((x, y) => x.Item3.CompareTo(y.Item3)));

        IEnumerable<(Board Board, float Mark)> moves = board.Moves().Chunk(100).SelectMany(chunk => chunk.Zip(Evaluate(chunk)));
        //в оригинале было так: (т.к. Moves() просто коллекцию досок возвращал)
        //var moves = Moves().Chunk(100).SelectMany(chunk => chunk.Zip(Evaluate(chunk)));

        foreach (var move in moves)
        {
            var rnd = Random.Shared.NextSingle();
            if (bestMoves.Count < CountTakedBestMoves)
                bestMoves.Add((move.Board, move.Mark, rnd));
            else if (move.Mark > bestMoves.Min.mark)
            {
                bestMoves.Remove(bestMoves.Min);
                bestMoves.Add((move.Board, move.Mark, rnd));
            }

            if (rndMoves.Count < CountTakedBestMoves + 1)
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
