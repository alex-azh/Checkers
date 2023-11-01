using BenchmarkDotNet.Attributes;
using Chess2.Tests;
/*
| Method          | Mean       | Error    | StdDev   | Gen0   | Allocated |
|---------------- |-----------:|---------:|---------:|-------:|----------:|
| Kills1_Julia    |   666.7 ns | 110.1 ns |  6.04 ns | 0.1850 |   1.52 KB |
| Kills3_Julia    | 1,088.6 ns | 100.9 ns |  5.53 ns | 0.2708 |   2.22 KB |
| Kills1Struct    | 1,204.5 ns | 235.6 ns | 12.91 ns | 0.2804 |    2.3 KB |
| Kills2_Julia    | 1,256.4 ns | 245.5 ns | 13.46 ns | 0.3357 |   2.75 KB |
| Variants1Struct | 1,315.3 ns | 252.2 ns | 13.82 ns | 0.3338 |   2.73 KB |
| Kills2Struct    | 3,023.2 ns | 313.5 ns | 17.18 ns | 0.6218 |   5.09 KB |
| Kills3Struct    | 3,023.5 ns | 233.0 ns | 12.77 ns | 0.5760 |   4.73 KB |
*/
namespace Chess2.Benchs;

[ShortRunJob]
[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class BenchTests
{
    #region Alex
    [Benchmark]
    public void Kills1Struct()
    {
        TestClass.TestKills1Count();
    }
    [Benchmark]
    public void Kills2Struct()
    {
        TestClass.TestKills2Count();

    }
    [Benchmark]
    public void Kills3Struct()
    {
        TestClass.TestKills3Count();
    }
    [Benchmark]
    public void Variants1Struct()
    {
        TestClass.TestVariants1Count();
    }
    #endregion Alex

    #region Julia
    [Benchmark]
    public void Kills1_Julia()
    {
        TestClassJulia.TestKills1Count();
    }
    [Benchmark]
    public void Kills2_Julia()
    {
        TestClassJulia.TestKills2Count();
    }
    [Benchmark]
    public void Kills3_Julia()
    {
        TestClassJulia.TestKills3Count();
    }
    #endregion Julia
}
