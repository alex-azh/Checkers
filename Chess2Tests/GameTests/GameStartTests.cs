using CheckersGame;
using CheckersGame.Evaluaters;
using CheckersGame.GameSpace;

namespace CheckersTests.GameTests
{
    [TestClass]
    public class GameStartTests
    {
        [TestMethod]
        public void Test2()
        {
            Board b = new(UintHelper.CreateNumber(0), 0, UintHelper.CreateNumber(5, 6, 7, 8, 9, 10, 13), UintHelper.CreateNumber(1, 2, 3));
            IEnumerable<(Board board, bool reversed)> moves = new Game(new ComputerPlayer(), new ComputerPlayer()) { CheckersBoard = b }.Start();
            int i = 0;
            foreach ((Board board, bool reversed) m in moves) { i++; }
        }
        [TestMethod]
        public void Test1()
        {
            uint StartWhites = 0b0000_0000_0000_0110_0011_1010_1111_1001;
            uint StartBlacks = 0b0110_0111_1011_1001_1100_0000_0000_0000;
            Board b = new(StartWhites, 0, StartBlacks, 0);
            Evaluater evaluater = new Evaluater(new RandomPredictor());
            _ = evaluater.GetBestMove(b, 4);
        }
        [TestMethod]
        public void StartTest()
        {
            ComputerPlayer p1 = new(), p2 = new();
            Game game = new Game(p1, p2);
            IEnumerable<(Board board, bool reversed)> result = game.Start();
            List<(Board board, bool reversed)> moves3 = result.Take(3).ToList();
            (Board board, bool reversed) first = moves3[0]; //white сходил
            (int fromPos, int toPos, int killedPos) m1 = Board.WhoMovedWhites(Board.NewBoard(), moves3[0].board);
            (Board board, bool reversed) second = moves3[1]; //черный сходил (реверснута)
            Assert.AreEqual(first.board.Whites, second.board.Flip().Whites);
            (Board board, bool reversed) three = moves3[2]; // white сходил
            Assert.AreEqual(second.board.Flip().Blacks, three.board.Blacks);
            //(int fromPos, int toPos, int killedPos) m2 = Board.WhoMoved(moves3[1].board, moves3[2].board);

            //Debug.WriteLine(cnt);
            //Debug.WriteLine(game.MovesWhithoutKillsCount);
        }
        [TestMethod]
        public void RunnerTest()
        {
            ComputerPlayer player1 = new(new Evaluater(new ModelPredictor())),
                player2 = new(new Evaluater(new ModelPredictor()));
            Game game = new Game(player1, player2);
            IEnumerable<(Board board, bool reversed)> moves = game.Start();
            Console.WriteLine(moves.Count());
        }
        [TestMethod]
        public void RunnerTest2()
        {
            ComputerPlayer player1 = new(new Evaluater(new RandomPredictor())),
                player2 = new(new Evaluater(new RandomPredictor()));
            Game game = new Game(player1, player2);
            IEnumerable<(Board board, bool reversed)> moves = game.Start();
            Console.WriteLine(moves.Count());
        }
    }
}
