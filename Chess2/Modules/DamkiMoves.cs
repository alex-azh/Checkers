namespace Chess2;
public static class Queen
{
    public static IEnumerable<CheckersBoard> Moves(CheckersBoard board)
    {
        foreach (uint position in UintHelper.Moves(board.WhiteD))
        {
            #region Forward
            #region steps
            Masks.Steps.TryGetValue(position, out uint variant);
            foreach (uint newPosition in UintHelper.Moves(variant & ~board.All))
            {
                yield return board with
                {
                    WhiteD = board.WhiteD & ~position | newPosition
                };
            }
            #endregion steps

            #region kills
            foreach (uint killedFigurePosition in UintHelper.Moves(variant & board.Blacks))
            {
                Masks.StepsByDirection.TryGetValue(killedFigurePosition | position, out uint stepPos);
                foreach (uint newPosition in UintHelper.Moves(stepPos & ~board.All))
                {
                    yield return board with
                    {
                        WhiteD = board.WhiteD & ~position | newPosition,
                        BlackP = board.BlackP & ~killedFigurePosition,
                        BlackD = board.BlackD & ~killedFigurePosition
                    };
                }
            }
            #endregion kills
            #endregion Forward

            #region Backward
            #region steps
            Masks.StepsBackward.TryGetValue(position, out uint backwardSteps);
            foreach (uint newPosition in UintHelper.Moves(backwardSteps & ~board.All))
            {
                yield return board with
                {
                    WhiteD = board.WhiteD & ~position | newPosition
                };
            }
            #endregion steps

            #region kills
            foreach (uint killedFigurePosition in UintHelper.Moves(backwardSteps & board.Blacks))
            {
                Masks.KillsBackward.TryGetValue(killedFigurePosition | position, out uint stepPos);
                foreach (uint newPosition in UintHelper.Moves(stepPos & ~board.All))
                {
                    yield return board with
                    {
                        WhiteD = board.WhiteD & ~position | newPosition,
                        BlackP = board.BlackP & ~killedFigurePosition,
                        BlackD = board.BlackD & ~killedFigurePosition
                    };
                }
            }
            #endregion kills
            #endregion Backward
        }
    }
}