namespace Chess2;

public static class CheckersGameExt
{
    public static CheckersBoard BetterMove(this CheckersBoard board)
    {
        // TODO: Метод Variants() должен вернуть неинвертированную доску, но заменить ImPlayer, если не была срублена фигура.
        // TODO: Метод Variants() ничего не возвращает, если !CanContinue (игрок не может ходить, при этом Blacks!=0 или Blacks==0).
        return board.Variants().First();
    }

    /// <summary>
    /// Запускает игру компьютер против компьютера.
    /// </summary>
    /// <param name="board">Доска, с которой будет старт игры.</param>
    /// <returns>Коллекция ходов двух игроков.</returns>
    public static IEnumerable<CheckersBoard> RunGame(CheckersBoard board)
    {
        // TODO: Reverse должен предполагать автоматически изменение текущего игрока
        // TODO: Variants() должен подразумевать, что у противника есть фигуры.
        do
        {
            board = board.BetterMove();
            yield return board;
            board = board.ImPlayer ? board : board.Reverse();
        }
        while (board.Variants().Any());
    }
}
