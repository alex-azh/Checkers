using CheckersGame.GameSpace;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch.nn;

namespace CheckersGame.Evaluaters;

/// <summary>
/// Нейросеть.
/// </summary>
public class ModelPredictor : IPredictor
{
    private Sequential _sequential;
    public ModelPredictor()
    {
        // load model
        _sequential = Sequential(
            ("inputLayer", Linear(128, 64)),
            ("func", Sigmoid()),
            //("hidden1", Linear(64, 64)),
            //("func", Sigmoid()),
            ("output", Linear(16, 1))
            );
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
    public void Save(string saveFileLocation) => _sequential.save(saveFileLocation);
    public void Train(int epochs, int gamesCount)
    {
        Evaluater evaluater = new(this);
        // загрузить в массивы результаты игр в количестве gameCount
        (float[][][] moves, float[] gameResults) = GamesCreator();

        for (int i = 0; i < epochs; i++)
        {

        }
        (float[][][], float[]) GamesCreator()
        {
            List<float[][]> moves = [];
            List<float> gamesResults = [];
            for (int i = 0; i < gamesCount; i++)
            {
                ComputerPlayer player1 = new ComputerPlayer(evaluater),
                                player2 = new ComputerPlayer(evaluater);
                Game game = new(new ComputerPlayer(evaluater), new ComputerPlayer(evaluater));
                float gameResult = game.Start();
                if (gameResult != 0)
                {
                    moves.Add(player1.Moves.Select(x => x.FloatArray()).ToArray());
                    moves.Add(player2.Moves.Select(x => x.FloatArray()).ToArray());
                    gamesResults.Add(gameResult);
                    gamesResults.Add(-gameResult);
                }
            }
            return (moves.ToArray(), gamesResults.ToArray());
        }
    }
}
