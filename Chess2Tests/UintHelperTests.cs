using BenchmarkDotNet.Running;
using CheckersGame;
using CheckersTests.Benchmarks;

namespace CheckersTests
{
    [TestClass()]
    public class UintHelperTests
    {
        [TestMethod()]
        public void GetBitArrayTest()
        {
            Board board = new Board(0b01010011, 0b100, 0, 0);//0b100, 0b01001, 0b101);
            bool[] actual = GetBoolArrayBench.GetBoolArray(board);
            bool[] actual2 = board.BoolArray();
            CollectionAssert.AreEqual(actual, actual2);
        }
        [TestMethod()]
        public void Benchs()
        {
            BenchmarkRunner.Run<GetBoolArrayBench>();
        }
    }
}