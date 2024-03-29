﻿namespace CheckersGame.Figures;

public static class Queen
{
    public static IEnumerable<Board> Kills(Board board)
    {
        foreach (uint position in board.WhiteD.ExtractOnes())
        {
            Masks.Steps.TryGetValue(position, out uint variant);
            foreach (uint killedFigurePosition in (variant & board.Blacks).ExtractOnes())
            {
                Masks.StepsByDirection.TryGetValue(killedFigurePosition | position, out uint stepPos);
                foreach (uint newPosition in (stepPos & ~board.All).ExtractOnes())
                {
                    yield return board with
                    {
                        WhiteD = board.WhiteD & ~position | newPosition,
                        BlackP = board.BlackP & ~killedFigurePosition,
                        BlackD = board.BlackD & ~killedFigurePosition
                    };
                }
            }
            Masks.StepsBackward.TryGetValue(position, out uint backwardSteps);
            foreach (uint killedFigurePosition in (backwardSteps & board.Blacks).ExtractOnes())
            {
                Masks.KillsBackward.TryGetValue(killedFigurePosition | position, out uint stepPos);
                foreach (uint newPosition in (stepPos & ~board.All).ExtractOnes())
                {
                    yield return board with
                    {
                        WhiteD = board.WhiteD & ~position | newPosition,
                        BlackP = board.BlackP & ~killedFigurePosition,
                        BlackD = board.BlackD & ~killedFigurePosition
                    };
                }
            }
        }
    }


    public static IEnumerable<Board> Moves(Board board)
    {
        foreach (uint position in board.WhiteD.ExtractOnes())
        {
            Masks.Steps.TryGetValue(position, out uint variant);
            foreach (uint newPosition in (variant & ~board.All).ExtractOnes())
            {
                yield return board with
                {
                    WhiteD = board.WhiteD & ~position | newPosition
                };
            }
            Masks.StepsBackward.TryGetValue(position, out uint backwardSteps);
            foreach (uint newPosition in (backwardSteps & ~board.All).ExtractOnes())
            {
                yield return board with
                {
                    WhiteD = board.WhiteD & ~position | newPosition
                };
            }
        }
    }
}
