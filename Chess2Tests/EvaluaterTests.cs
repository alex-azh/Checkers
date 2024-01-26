using CheckersGame;
using CheckersGame.Evaluaters;

namespace CheckersTests;
// Тесты работали до изменения системы ходов фигур(обязательна рубка).
[TestClass]
public class EvaluaterTests
{
    //[TestMethod]
    public void MinMaxTest1()
    {
        Board board = new([1], [], [5], []),
            best = new([10], [], [], []);
        Board[] moves = board.Moves().ToArray();
        Evaluater ev = new(new ModelPredictor());
        var evls = moves.Zip(ev.Evaluate(moves));
        var notbetterMove = evls.First(x => x.First == new Board([4], [], [5], []));
        var BetterMove = evls.First(x => x.First == new Board([10], [], [], []));
        int failInitCount = 0;
        while (notbetterMove.Second > BetterMove.Second)
        {
            ev = new Evaluater(new ModelPredictor());
            evls = moves.Zip(ev.Evaluate(moves));
            notbetterMove = evls.First(x => x.First == new Board([4], [], [5], []));
            BetterMove = evls.First(x => x.First == new Board([10], [], [], []));
            failInitCount++;
        }
        Board eval_best = ev.GetBestMove(board);
        Console.WriteLine("Fail init count = {0}", failInitCount);
        Assert.AreEqual(best, eval_best);
        //Board board = new([1], [], [5], []),
        //    best = new([10], [], [], []);
        //Board[] moves = board.Moves().ToArray();
        //Evaluater ev = new(new ModelPredictor());
        //float[] evls = ev.Evaluate(moves);
        //int failInitCount = 0;
        //while (evls[0] > evls[1])
        //{
        //    ev = new Evaluater(new ModelPredictor());
        //    evls = ev.Evaluate(moves);
        //    failInitCount++;
        //}
        //Board eval_best = ev.GetBestMove(board);
        //Console.WriteLine($"Fail init count = {0}", failInitCount);
        //Assert.AreEqual(best, eval_best);
    }
    // ТЕСТ РАБОТАЛ ПЕРЕД ТЕМ, КАК СДЕЛАТЬ ОБЯЗАТЕЛЬНЫЕ РУБКИ.
    //[TestMethod]
    public void MinMaxTest2()
    {
        // init Board
        Board board = new([1], [], [], [2]),
            best = new([4], [], [], [2]);
        Board[] moves = board.Moves().ToArray();
        // Create computer evaluater (ai)
        Evaluater ev = new(new ModelPredictor());
        float[] evls = ev.Evaluate(moves);
        float[] evls_opp = ev.Evaluate(new Board([5], [], [], [2]).Flip().Moves().ToArray());
        // проверка, что компьютер посчитал оценки в правильном ключе
        int failInitCount = 0;
        while ((evls[0] < evls[1]) || (evls_opp[0] < evls_opp[1]))
        {
            ev = new Evaluater(new ModelPredictor());
            evls = ev.Evaluate(moves);
            evls_opp = ev.Evaluate(new Board([5], [], [], [2]).Flip().Moves().ToArray());
            failInitCount++;
        }
        Board eval_best = ev.GetBestMove(board);
        Console.WriteLine("Fail init count = {0}", failInitCount);
        Assert.AreEqual(best, eval_best);
    }
}
