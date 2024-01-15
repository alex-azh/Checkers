using CheckersGame;
using CheckersGame.Figures;
using System.Diagnostics;

namespace CheckersTests;

[TestClass]
public class QueenMovesTests
{
    [TestMethod]
    public void ForwardKill_Test1()
    {
        Board board = new(0, UintHelper.CreateNumber(0, 1, 18), UintHelper.CreateNumber(4, 8, 5, 22), 0);
        List<(int fromPos, int toPos, int killedPos)> results = (from b in Queen.Moves(board)
                                                                 select Board.WhoMoved(board, b.board))
                       .ToList();
        List<(int, int, int)> expected = new()
        {
            (0, 9, 4),
            (1,10,5),
            (18,13,0),
            (18,14,0),
            (18,21,0),
            (18,27,22)
        };
        CollectionAssert.AreEquivalent(expected, results);
    }

    [TestMethod]
    public void StepsTest()
    {
        Board board = new(0, UintHelper.CreateNumber(20, 24, 19, 16), 0, 0);
        List<(int fromPos, int toPos, int killedPos)> results = (from b in Queen.Moves(board)
                                                                 select Board.WhoMoved(board, b.board))
                       .ToList();
        List<(int, int, int)> expected = new()
        {
            (20, 17, 0),
            (20,25,0),
            (24,28,0),
            (19,15,0),
            (19,22,0),
            (19,14,0),
            (19,23,0),
            (16,12,0)
        };
        Debug.WriteLine(string.Join(",", results));
        CollectionAssert.AreEquivalent(expected, results);
    }
    [TestMethod]
    public void BackwardKillsTest()
    {
        Board board = new(0, UintHelper.CreateNumber(31, 16, 2), 0, UintHelper.CreateNumber(27, 5, 25, 12, 20, 9, 6, 11));
        List<(int fromPos, int toPos, int killedPos)> results = (from b in Queen.Moves(board)
                                                                 select Board.WhoMoved(board, b.board))
                       .ToList();
        List<(int, int, int)> expected = new()
        {
            (31,22,27)
        };
        Debug.WriteLine(string.Join(",", results));
        CollectionAssert.AreEquivalent(expected, results);
    }
}
