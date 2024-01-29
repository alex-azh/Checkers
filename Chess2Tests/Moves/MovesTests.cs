using CheckersGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersTests.Moves;

[TestClass]
public class MovesTests
{
    [TestMethod]
    public void Test1()
    {
        var board = new Board([], [0], [4, 5, 12], []);
        var moves = board.Moves().Select(x => x.All).ToList();
        uint expected1 = UintHelper.CreateNumber(16, 5), expected2 = UintHelper.CreateNumber(12, 2);
        CollectionAssert.AreEquivalent(new List<uint>() { expected1, expected2 }, moves);
    }
    [TestMethod]
    public void Test2()
    {
        var board = new Board([], [0], [4, 5, 12, 13], []);
        var moves = board.Moves().Select(x => x.All).ToList();
        uint expected1 = UintHelper.CreateNumber(16, 5, 13), expected2 = UintHelper.CreateNumber(12, 2, 13),
            expected3 = UintHelper.CreateNumber(12, 5, 18);
        CollectionAssert.AreEquivalent(new List<uint>() { expected1, expected2, expected3 }, moves);
    }
}
