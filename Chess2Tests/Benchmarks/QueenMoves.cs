namespace CheckersTests.Benchmarks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CheckersGame;


[TestClass]
public class QueenMovesBenchTests
{
    //[TestMethod]
    public void Run()
    {
        BenchmarkRunner.Run<ReverseBench>();
    }
}
/*
| Method        | Mean     | Error    | StdDev  | Gen0   | Allocated |
|-------------- |---------:|---------:|--------:|-------:|----------:|
| Concat        | 468.3 ns | 35.46 ns | 1.94 ns | 0.1936 |    1216 B |
| AllInMethod   | 374.6 ns | 43.55 ns | 2.39 ns | 0.1526 |     960 B |
| AllInMethod_2 | 372.2 ns |  6.53 ns | 0.36 ns | 0.1526 |     960 B |
*/
[ShortRunJob]
[MemoryDiagnoser]
public class QueenMovesBench
{
    static Board board = new(0, UintHelper.CreateNumber(0, 1, 18), UintHelper.CreateNumber(4, 8, 5, 22), 0);
    public static IEnumerable<Board> Moves3(Board board)
    {
        foreach (uint position in board.WhiteD.ExtractOnes())
        {
            #region Forward
            #region steps
            Masks.Steps.TryGetValue(position, out uint variant);
            foreach (uint newPosition in (variant & ~board.All).ExtractOnes())
            {
                yield return board with
                {
                    WhiteD = board.WhiteD & ~position | newPosition
                };
            }
            #endregion steps

            #region kills
            foreach (uint killedFigurePosition in (variant & board.Blacks).ExtractOnes())
            {
                Masks.StepsByDirection.TryGetValue(killedFigurePosition | position, out uint stepPos);
                foreach (uint newPosition in (stepPos & ~board.All).ExtractOnes())
                {
                    yield return board with
                    {
                        WhiteD = board.WhiteD & ~position | newPosition,
                        BlackP = board.BlackP & ~killedFigurePosition,
                        BlackD = board.BlackD & ~killedFigurePosition
                    };
                }
            }
            #endregion kills
            #endregion Forward

            #region Backward
            #region steps
            Masks.StepsBackward.TryGetValue(position, out uint backwardSteps);
            foreach (uint newPosition in (backwardSteps & ~board.All).ExtractOnes())
            {
                yield return board with
                {
                    WhiteD = board.WhiteD & ~position | newPosition
                };
            }
            #endregion steps

            #region kills
            foreach (uint killedFigurePosition in (backwardSteps & board.Blacks).ExtractOnes())
            {
                Masks.KillsBackward.TryGetValue(killedFigurePosition | position, out uint stepPos);
                foreach (uint newPosition in (stepPos & ~board.All).ExtractOnes())
                {
                    yield return board with
                    {
                        WhiteD = board.WhiteD & ~position | newPosition,
                        BlackP = board.BlackP & ~killedFigurePosition,
                        BlackD = board.BlackD & ~killedFigurePosition
                    };
                }
            }
            #endregion kills
            #endregion Backward
        }
    }
    public static IEnumerable<Board> Moves2(Board board)
    {
        foreach (uint position in board.WhiteD.ExtractOnes())
        {
            #region Forward
            #region steps
            Masks.Steps.TryGetValue(position, out uint variant);
            foreach (uint newPosition in (variant & ~board.All).ExtractOnes())
            {
                yield return new(
                    board.WhiteP,
                    board.WhiteD & ~position | newPosition,
                    board.BlackP,
                    board.BlackD);
            }
            #endregion steps

            #region kills
            foreach (uint killedFigurePosition in (variant & board.Blacks).ExtractOnes())
            {
                Masks.StepsByDirection.TryGetValue(killedFigurePosition | position, out uint stepPos);
                foreach (uint newPosition in (stepPos & ~board.All).ExtractOnes())
                {
                    yield return new(
                        board.WhiteP,
                        board.WhiteD & ~position | newPosition,
                        board.BlackP & ~killedFigurePosition,
                        board.BlackD & ~killedFigurePosition
                        );
                }
            }
            #endregion kills
            #endregion Forward

            #region Backward
            #region steps
            Masks.StepsBackward.TryGetValue(position, out uint backwardSteps);
            foreach (uint newPosition in (backwardSteps & ~board.All).ExtractOnes())
            {
                yield return new(
                    board.WhiteP,
                    board.WhiteD & ~position | newPosition,
                    board.BlackP,
                    board.BlackD);
            }
            #endregion steps

            #region kills
            foreach (uint killedFigurePosition in (backwardSteps & board.Blacks).ExtractOnes())
            {
                Masks.KillsBackward.TryGetValue(killedFigurePosition | position, out uint stepPos);
                foreach (uint newPosition in (stepPos & ~board.All).ExtractOnes())
                {
                    yield return new(
                        board.WhiteP,
                        board.WhiteD & ~position | newPosition,
                        board.BlackP & ~killedFigurePosition,
                        board.BlackD & ~killedFigurePosition
                        );
                }
            }
            #endregion kills
            #endregion Backward
        }
    }
    public static IEnumerable<Board> Moves(Board board)
    {
        return Variants(Masks.Steps, Masks.StepsByDirection)
        .Concat(Variants(Masks.StepsBackward, Masks.KillsBackward));

        IEnumerable<Board> Variants(Dictionary<uint, uint> stepsDict, Dictionary<uint, uint> killsDict)
        {
            foreach (uint position in board.WhiteD.ExtractOnes())
            {
                #region Forward
                #region steps
                stepsDict.TryGetValue(position, out uint variant);
                foreach (uint newPosition in (variant & ~board.All).ExtractOnes())
                {
                    // yield return board with
                    // {
                    //     WhiteD = board.WhiteD & ~position | newPosition
                    // };
                    yield return new(
                        board.WhiteP,
                        board.WhiteD & ~position | newPosition,
                        board.BlackP,
                        board.BlackD);
                }
                #endregion steps

                #region kills
                foreach (uint killedFigurePosition in (variant & board.Blacks).ExtractOnes())
                {
                    killsDict.TryGetValue(killedFigurePosition | position, out uint stepPos);
                    foreach (uint newPosition in (stepPos & ~board.All).ExtractOnes())
                    {
                        // yield return board with
                        // {
                        //     WhiteD = board.WhiteD & ~position | newPosition,
                        //     BlackP = board.BlackP & ~killedFigurePosition,
                        //     BlackD = board.BlackD & ~killedFigurePosition
                        // };
                        yield return new(
                            board.WhiteP,
                            board.WhiteD & ~position | newPosition,
                            board.BlackP & ~killedFigurePosition,
                            board.BlackD & ~killedFigurePosition
                            );
                    }
                }
                #endregion kills
                #endregion Forward
            }
        }
    }
    [Benchmark]
    public void Concat()
    {
        _ = Moves(board).Count();
    }
    [Benchmark]
    public void AllInMethod()
    {
        _ = Moves2(board).Count();
    }
    [Benchmark]
    public void AllInMethod_2()
    {
        _ = Moves3(board).Count();
    }
}
