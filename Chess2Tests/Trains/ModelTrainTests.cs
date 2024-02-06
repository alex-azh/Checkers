using CheckersGame.Evaluaters;
using CheckersGame.GameSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersTests.Trains;

[TestClass]
public class ModelTrainTests
{
    [TestMethod]
    public void Train1()
    {
        var predictor = new ModelPredictor();
        Console.WriteLine(Environment.CurrentDirectory);
        predictor.Train(100, 50, new(0, 0, 1));
    }
    [TestMethod]
    public void TrainWithLoad()
    {
        var predictor = new ModelPredictor();
        predictor.Load("model_checkers_93_2");
        Console.WriteLine(Environment.CurrentDirectory);
        predictor.Train(300, 50, new(0, 0, 10));
    }
    [TestMethod]
    public void GameTest2()
    {
        var predictor = new ModelPredictor();
        //predictor.Load(@"C:\Users\Azhgihin_AA\source\repos\Chess2\Chess2Tests\bin\Release\net8.0\model_checkers_296");
        var eval = new Evaluater(predictor);
        ComputerPlayer p1 = new(eval), p2 = new(eval);
        for (int j = 0; j < 10; j++)
        {
            (List<CheckersGame.Board> boards, List<float> targets, List<float> res) = ModelPredictor.GamesCreator(100, eval);
            predictor.Train((boards, targets), 100, 100, new(23, 0, 0));
            Console.WriteLine($"plus: {res.Count(x => x == 1f)}; minus: {res.Count(x => x == -1f)}; zero: {res.Count(x => x == 0f)}");
        }
    }
}
// Я ОТОШЁЛ