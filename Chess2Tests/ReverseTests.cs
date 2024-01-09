using CheckersGame;

namespace CheckersTests
{
    [TestClass]
    public class ReverseTests
    {
        public static uint AllBlack = 0b11_11111_11111_00000_00000_00000_00000;
        public static uint AllWhite = 0b00_00000_00000_00000_00011_11111_11111;

        [TestMethod]
        public void Rev1Test()
        {
            Board board = new(UintHelper.CreateNumber(0, 1, 2), 0, UintHelper.CreateNumber(20, 21), 0),
                tested = new(UintHelper.CreateNumber(10, 11), 0, UintHelper.CreateNumber(31, 30, 29), 0);
            Assert.AreEqual(tested, board.Reverse());
        }
    }
}
