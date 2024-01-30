using CheckersGame.Evaluaters;

namespace CheckersGame.GameSpace;

public record ComputerPlayer(Evaluater Evaluater, string Name = "Computer") : IPlayer
{
    public ComputerPlayer() : this(new Evaluater(new RandomPredictor())) { }
    public Evaluater Evaluater { get; set; } = Evaluater;
    public List<Board> Moves { get; init; } = [];

    public Board Move(Board board) => Evaluater.GetBestMove(board);
}
