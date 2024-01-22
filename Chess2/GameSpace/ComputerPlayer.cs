namespace CheckersGame.GameSpace;


public interface IEvaluater
{
    int CountTakedBestMoves { get; set; }
    float DepthControl { get; set; }
    float[] Evaluate(Board[] moves);
}
public class RandomEvaluater : IEvaluater
{
    private int _cntTakedBestMoves = 3;
    private float _depthFactor = 0.0001f;
    public int CountTakedBestMoves { get => _cntTakedBestMoves; set => _cntTakedBestMoves = value; }
    public float DepthControl { get => _depthFactor; set => _depthFactor = value; }
    public float[] Evaluate(Board[] moves) => moves.Select(m => Random.Shared.NextSingle()).ToArray();
}
public class NeuroEvaluater : IEvaluater
{
    private int _cntTakedBestMoves = 3;
    private float _depthFactor = 0.0001f;

    public int CountTakedBestMoves { get => _cntTakedBestMoves; set => _cntTakedBestMoves = value; }
    public float DepthControl { get => _depthFactor; set => _depthFactor = value; }

    public float[] Evaluate(Board[] moves) => throw new NotImplementedException();
}

public sealed class ComputerPlayer(IEvaluater evaluater) : IPlayer
{
    public IEvaluater Predictor { get; set; } = evaluater;
    public Board Move(Board board)
    {
        return GetBestMove(board, 1).board;
    }
    public (Board board, float mark) GetBestMove(Board board, float depth)
    {
        (Board rBoard, float mark) best = board.GetBestAndRndMoves(Predictor).Select(board =>
        {
            var moves = board.board.Flip().GetBestAndRndMoves(Predictor).ToList();
            var amount = moves.Count;
            //в оригинале было так: (т.е. возвращалось ((board, mark), mark) )
            //return (board, mark: board.board.Flip().GetBestAndRndMoves(countBestMoves)
            return (board.board, mark: moves
            .Select(rBoard => depth > Predictor.DepthControl ? GetBestMove(rBoard.board, depth / amount).mark : rBoard.mark)
            .DefaultIfEmpty(board.board.BlackP == 0 ? -1f : 0)
            .Max());
        })
       .MinBy(p => p.mark);
        //в оригинале:
        //return (best.board.rBoard, best.mark)
        return best;
    }
}
