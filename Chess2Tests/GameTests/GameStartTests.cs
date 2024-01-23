using CheckersGame;
using CheckersGame.GameSpace;
using CheckersGame.Evaluaters;
using System.Diagnostics;

namespace CheckersTests.GameTests
{
    [TestClass]
    public class GameStartTests
    {
        [TestMethod]
        public void Test2()
        {
            Board b = new(UintHelper.CreateNumber(0), 0, UintHelper.CreateNumber(5, 6, 7, 8, 9, 10, 13), UintHelper.CreateNumber(1, 2, 3));
            var moves = new Game(new ComputerPlayer(), new ComputerPlayer()) { CheckersBoard = b }.Start();
            int i = 0;
            foreach (var m in moves) { i++; }
        }
        [TestMethod]
        public void Test1()
        {
            uint StartWhites = 0b0000_0000_0000_0110_0011_1010_1111_1001;
            uint StartBlacks = 0b0110_0111_1011_1001_1100_0000_0000_0000;
            Board b = new(StartWhites, 0, StartBlacks, 0);
            var evaluater = new Evaluater(new RandomPredictor());
            _ = evaluater.GetBestMove(b, 4);
        }
        [TestMethod]
        public void StartTest()
        {
            ComputerPlayer p1 = new(), p2 = new();
            var game = new Game(p1, p2);
            IEnumerable<(Board board, bool reversed)> result = game.Start();
            var moves3 = result.Take(3).ToList();
            var first = moves3[0]; //white сходил
            (int fromPos, int toPos, int killedPos) m1 = Board.WhoMovedWhites(Board.NewBoard(), moves3[0].board);
            var second = moves3[1]; //черный сходил (реверснута)
            Assert.AreEqual(first.board.Whites, second.board.Flip().Whites);
            var three = moves3[2]; // white сходил
            Assert.AreEqual(second.board.Flip().Blacks, three.board.Blacks);
            //(int fromPos, int toPos, int killedPos) m2 = Board.WhoMoved(moves3[1].board, moves3[2].board);

            //Debug.WriteLine(cnt);
            //Debug.WriteLine(game.MovesWhithoutKillsCount);
        }
    }
}
