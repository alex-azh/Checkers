using TorchSharp;
using TorchSharp.Modules;
using static Tensorboard.ApiDef.Types;
using static TorchSharp.torch.nn;
using static TorchSharp.torch.optim.lr_scheduler.impl;

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
            ("hidden1", Linear(64, 64)),
            ("hidden2", Linear(64, 16)),
            ("hidden", Linear(16, 1))
            );
    }
    public float[] Predict(bool[][] array)
    {
        float[] input = array.SelectMany(x => x.Select(x => x ? 1f : 0f)).ToArray();
        torch.Tensor tensor = torch.from_array(input).reshape(array.Length, 128);
        torch.Tensor result = _sequential.forward(tensor);
        return [.. result.data<float>()];
    }
}
