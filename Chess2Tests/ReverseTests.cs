using Chess2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess2Tests
{
    [TestClass]
    public class ReverseTests
    {
        public static uint AllBlack = 0b11_11111_11111_00000_00000_00000_00000;
        public static uint AllWhite = 0b00_00000_00000_00000_00011_11111_11111;

        [TestMethod]
        public void Rev1()
        {
            /*
             *     00:00:00.0000561
             *     00:00:00.0004679
             */
            var stop = Stopwatch.StartNew();
            Assert.AreEqual(AllWhite, UintHelper.Reverse(AllBlack));
        }
    }
}
