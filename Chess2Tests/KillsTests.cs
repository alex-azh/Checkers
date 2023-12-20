using Chess2;

namespace Chess2Tests;

[TestClass]
public class KillsTests
{
    public readonly uint AllBlack = 0b11_11111_11111_00000_00000_00000_00000;
    public readonly uint AllWhite = 0b00_00000_00000_00000_00011_11111_11111;
    [TestMethod]
    public void Test1()
    {
        var whites = UintHelper.CreateNumber(0, 1, 11, 15, 21);
        var blacks = UintHelper.CreateNumber(4, 8, 5, 10, 18, 19, 26);
        var doska = new CheckersBoard(whites, 0, 0, blacks);
        var actual = new List<(int, int, int)>()
        {
            (15, 22, 19),
            (0, 9, 4),
            (21,30,26),
            (21, 25, 0),
            (11, 14, 0)
        };
        var result = doska.Variants().Select(x => CheckersBoard.KillOutput(doska, x)).ToList();
        Console.WriteLine(string.Join("\n", result));
        CollectionAssert.AreEquivalent(actual, result);
    }
    [TestMethod]
    public void Test2()
    {
        var whites = UintHelper.CreateNumber(0, 1, 11, 15, 21);
        var blacks = UintHelper.CreateNumber(4, 8, 5, 10, 18, 19, 26);
        var doska = new CheckersBoard(whites, 0, 0, blacks);
        var actual = new List<(int, int, int)>()
        {
            (0, 9, 4),
            (11, 14, 0),
            (15, 22, 19),
            (21, 25, 0),
            (21, 30, 26)
        };
        var result = doska.Variants().Select(x => CheckersBoard.KillOutput(doska, x)).ToList();
        Console.WriteLine(string.Join("\n", result));
        CollectionAssert.AreEquivalent(actual, result);
    }
    [TestMethod]
    public void Test3()
    {
        var whites = UintHelper.CreateNumber(0, 1, 11, 15, 21);
        var blacks = UintHelper.CreateNumber(4, 8, 5, 18, 19, 26);
        var doska = new CheckersBoard(whites, 0, 0, blacks);
        var actual = new List<(int, int, int)>()
        {
            (0, 9, 4) ,
            (1, 10, 5),
            (11, 14, 0),
            (15,22,19),
            (21,30,26),
            (21, 25, 0)
        };
        var l = doska.Variants().Select(x => CheckersBoard.KillOutput(doska, x)).ToList();
        Console.WriteLine(string.Join(", ", l));
        CollectionAssert.AreEquivalent(actual, l);
    }
    [TestMethod]
    public void Test4()
    {
        var whites = UintHelper.CreateNumber(0, 1, 11, 15, 21);
        var blacks = UintHelper.CreateNumber(4, 8, 5, 10, 18, 19, 26);
        var doska = new CheckersBoard(whites, 0, 0, blacks);
        var actual = new List<(int, int, int)>()
        {
            (15, 22, 19),
            (0,9,4),
            (21,30,26),
            (21, 25, 0),
             (11, 14, 0)
        };
        var result = doska.Variants().Select(x => CheckersBoard.KillOutput(doska, x)).ToList();
        CollectionAssert.AreEquivalent(actual, result);
    }
    [TestMethod]
    public void Test5()
    {
        var doska = new CheckersBoard(AllWhite, 0, AllBlack, 0);
        Assert.AreEqual(7, doska.Variants().Count());
    }
}