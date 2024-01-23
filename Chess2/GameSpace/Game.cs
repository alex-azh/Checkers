namespace CheckersGame.GameSpace;

public sealed class Game(IPlayer whitePlayer, IPlayer blackPlayer)
{
    private readonly IDictionary<IPlayer, IPlayer> Opponents = new Dictionary<IPlayer, IPlayer>
    {
        { whitePlayer, blackPlayer }, { blackPlayer, whitePlayer }
    };
    public Board CheckersBoard { get; set; } = Board.NewBoard();
    public ushort MovesWhithoutKillsCount { get; set; } = 0;
    public bool GameContinue => MovesWhithoutKillsCount < 50 && CheckersBoard.Moves().Any();
    public IPlayer LastPlayer { get; private set; } = whitePlayer;
    public bool Reversed { get; private set; } = false;
    public List<(Board board, bool reversed)> Start()
    {
        List<(Board, bool)> list = [];
        while (GameContinue)
        {
            Board move = LastPlayer.Move(CheckersBoard);
            list.Add((move, Reversed));
            //yield return (move, Reversed);
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
        return list;
    }
}
