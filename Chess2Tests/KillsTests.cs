using System.Numerics;

namespace Chess2Tests;

[TestClass]
public class KillsTests
{
    public uint AllBlack = 0b11_11111_11111_00000_00000_00110_00000;
    public uint AllWhite = 0b00_00000_00000_00000_00011_11111_11111;
    [TestMethod]
    public void Test1()
    {
        var whites = Doska.CreateNumber(0, 1, 11, 15, 21);
        var blacks = Doska.CreateNumber(4, 8, 5, 10, 18, 19, 26);
        var doska = new Doska(whites, 0, 0, blacks);
        var actual = new List<(int, int, int)>()
        {
            (15, 22, 19),
            (0,9,4),
            (21,30,26)
        };
        var killsOfJulia = doska.WhitePKills().Select(x => Doska.KillOutput(doska, x)).ToList();
        var killsOfAlex = doska.WhitePKillVariants().Select(x => Doska.KillOutput(doska, x)).ToList();
        CollectionAssert.AreEquivalent(actual, killsOfJulia);
        CollectionAssert.AreEquivalent(actual, killsOfAlex);
    }

    [TestMethod]
    public void Test2()
    {
        var whites = Doska.CreateNumber(0, 1, 11, 15, 21);
        var blacks = Doska.CreateNumber(4, 8, 5, 10, 18, 19, 26);
        var doska = new Doska(whites, 0, 0, blacks);
        var actual = new List<(int, int, int)>()
        {
            (15, 22, 19),
            (0,9,4),
            (21,30,26)
        };
        var l = doska.WhitePVariants().ToList();
        Console.WriteLine(Doska.KillOutput(doska, l.First()));
        Console.WriteLine(doska.GetHod(0));
    }
}