using CheckersGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IEnumerable<Board> result = game.Start();
            int cnt = 0;
            foreach (var b in result)
            {
                string whitePoses = string.Join(",", b.Whites.IndexesOfOnesPositions());
                string blacksPoses = string.Join(",", b.Blacks.IndexesOfOnesPositions());
                cnt++;
            };
            Debug.WriteLine(cnt);
            Debug.WriteLine(game.MovesWhithoutKillsCount);
        }
    }
}
