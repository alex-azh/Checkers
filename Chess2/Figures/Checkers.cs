namespace CheckersGame.Figures;

public static class Checkers
{
    public static IEnumerable<Board> Kills(Board board)
    {
        foreach (uint position in board.WhiteP.ExtractOnes())
        {
            foreach (uint kill in (Masks.Steps[position] & board.Blacks).ExtractOnes())
            {
                if (Masks.StepsByDirection.TryGetValue(kill | position, out uint hod))
                {
                    foreach (uint step in (hod & ~board.All).ExtractOnes())
                    {
                        yield return new(
                            board.WhiteP & ~position | (~Masks.QueenPositions & step),
                            board.WhiteD | (step & Masks.QueenPositions),
                            board.BlackP & ~kill,
                            board.BlackD & ~kill);
                    }
                }
            }
        }
    }

    public static IEnumerable<Board> Moves(Board board)
    {
        foreach (uint position in board.WhiteP.ExtractOnes())
        {
            foreach (uint step in (Masks.Steps[position] & ~board.All).ExtractOnes())
            {
                yield return board with
                {
                    WhiteP = (board.WhiteP & ~position) | (~Masks.QueenPositions & step),
                    WhiteD = board.WhiteD | (step & Masks.QueenPositions)
                };
            }
        }
    }
}
