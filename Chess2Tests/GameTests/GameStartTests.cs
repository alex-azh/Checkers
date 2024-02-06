using CheckersGame;
using CheckersGame.Evaluaters;
using CheckersGame.GameSpace;

namespace CheckersTests.GameTests
{
    [TestClass]
    public class GameStartTests
    {
        //[TestMethod]
        //public void Test2()
        //{
        //    Board b = new(UintHelper.CreateNumber(0), 0, UintHelper.CreateNumber(5, 6, 7, 8, 9, 10, 13), UintHelper.CreateNumber(1, 2, 3));
        //    var moves = new Game(new ComputerPlayer(), new ComputerPlayer()) { CheckersBoard = b }.Start();
        //    int i = 0;
        //    foreach (var m in moves) { i++; }
        //}
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
        public void RunnerTest()
        {
            ComputerPlayer player1 = new(new Evaluater(new ModelPredictor())),
                player2 = new(new Evaluater(new ModelPredictor()));
            Game game = new Game(player1, player2);
            _ = game.Start();
            Console.WriteLine(player1.Moves.Count + player2.Moves.Count);
        }
        [TestMethod]
        public void RunnerTest2()
        {
            ComputerPlayer player1 = new(new Evaluater(new RandomPredictor())),
                player2 = new(new Evaluater(new RandomPredictor()));
            Game game = new Game(player1, player2);
            var moves = game.Start();
            Console.WriteLine(player1.Moves.Count + player2.Moves.Count);
        }
        [TestMethod]
        public void RunnerTest3()
        {
            ComputerPlayer player1 = new(new Evaluater(new RandomPredictor())),
                player2 = new(new Evaluater(new RandomPredictor()));
            Game game = new Game(player1, player2);
            var moves = game.Start();
            Console.WriteLine(player1.Moves.Count + player2.Moves.Count);
        }
    }
}
