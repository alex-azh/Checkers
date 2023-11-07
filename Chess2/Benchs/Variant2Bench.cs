using BenchmarkDotNet.Attributes;

namespace Chess2.Benchs;
/*
| Method | Mean     | Error    | StdDev   | Gen0   | Allocated |
|------- |---------:|---------:|---------:|-------:|----------:|
| Var1   | 845.7 ns | 113.1 ns |  6.20 ns | 0.1726 |   1.41 KB |
| Var2   | 525.5 ns | 240.8 ns | 13.20 ns | 0.1554 |   1.27 KB |
 */

[MemoryDiagnoser]
[ShortRunJob]
public class Variant2Bench
{
    public readonly static uint Whites = UintHelper.CreateNumber(0, 1, 11, 15, 21);
    public readonly static uint Blacks = UintHelper.CreateNumber(4, 8, 5, 10, 18, 19, 26);
    public readonly static uint AllBlack = 0b11_11111_11111_00000_00000_00000_00000;
    public readonly static uint AllWhite = 0b00_00000_00000_00000_00011_11111_11111;
    public readonly static CheckersBoard Board = new(AllWhite, 0, AllBlack, 0);

    //[Benchmark]
    //public void Var1()
    //{
    //    _ = Board.Variants_old().Count();
    //}
    [Benchmark]
    public void VariantsP()
    {
        _ = Board.VariantsP().Count();
    }
}
