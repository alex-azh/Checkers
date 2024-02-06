using CheckersGame.Evaluaters;
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
        predictor.Train(2, 5, new(0, 0, 10));
    }
}
