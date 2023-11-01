using BenchmarkDotNet.Attributes;

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
