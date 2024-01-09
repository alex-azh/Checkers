using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CheckersGame;

namespace CheckersTests.Benchmarks
{
    [TestClass]
    public class ReverseBenchTest
    {
        [TestMethod]
        public void Run()
        {
            BenchmarkRunner.Run<ReverseBench>();
        }
    }
    /*
    | Method   | Mean        | Error      | StdDev    | Median      | Gen0   | Allocated |
    |--------- |------------:|-----------:|----------:|------------:|-------:|----------:|
    | Reverse1 |   0.0007 ns |  0.0215 ns | 0.0012 ns |   0.0000 ns |      - |         - |
    | Reverse2 | 883.0536 ns | 39.7229 ns | 2.1773 ns | 882.3501 ns | 0.0238 |     200 B |
     */
    [MemoryDiagnoser]
    [ShortRunJob]
    public class ReverseBench
    {
        public static uint AllBlack = 0b11_11111_11111_00000_00000_00000_00000;
        [Benchmark]
        public void Reverse1()
        {
            _ = Reverse1(AllBlack);
            _ = Reverse1(AllBlack);
            _ = Reverse1(AllBlack);
            _ = Reverse1(AllBlack);
            _ = Reverse1(AllBlack);
        }
        [Benchmark]
        public void Reverse2()
        {
            _ = Reverse2(AllBlack);
            _ = Reverse2(AllBlack);
            _ = Reverse2(AllBlack);
            _ = Reverse2(AllBlack);
            _ = Reverse2(AllBlack);
        }

        public static uint Reverse1(uint x)
        {
            x = (x & 0x55555555) << 1 | (x >>> 1) & 0x55555555;  // Четные и нечетные биты поменялись местами.
            x = (x & 0x33333333) << 2 | (x >>> 2) & 0x33333333;  // Биты "перетасовываются" группами по два.
            x = (x & 0x0F0F0F0F) << 4 | (x >>> 4) & 0x0F0F0F0F;  // Биты "перетасовываются" группами по четыре.
            x = (x & 0x00FF00FF) << 8 | (x >>> 8) & 0x00FF00FF;  // Биты "перетасовываются" группами по восемь.
            x = (x & 0x0000FFFF) << 16 | (x >>> 16) & 0x0000FFFF;  // Биты "перетасовываются" группами по 16.
            return x;
        }
        public static uint Reverse2(uint x)
        {
            uint result = 0;
            IEnumerable<uint> items = x.ExtractOnes();
            foreach (uint item in items)
            {
                int index = (int)Math.Log2(item);
                result |= 1u << 31 - index;
            }
            return result;
        }
    }
}
