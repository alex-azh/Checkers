using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch.nn;

namespace CheckersTests.ML
{
    [TestClass]
    public class MLTests
    {
        [TestMethod]
        public void TestInit()
        {
            Sequential sequential = Sequential(
            ("inputLayer", Linear(3, 64)),
            ("hidden1", Linear(64, 64)),
            ("hidden2", Linear(64, 16)),
            ("hidden", Linear(16, 1))
            );
            bool[][] arr = [
                [false, false, false],
                [true, true, true],
            ];
            // (1x6 and 3x64)
            float[] input = arr.SelectMany(x => x.Select(x => x ? 1f : 0f)).ToArray();
            torch.Tensor tensor = torch.from_array(input, torch.ScalarType.Float32).reshape(2, 3);
            torch.Tensor forwared = sequential.forward(tensor);
            float[] result = [..forwared.data<float>()];
            Console.WriteLine(string.Join(", ", result));
        }
    }
}
