using CheckersGame;
using System.Diagnostics;
using CheckersGame.GameSpace;

namespace CheckersTests.GameTests
{
    [TestClass]
    public class GameStartTests
    {
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
