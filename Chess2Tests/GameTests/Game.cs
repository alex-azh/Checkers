using CheckersGame;

namespace CheckersTests;

public interface IInputMove
{
    (int fromPosition, int toPosition) Input();
    void SendBadMoveInformation();
}
public interface IPlayer
{
    /// <summary>
    /// Получить доску с ходом от <see cref="IPlayer"/>.
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    Board Move(Board board);
}

public class Game(IPlayer whitesPlayer, IPlayer blacksPlayer)
{
    public IPlayer Whites { get; private set; } = whitesPlayer;
    public IPlayer Blacks { get; private set; } = blacksPlayer;
    public Board CheckersBoard { get; private set; } = Board.NewBoard();
    IEnumerable<Board> Start()
    {
        IPlayer lastPlayer = Whites;
        while (CheckersBoard.Moves2().Any())
        {
            
        }
    }
}
