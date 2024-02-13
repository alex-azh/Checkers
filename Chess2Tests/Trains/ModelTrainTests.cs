﻿using CheckersGame.Evaluaters;
using CheckersGame.GameSpace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchSharp.Modules;
using static TorchSharp.torch;
using static TorchSharp.torch.nn;
using static TorchSharp.torch.optim;

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
    public void CudaTest()
    {
        var cuda = TorchSharp.torch.cuda_is_available();
        Assert.AreEqual(true, cuda);
        float[] arr = [1, 2, 3];
        Tensor t = TorchSharp.torch.from_array(arr);
        var t2 = t.to("cuda", true, true);
        Console.WriteLine(t2.device_type);
    }
    [TestMethod]
    public void CpuMultiTest()
    {
        Tensor t1 = TorchSharp.torch.randn(1024, 1024), t2 = TorchSharp.torch.randn(1024, 1024);
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            var t = TorchSharp.torch.mm(t1, t2);
            _ = t.data<float>().ToArray();
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
    [TestMethod]
    public void CpuMultiTestWithoutData()
    {
        Tensor t1 = TorchSharp.torch.randn(1024, 1024), t2 = TorchSharp.torch.randn(1024, 1024);
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            var t = TorchSharp.torch.mm(t1, t2);
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
    [TestMethod]
    public void CudaMultiTest()
    {
        Tensor t1 = TorchSharp.torch.randn(1024, 1024).to("cuda"), t2 = TorchSharp.torch.randn(1024, 1024).to("cuda");
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            var t = TorchSharp.torch.mm(t1, t2);
            _ = t.data<float>().ToArray();
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
    [TestMethod]
    public void CudaMultiTestWithoutData()
    {
        Tensor t1 = TorchSharp.torch.randn(1024, 1024).to("cuda"), t2 = TorchSharp.torch.randn(1024, 1024).to("cuda");
        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < 1000; i++)
        {
            var t = TorchSharp.torch.mm(t1, t2);
        }
        sw.Stop();
        Console.WriteLine(sw.Elapsed);
    }
    [TestMethod]
    public void GameTest2()
    {
        var predictor = new ModelPredictor();
        //predictor.Load(@"C:\Users\Azhgihin_AA\source\repos\Chess2\Chess2Tests\bin\Release\net8.0\model_checkers_296");
        var eval = new Evaluater(predictor);
        ComputerPlayer p1 = new(eval), p2 = new(eval);
        for (int j = 0; j < 2; j++)
        {
            (List<CheckersGame.Board> boards, List<float> targets, List<float> res) = ModelPredictor.GamesCreator(100, eval);
            predictor.Train((boards, targets), 100, 100, new(23, 0, 0));
            Console.WriteLine($"plus: {res.Count(x => x == 1f)}; minus: {res.Count(x => x == -1f)}; zero: {res.Count(x => x == 0f)}");
        }
    }
    [TestMethod]
    public void tt()
    {

        var sequential = nn.Sequential(
            ("inputLayer", Linear(1000, 64)),
            ("func", Sigmoid()),
            ("hidden1", Linear(64, 1)),
            ("func", Tanh())
            );

        var t1 = TorchSharp.torch.randn(1000, 1000);
        sequential.forward(t1);

    }
}
