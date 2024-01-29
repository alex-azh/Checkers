using CheckersGame;

namespace CheckersTests.Moves;

[TestClass]
public class KillsTests
{

    [TestMethod]
    public void Test1()
    {
        uint whites = UintHelper.CreateNumber(0, 1, 11, 15, 21);
        uint blacks = UintHelper.CreateNumber(4, 8, 5, 10, 18, 19, 26);
        Board doska = new Board(whites, 0, 0, blacks);
        List<List<uint>> exp = [
            [1, 5, 8, 9, 10, 11, 15, 18, 19, 21, 26],
            [0, 1, 4, 5, 8, 10, 11, 18, 21, 29],
            [0, 1, 4, 5, 8, 10, 11, 15, 18, 19, 30]
        ];
        List<uint> result = doska.Moves().Select(x => x.All.IndexesOfOnesPositions().ToList()).SelectMany(x => x).ToList();
        List<uint> col1 = exp.SelectMany(x => x).ToList();
        CollectionAssert.AreEquivalent(col1, result);
    }
    [TestMethod]
    public void Test2()
    {
        uint whites = UintHelper.CreateNumber(0, 1, 11, 15, 21);
        uint blacks = UintHelper.CreateNumber(4, 8, 5, 10, 18, 19, 26);
        Board doska = new Board(whites, 0, 0, blacks);
        List<(int, int, int)> actual = new List<(int, int, int)>()
        {
            // простых ходов не должно быть, т.к. об€зательна рубка
            //(11, 14, 0),
            //(21, 25, 0),
            (0, 9, 4),
            (15, 22, 19),
            (21, 30, 26)
        };
        List<(int fromPos, int toPos, int killedPos)> result = doska.Moves().Select(x => Board.WhoMovedWhites(doska, x)).ToList();
        Console.WriteLine(string.Join("\n", result));
        CollectionAssert.AreEquivalent(actual, result);
    }
    [TestMethod]
    public void Test3()
    {
        uint whites = UintHelper.CreateNumber(0, 1, 11, 15, 21);
        uint blacks = UintHelper.CreateNumber(4, 8, 5, 18, 19, 26);
        Board doska = new Board(whites, 0, 0, blacks);
        List<(int, int, int)> actual = new List<(int, int, int)>()
        {
            //(11, 14, 0),
            //(21, 25, 0)
            (0, 9, 4) ,
            (1, 10, 5),
            (15,22,19),
            (21,30,26),
        };
        List<(int fromPos, int toPos, int killedPos)> l = doska.Moves().Select(x => Board.WhoMovedWhites(doska, x)).ToList();
        Console.WriteLine(string.Join(", ", l));
        CollectionAssert.AreEquivalent(actual, l);
    }
    [TestMethod]
    public void Test4()
    {
        uint whites = UintHelper.CreateNumber(0, 1, 11, 15, 21);
        uint blacks = UintHelper.CreateNumber(4, 8, 5, 10, 18, 19, 26);
        Board doska = new Board(whites, 0, 0, blacks);
        List<(int, int, int)> actual = new List<(int, int, int)>()
        {
             //(11, 14, 0)
            //(21, 25, 0),
            (15, 22, 19),
            (0,9,4),
            (21,30,26),
        };
        List<(int fromPos, int toPos, int killedPos)> result = doska.Moves().Select(x => Board.WhoMovedWhites(doska, x)).ToList();
        CollectionAssert.AreEquivalent(actual, result);
    }
    [TestMethod]
    public void Test5()
    {
        uint AllBlack = 0b11_11111_11111_00000_00000_00000_00000;
        uint AllWhite = 0b00_00000_00000_00000_00011_11111_11111;
        Board doska = new Board(AllWhite, 0, AllBlack, 0);
        Assert.AreEqual(7, doska.Moves().Count());
    }

}