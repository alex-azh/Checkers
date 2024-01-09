using CheckersGame;
using System.Numerics;

namespace CheckersTests;

[TestClass]
public class QueenCreateFromCheckersTests
{
    [TestMethod]
    public void Test1()
    {
        Board b = new(UintHelper.CreateNumber(24, 25, 26, 27), 0, 0, 0);
        int[] collection = b.Moves().Select(x => BitOperations.Log2(x.WhiteD)).Distinct().ToArray();
        int[] values = { 28, 29, 30, 31 };
        CollectionAssert.AreEquivalent(values, collection);
    }
    [TestMethod]
    public void Test2()
    {
        uint mask = UintHelper.CreateNumber(28, 29, 30, 31),
            x26 = UintHelper.CreateNumber(26),
            x28 = UintHelper.CreateNumber(28),
            x30 = UintHelper.CreateNumber(30),
            x5 = UintHelper.CreateNumber(5),
            x21 = UintHelper.CreateNumber(21),
            x27 = UintHelper.CreateNumber(27);
        Assert.AreEqual(26, f(x26));
        Assert.AreEqual(0, f(x28));
        Assert.AreEqual(0, f(x30));
        Assert.AreEqual(5, f(x5));
        Assert.AreEqual(21, f(x21));
        Assert.AreEqual(27, f(x27));

        int f(uint x) => BitOperations.Log2((mask ^ x) & x);
    }
    [TestMethod]
    public void Test3()
    {
        uint mask = UintHelper.CreateNumber(28, 29, 30, 31),
            x26 = UintHelper.CreateNumber(26),
            x28 = UintHelper.CreateNumber(28),
            x30 = UintHelper.CreateNumber(30),
            x5 = UintHelper.CreateNumber(5),
            x21 = UintHelper.CreateNumber(21),
            x27 = UintHelper.CreateNumber(27);
        Assert.AreEqual(26, f(x26));
        Assert.AreEqual(0, f(x28));
        Assert.AreEqual(0, f(x30));
        Assert.AreEqual(5, f(x5));
        Assert.AreEqual(21, f(x21));
        Assert.AreEqual(27, f(x27));

        int f(uint x) => BitOperations.Log2(~mask & x);
    }
}
