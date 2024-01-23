using CheckersGame;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace CheckersTests
{
    [TestClass]
    public class ReverseTests
    {
        public static uint AllBlack = 0b1111_1111_1111_0000_0000_0000_0000_0000;
        public static uint AllWhite = 0b0000_0000_0000_0000_0000_1111_1111_1111;

        [TestMethod]
        public void Rev1Test()
        {
            Board board = new(UintHelper.CreateNumber(0, 1, 2), 0, UintHelper.CreateNumber(20, 21), 0),
                tested = new(UintHelper.CreateNumber(10, 11), 0, UintHelper.CreateNumber(31, 30, 29), 0);
            Assert.AreEqual(tested, board.Flip());
        }
        [TestMethod]
        public void Rev2Test()
        {
            uint reversed = AllWhite.Reverse();
            Debug.WriteLine(reversed.ToString("b"));
            Debug.WriteLine(AllBlack.ToString("b"));
            Assert.AreEqual(AllBlack, reversed);
        }
        
    }
}
