namespace CheckersGame.GameSpace;


public class Game(IPlayer whitePlayer, IPlayer blackPlayer)
{
    private readonly IDictionary<IPlayer, IPlayer> Opponents = new Dictionary<IPlayer, IPlayer>
    {
        { whitePlayer, blackPlayer }, { blackPlayer, whitePlayer }
    };
    public Board CheckersBoard { get; private set; } = Board.NewBoard();
    public ushort MovesWhithoutKillsCount { get; set; } = 0;
    public bool GameContinue => MovesWhithoutKillsCount < 50 && CheckersBoard.Moves().Any();
    public IPlayer LastPlayer { get; private set; } = whitePlayer;
    public bool Reversed { get; private set; } = false;
    public IEnumerable<Board> Start(IDisplayBoard display)
    {
        display.Show(CheckersBoard);
        while (GameContinue)
        {
            (Board board, bool wasDeletedFigure) = LastPlayer.Move(CheckersBoard);
            display.Show(CheckersBoard);
            yield return board;
            if (wasDeletedFigure)
            {
                CheckersBoard = board;
                MovesWhithoutKillsCount = 0;
            }
            else
            {
                CheckersBoard = board.Flip();
                Reversed = true;
                LastPlayer = Opponents[LastPlayer];
                MovesWhithoutKillsCount++;
            }
        }
    }
    public IEnumerable<(Board board, bool reversed)> Start()
    {
        while (GameContinue)
        {
            var move = CheckersBoard.GetBestMove(0.001f).board;
            //(Board board, bool wasDeletedFigure) = LastPlayer.Move(CheckersBoard);
            yield return (move, Reversed);
            if (move.Blacks != CheckersBoard.Blacks)
            {
                CheckersBoard = move;
                MovesWhithoutKillsCount = 0;
                Reversed = false | Reversed;
            }
            else
            {
                CheckersBoard = move.Flip();
                Reversed = !Reversed;
                LastPlayer = Opponents[LastPlayer];
                MovesWhithoutKillsCount++;
            }
        }
    }
}
