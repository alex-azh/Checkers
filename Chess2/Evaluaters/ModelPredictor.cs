using CheckersGame.GameSpace;
using System.Diagnostics;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;
using static TorchSharp.torch.nn;
using static TorchSharp.torch.optim;

namespace CheckersGame.Evaluaters;

/// <summary>
/// Нейросеть.
/// </summary>
public class ModelPredictor : IPredictor
{
    public Sequential _sequential;
    private OptimizerHelper _optimizer;
    public Device DEVICE = torch.device(DeviceType.CPU);
    //public Device DEVICE = torch.device(torch.cuda.is_available() ? DeviceType.GPU : DeviceType.CPU);
    public static int COUNTPredicts = 0;
    public ModelPredictor()
    {
        // load model
        Linear lin1 = Linear(128, 64, false, DEVICE),
            lin2 = Linear(64, 32, false, DEVICE),
            lin3 = Linear(32, 16, false, DEVICE),
            lin4 = Linear(16, 1, false, DEVICE);
        init.normal_(lin1.weight);
        init.normal_(lin2.weight);
        init.normal_(lin3.weight);
        init.normal_(lin4.weight);
        _sequential = Sequential(
            ("inputLayer", lin1),
            ("func", Sigmoid().to(DEVICE)),
            ("hidden1", lin2),
            ("func", Sigmoid().to(DEVICE)),
            ("hidden2", lin3),
            ("func", Sigmoid().to(DEVICE)),
            ("output", lin4),
            ("func", Tanh().to(DEVICE)));
        double learningRate = 0.001;
        var list = this._sequential.parameters().Select(x => x.device_type).ToList();
        _optimizer = Adam(_sequential.parameters(), learningRate);
        _sequential.eval();
    }
    public ModelPredictor(string modelPath) : this()
    {
        Load(modelPath);
    }
    public float[] Predict(float[][] array)
    {
        //COUNTPredicts += array.Length;
        using var nograd = torch.no_grad();
        float[] input = array.SelectMany(x => x).ToArray();
        torch.Tensor tensor = torch.from_array(input, DEVICE).reshape(array.Length, 128);
        torch.Tensor result = _sequential.forward(tensor);
        //return [.. Enumerable.Range(0, array.Length).Select(x => 0.1f)];
        return [.. result.data<float>()];
    }
    public void Load(string filePath) => _sequential.load(filePath);
    public void Save(int epochNumber, string saveFileLocation) => _sequential.save(Path.Combine(@"D:\models_checkers", saveFileLocation) + $"_{epochNumber}");
    public void Train(int epochs, int gamesCount, TimeSpan durationEpoch)
    {
        //TODO: вывести среднее время эпохи
        //TODO: сделать chunk
        //TODO: какие варианты выбирает нейронная сеть из лучших (статистика после эпох обучения)
        _sequential.train();
        Evaluater evaluater = new(this);
        // загрузить в массивы результаты игр в количестве gameCount
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < epochs; i++)
        {
            (List<Board> boards, List<float> targets, List<float> results) games = GamesCreator(gamesCount, evaluater);
            float[] input = games.boards.SelectMany(x => x.FloatArray()).ToArray();
            Tensor tensor = from_array(input, DEVICE).reshape(games.boards.Count, 128);
            Tensor evals = _sequential.forward(tensor);
            Tensor targets = from_array(games.targets.ToArray(), DEVICE);
            Tensor loss = functional.mse_loss(evals, targets, Reduction.Sum);
            _optimizer.zero_grad();
            loss.backward();
            _optimizer.step();
            //if (stopwatch.Elapsed > durationEpoch)
            //{
            //    Save(i, "model_checkers");
            //    stopwatch.Restart();
            //}
            //Console.WriteLine(stopwatch.Elapsed);
        }
        _sequential.eval();
    }

    public static (List<Board> boards, List<float> targets, List<float> gameResults) GamesCreator(int gamesCount, Evaluater evaluater)
    {
        const float df = 0.7f;
        List<Board> boards = []; List<float> targets = [];
        List<float> gameResults = [];
        ComputerPlayer player1 = new(evaluater), player2 = new(evaluater);
        for (int i = 0; i < gamesCount; i++)
        {
            Game game = new(player1, player2);
            float gameResult = game.Start();
            if (gameResult != 0)
            {
                boards.AddRange(player1.Moves);
                boards.AddRange(player2.Moves);
                targets.AddRange(Target(player1.Moves, gameResult).Reverse());
                targets.AddRange(Target(player2.Moves, -gameResult).Reverse());
            }
            player1.Moves.Clear();
            player2.Moves.Clear();
            gameResults.Add(gameResult);
        }
        return (boards, targets, gameResults);
        static IEnumerable<float> Target(List<Board> moves, float result)
        {
            float next = result;
            for (int j = moves.Count - 1; j >= 0; j--)
            {
                next = moves[j].Mark + (next - moves[j].Mark) * df;
                yield return next;
            }
        }
    }
    public void Train((List<Board> boards, List<float> targets) games, int epochs, int gamesCount, TimeSpan durationEpoch)
    {
        //TODO: вывести среднее время эпохи
        //TODO: сделать chunk
        //TODO: какие варианты выбирает нейронная сеть из лучших (статистика после эпох обучения)
        Evaluater evaluater = new(this);
        // загрузить в массивы результаты игр в количестве gameCount
        for (int i = 0; i < epochs; i++)
        {
            float[] input = games.boards.SelectMany(x => x.FloatArray()).ToArray();
            Tensor tensor = from_array(input, DEVICE).reshape(games.boards.Count, 128);
            Tensor evals = _sequential.forward(tensor);
            Tensor targets = from_array(games.targets.ToArray(), DEVICE);
            Tensor loss = functional.mse_loss(evals, targets, Reduction.Sum);
            _optimizer.zero_grad();
            loss.backward();
            _optimizer.step();
            //Console.WriteLine(stopwatch.Elapsed);
        }
        //Save(0, string.Format("model_checkers_end_{0}", DateTime.Now));
        (List<Board> boards, List<float> targets) GamesCreator()
        {
            const float df = 0.7f;
            List<Board> boards = []; List<float> targets = [];
            ComputerPlayer player1 = new(evaluater), player2 = new(evaluater);
            for (int i = 0; i < gamesCount; i++)
            {
                Game game = new(player1, player2);
                float gameResult = game.Start();
                if (gameResult != 0)
                {
                    boards.AddRange(player1.Moves);
                    boards.AddRange(player2.Moves);
                    targets.AddRange(Target(player1.Moves, gameResult).Reverse());
                    targets.AddRange(Target(player2.Moves, -gameResult).Reverse());
                }
                player1.Moves.Clear();
                player2.Moves.Clear();
            }
            return (boards, targets);
            static IEnumerable<float> Target(List<Board> moves, float result)
            {
                float next = result;
                for (int j = moves.Count - 1; j >= 0; j--)
                {
                    next = moves[j].Mark + (next - moves[j].Mark) * df;
                    yield return next;
                }
            }
        }

    }
    //public (List<Board> boards, List<float> targets) GamesCreator()
    //{
    //    const float df = 0.7f;
    //    List<Board> boards = []; List<float> targets = [];
    //    ComputerPlayer player1 = new(evaluater), player2 = new(evaluater);
    //    for (int i = 0; i < gamesCount; i++)
    //    {
    //        Game game = new(player1, player2);
    //        float gameResult = game.Start();
    //        if (gameResult != 0)
    //        {
    //            boards.AddRange(player1.Moves);
    //            boards.AddRange(player2.Moves);
    //            targets.AddRange(Target(player1.Moves, gameResult).Reverse());
    //            targets.AddRange(Target(player2.Moves, -gameResult).Reverse());
    //        }
    //        player1.Moves.Clear();
    //        player2.Moves.Clear();
    //    }
    //    return (boards, targets);
    //    static IEnumerable<float> Target(List<Board> moves, float result)
    //    {
    //        float next = result;
    //        for (int j = moves.Count - 1; j >= 0; j--)
    //        {
    //            next = moves[j].Mark + (next - moves[j].Mark) * df;
    //            yield return next;
    //        }
    //    }
    //}
}

