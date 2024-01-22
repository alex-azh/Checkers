namespace CheckersGame.GameSpace;

public interface IPlayer
{
    /// <summary>
    /// Получить доску с ходом от <see cref="IPlayer"/>.
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    Board Move(Board board);
}
