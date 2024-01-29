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
    public List<Board> Start()
    {
        List<Board> list = [];
        while (GameContinue)
        {
            Board move = LastPlayer.Move(CheckersBoard);
            list.Add(move);
            // игрок и доска всегда переворачиваются, потому что рубка обязательна.
            LastPlayer = Opponents[LastPlayer];
            CheckersBoard = move.Flip();
            // если кого-то срубили
            if (move.Blacks != CheckersBoard.Blacks)
                MovesWhithoutKillsCount = 0;
            else
                MovesWhithoutKillsCount++;
        }
        return list;
    }
}
