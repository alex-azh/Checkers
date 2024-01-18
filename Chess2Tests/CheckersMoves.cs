using CheckersGame;

namespace CheckersTests;

[TestClass]
public class CheckersMoves
{
    [TestMethod]
    public void Test1()
    {
        Board b = new(UintHelper.CreateNumber(0, 3, 7, 27, 24), 0, 0, 0);
        var moves = b.Moves().ToList();
        var indexesMoves = moves.Select(x => x.board.Whites.IndexesOfOnesPositions().ToArray()).ToList();
        Assert.AreEqual(6, moves.Count);
        (int fromPos, int toPos)[] actual = moves.Select(x =>
        {
            var (fromPos, toPos, killedPos) = Board.WhoMovedWhites(b, x.board);
            return (fromPos, toPos);
        }).ToArray();
        (int, int)[] exp =
        {
            (0,4), (3,6), (7,11), (27,30), (27, 31), (24,28)
        };
        CollectionAssert.AreEquivalent(exp, actual);
    }
    [TestMethod]
    public void Test2()
    {
        Board b = new(UintHelper.CreateNumber(24, 16), 0, 0, 0);
        var actual = b.Moves().Select(x => Board.WhoMovedWhites(b, x.board));
        (int, int, int)[] exp = { (24, 28, 0), (16, 20, 0) };
        CollectionAssert.AreEquivalent(exp, actual.ToArray());
    }
}
