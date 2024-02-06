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
    public float Start()
    {
        while (GameContinue)
        {
            Board move = LastPlayer.Move(CheckersBoard);
            LastPlayer.Moves.Add(move);
            // игрок и доска всегда переворачиваются, потому что рубка обязательна.
            LastPlayer = Opponents[LastPlayer];
            // если кого-то срубили
            if (move.Blacks != CheckersBoard.Blacks)
                MovesWhithoutKillsCount = 0;
            else
                MovesWhithoutKillsCount++;
            CheckersBoard = move.Flip();
        }

        if (MovesWhithoutKillsCount == 50)
            return 0f;
        else if (LastPlayer == Opponents.Keys.First())
            return -1f;
        else
            return 1f;
    }
}
