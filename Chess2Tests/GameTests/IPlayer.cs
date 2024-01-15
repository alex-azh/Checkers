using CheckersGame;

namespace CheckersTests;

public interface IPlayer
{
    /// <summary>
    /// Получить доску с ходом от <see cref="IPlayer"/>.
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    (Board board, bool changedPlayer) Move(Board board);
}
