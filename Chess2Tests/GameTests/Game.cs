using CheckersGame;

namespace CheckersTests;

public interface IInputMove
{
    (int fromPosition, int toPosition) Input();
    void SendBadMoveInformation();
}

public sealed class ComputerPlayer : IPlayer
{
    public (Board board, bool changedPlayer) Move(Board board)
    {

        var list = board.Moves().ToList();
        return list[new Random().Next(list.Count)];
    }
}

public class Game(IPlayer whitePlayer, IPlayer blackPlayer)
{
    private readonly IDictionary<IPlayer, IPlayer> Opponents = new Dictionary<IPlayer, IPlayer>
    {
        { whitePlayer, blackPlayer }, { blackPlayer, whitePlayer }
    };
    public delegate void DisplayBoardHandler(Board board);
    private DisplayBoardHandler? _display;
    public void RegisterDisplayHandler(DisplayBoardHandler handler)
    {
        _display += handler;
    }
    public void UnRegisterDisplayHandler(DisplayBoardHandler handler) => _display -= handler;
    public Board CheckersBoard { get; private set; } = Board.NewBoard();
    public ushort MovesWhithoutKillsCount { get; set; } = 0;
    public bool GameContinue => MovesWhithoutKillsCount < 50 && CheckersBoard.Moves().Any();
    public IPlayer LastPlayer { get; private set; } = whitePlayer;
    public bool Reversed { get; private set; } = false;
    public IEnumerable<Board> Start()
    {
        _display?.Invoke(CheckersBoard);
        while (GameContinue)
        {
            (Board board, bool wasDeletedFigure) = LastPlayer.Move(CheckersBoard);
            _display?.Invoke(CheckersBoard);
            yield return board;
            if (wasDeletedFigure)
            {
                CheckersBoard = board;
                MovesWhithoutKillsCount = 0;
            }
            else
            {
                CheckersBoard = board.Reverse();
                Reversed = true;
                LastPlayer = Opponents[LastPlayer];
                MovesWhithoutKillsCount++;
            }
        }
    }
}
