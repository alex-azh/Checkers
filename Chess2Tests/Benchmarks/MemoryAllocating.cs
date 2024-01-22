using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersTests.Benchmarks;
public class TestClass
{
    byte data;
}
public class TestClass2(TestClass test)
{
    byte data;
    TestClass testClass { get; set; } = test;
}

[TestClass]
[MemoryDiagnoser]
[ShortRunJob]
public class MemoryAllocating
{
    [TestMethod]
    public void Run()
    {
        BenchmarkRunner.Run<MemoryAllocating>();
    }
    [Benchmark]
    public void WithoutRefObject()
    {
        // создано 4 класса.
        TestClass c = new TestClass(), c2 = new(), c3 = new(), c4 = new();
    }
    [Benchmark]
    public void RefObject()
    {
        // создано 2 класса Тесткласс и 2 класса обычных.
        TestClass2 c = new(new()),
            c2 = new(new());
    }
}
