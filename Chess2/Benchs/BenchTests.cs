using BenchmarkDotNet.Attributes;
/*
| Method              | Mean     | Error     | StdDev    | Gen0   | Allocated |
|-------------------- |---------:|----------:|----------:|-------:|----------:|
| TestKills1Struct    | 1.259 us | 0.8436 us | 0.0462 us | 0.2804 |    2.3 KB |
| TestKills2Struct    | 3.025 us | 0.1849 us | 0.0101 us | 0.6218 |   5.09 KB |
| TestKills3Struct    | 2.964 us | 0.7900 us | 0.0433 us | 0.5760 |   4.73 KB |
| TestVariants1Struct | 1.268 us | 0.1305 us | 0.0072 us | 0.3338 |   2.73 KB |
 */
namespace Chess2.Benchs;

[ShortRunJob]
[MemoryDiagnoser]
public class BenchTests
{
    [Benchmark]
    public void TestKills1Struct()
    {
        TestClass.TestKills1Count();
    }
    [Benchmark]
    public void TestKills2Struct()
    {
        TestClass.TestKills2Count();

    }
    [Benchmark]
    public void TestKills3Struct()
    {
        TestClass.TestKills3Count();
    }
    [Benchmark]
    public void TestVariants1Struct()
    {
        TestClass.TestVariants1Count();
    }
}
