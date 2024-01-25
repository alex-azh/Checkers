namespace CheckersGame.Evaluaters;

/// <summary>
/// Оценщик ходов доски <see cref="Board"/>. Применяется для выбора лучшего хода для текущего состояния доски.
/// </summary>
/// <param name="modelPredictor">Модель для оценки числовых последовательностей, получаемых из доски.</param>
/// <param name="depth">Глубина ходов, на которые смотрит оценщик.</param>
/// <param name="countTakedBestMoves">Количество лучших ходов для отбора + 1 случайный.</param>
public class Evaluater(IPredictor modelPredictor, int depth, int countTakedBestMoves)
{
    /// <summary>
    /// Конструктор по умолчанию с указанными <see cref="Depth"/> = 2 b <see cref="CountTakedBestMoves"/> = 3.
    /// </summary>
    /// <param name="modelPredictor">Модель для оценки числовых последовательностей, получаемых из доски.</param>
    public Evaluater(IPredictor modelPredictor) : this(modelPredictor, 2, 3) { }

    /// <summary>
    /// Модель для оценки числовых последовательностей, получаемых из доски.
    /// </summary>
    IPredictor Model { get; init; } = modelPredictor;

    /// <summary>
    /// Количество лучших ходов для отбора + 1 случайный.
    /// </summary>
    public int CountTakedBestMoves { get; set; } = countTakedBestMoves;

    /// <summary>
    /// Глубина ходов, на которые смотрит оценщик.
    /// </summary>
    public int Depth { get; set; } = depth;

    /// <summary>
    /// Получить оценки для заданных досок.
    /// </summary>
    /// <param name="moves">Доски для оценки.</param>
    /// <returns>Оценки, соответствующие каждым доскам.</returns>
    public float[] Evaluate(Board[] moves) => Model.Predict(moves.Select(x => x.BoolArray()).ToArray());

    /// <summary>
    /// Получить лучший ход с текущего состояния доски.
    /// </summary>
    /// <param name="board">Доска, с которой требуется получить лучший ход.</param>
    /// <returns>Доска после лучшего хода.</returns>
    public Board GetBestMove(Board board) => GetBestMove(board, Depth).board;

    /// <summary>
    /// Получить лучший ход с текущего состояния доски и его оценку.
    /// </summary>
    /// <param name="board">Доска, с которой требуется получить лучший ход.</param>
    /// <param name="depthSearch">Глубина просмотра ходов.</param>
    /// <returns>Доска после лучшего хода и её оценка.</returns>
    public (Board board, float mark) GetBestMove(Board board, float depthSearch)
    {
     //   (Board rBoard, float mark) best = GetBestAndRndMoves(board).Select(board =>
     //   {
     //       SortedSet<(Board board, float mark, float)> moves = GetBestAndRndMoves(board.board.Flip());
     //       return (board.board, mark: moves
     //       .Select(rBoard => depthSearch > 0 && rBoard.board.Moves().Any()
     //       ? GetBestMove(rBoard.board, depthSearch - 1).mark : rBoard.mark)
     //       .DefaultIfEmpty(board.board.BlackP == 0 ? -1f : 0)
     //       .Max());
     //   })
     //.MinBy(p => p.mark);
     //   return best;
        (Board rBoard, float mark) best = GetBestAndRndMoves(board).Select(board =>
        {
            //TODO: сделать другую логику.
            var flipped = board.board.Flip();
            return (depthSearch > 0, flipped.Moves().Any()) switch
            {
                (false, true) => (board.board, board.mark),
                (true, true) => (board.board, mark: GetBestMove(flipped, depthSearch - 1).mark),
                (_, false) => (board.board, mark: flipped.Whites == 0 ? 1f : 0)
            };

            //SortedSet<(Board board, float mark, float)> moves = GetBestAndRndMoves(board.board.Flip());
            //return (board.board, mark: moves
            //.Select(rBoard => depthSearch > 0 && rBoard.board.Moves().Any()
            //? GetBestMove(rBoard.board, depthSearch - 1).mark : rBoard.mark)
            //.DefaultIfEmpty(board.board.BlackP == 0 ? -1f : 0)
            //.Max());
        })
       .MaxBy(p => p.mark);
        return (best.rBoard, -best.mark);
    }

    SortedSet<(Board board, float mark, float)> GetBestAndRndMoves(Board board)
    {
        SortedSet<(Board board, float mark, float rnd)> bestMoves = new(Comparer<(Board, float, float)>.Create((x, y) => x.Item2.CompareTo(y.Item2)));
        SortedSet<(Board board, float mark, float rnd)> rndMoves = new(Comparer<(Board, float, float)>.Create((x, y) => x.Item3.CompareTo(y.Item3)));
        IEnumerable<(Board Board, float Mark)> moves = board.Moves().Chunk(100).SelectMany(chunk => chunk.Zip(Evaluate(chunk)));

        foreach ((Board Board, float Mark) move in moves)
        {
            float rnd = Random.Shared.NextSingle();
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
        bestMoves.UnionWith(rndMoves.Take(1));
        return bestMoves;
    }
}
