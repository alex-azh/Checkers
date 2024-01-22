using CheckersGame.Evaluaters;

namespace CheckersGame.GameSpace;

public sealed class ComputerPlayer(Evaluater evaluater) : IPlayer
{
    public ComputerPlayer() : this(new Evaluater(new RandomPredictor())) { }
    public Evaluater Evaluater { get; set; } = evaluater;
    public Board Move(Board board) => Evaluater.GetBestMove(board, 1).board;
}
