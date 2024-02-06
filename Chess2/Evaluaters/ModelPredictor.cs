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
    private Sequential _sequential;
    private OptimizerHelper _optimizer;
    public ModelPredictor()
    {
        // load model
        _sequential = Sequential(
            ("inputLayer", Linear(128, 64)),
            ("func", Sigmoid()),
            ("hidden1", Linear(64, 16)),
            ("func", Sigmoid()),
            ("output", Linear(16, 1)),
            ("func", Sigmoid())
            );
        double learningRate = 0.1; // Чем он выше, тем сильнее агент доверяет новой информации
        _optimizer = Adam(_sequential.parameters(), learningRate);
    }
    public ModelPredictor(string modelPath) : this()
    {
        Load(modelPath);
    }
    public float[] Predict(bool[][] array)
    {
        float[] input = array.SelectMany(x => x.Select(x => x ? 1f : 0f)).ToArray();
        torch.Tensor tensor = torch.from_array(input).reshape(array.Length, 128);
        torch.Tensor result = _sequential.forward(tensor);
        return [.. result.data<float>()];
    }
    public void Load(string filePath) => _sequential.load(filePath);
    public void Save(int epochNumber, string saveFileLocation) => _sequential.save(saveFileLocation + $"_{epochNumber}");
    public void Train(int epochs, int gamesCount, TimeSpan durationEpoch)
    {
        //TODO: вывести среднее время эпохи
        //TODO: сделать chunk
        //TODO: какие варианты выбирает нейронная сеть из лучших (статистика после эпох обучения)
        Evaluater evaluater = new(this);
        // загрузить в массивы результаты игр в количестве gameCount
        Stopwatch stopwatch = Stopwatch.StartNew();
        for (int i = 0; i < epochs; i++)
        {
            (List<float> evals, List<float> targets) games = GamesCreator();
            Tensor evals = from_array(games.evals.ToArray()),
                targets = from_array(games.targets.ToArray());
            Tensor loss = functional.mse_loss(evals, targets, Reduction.Sum);
            _optimizer.zero_grad();
            loss.backward();
            _optimizer.step();
            if (stopwatch.Elapsed > durationEpoch)
            {
                Save(i, "model_checkers");
                stopwatch.Restart();
            }
            Console.WriteLine(stopwatch.Elapsed);
        }

        (List<float> evals, List<float> targets) GamesCreator()
        {
            const float df = 0.7f;
            List<float> evals = [], targets = [];
            ComputerPlayer player1 = new(evaluater), player2 = new(evaluater);
            for (int i = 0; i < gamesCount; i++)
            {
                Game game = new(player1, player2);
                float gameResult = game.Start();
                if (gameResult != 0)
                {
                    evals.AddRange(player1.Moves.Select(x => x.Mark));
                    evals.AddRange(player2.Moves.Select(x => x.Mark));
                    targets.AddRange(Target(player1.Moves, gameResult).Reverse());
                    targets.AddRange(Target(player2.Moves, -gameResult).Reverse());
                }
                player1.Moves.Clear();
                player2.Moves.Clear();
            }
            return (evals, targets);
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
}

