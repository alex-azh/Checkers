namespace CheckersGame.GameSpace;

public sealed class ComputerPlayer : IPlayer
{
    public (Board board, bool changedPlayer) Move(Board board)
    {
        List<(Board board, bool hasKill)> list = board.Moves().ToList();
        return list[new Random().Next(list.Count)];
    }
}
