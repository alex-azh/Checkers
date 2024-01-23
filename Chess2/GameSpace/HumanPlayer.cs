namespace CheckersGame.GameSpace;

public record struct HumanPlayer(string Name = "Person") : IPlayer
{
    public Board Move(Board board)
    {
        throw new NotImplementedException();
    }
}
