
namespace CheckersGame.GameSpace;

public record struct HumanPlayer(string Name = "Person") : IPlayer
{
    public List<Board> Moves { get => throw new NotImplementedException(); init => throw new NotImplementedException(); }

    public Board Move(Board board)
    {
        throw new NotImplementedException();
    }
}
