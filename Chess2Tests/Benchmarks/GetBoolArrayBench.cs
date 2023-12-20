using BenchmarkDotNet.Attributes;
using Chess2;
using System.Collections;

namespace Chess2Tests.Benchmarks
{
    [MemoryDiagnoser]
    [ShortRunJob]
    public class GetBoolArrayBench
    {
        public static CheckersBoard board = new CheckersBoard(0b01010011, 0b100, 0b010000101, 0b01010111);
        [Benchmark]
        public void GetBoolArray_Julia()
        {
            _ = GetBoolArray(board);
        }

        [Benchmark]
        public void GetBoolArray_Alex()
        {
            _ = GetBoolArray2(board);
        }
        public static bool[] GetBoolArray2(CheckersBoard board)
        {
            bool[] result = new bool[128];
            for (int i = 0; i < 32; ++i)
            {
                result[i] = ((board.WhiteP >> i) & 0x01) > 0;
                result[i + 32] = ((board.WhiteD >> i) & 0x01) > 0;
                result[i + 64] = ((board.BlackP >> i) & 0x01) > 0;
                result[i + 96] = ((board.BlackD >> i) & 0x01) > 0;
            }
            return result;
        }
        public static bool[] GetBoolArray(CheckersBoard board)
        {
            bool[] result = new bool[128];
            int index = 0;
            tt(board.WhiteP);
            tt(board.WhiteD);
            tt(board.BlackP);
            tt(board.BlackD);
            void tt(uint number)
            {
                BitArray ba = new BitArray(BitConverter.GetBytes(number));
                ba.Cast<bool>().ToArray().CopyTo(result, index);
                index += 32;
            }
            return result;
        }
    }
}
