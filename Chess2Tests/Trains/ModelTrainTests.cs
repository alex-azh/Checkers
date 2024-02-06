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
    public void GameTest()
    {
        var predictor = new ModelPredictor();
        predictor.Load("model_checkers_98_old");
        ComputerPlayer p1 = new(new Evaluater(predictor)), p2 = new(new Evaluater(predictor));
    }
}
