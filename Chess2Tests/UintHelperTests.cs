using Microsoft.VisualStudio.TestTools.UnitTesting;
using Chess2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using Chess2Tests.Benchmarks;

namespace Chess2.Tests
{
    [TestClass()]
    public class UintHelperTests
    {
        [TestMethod()]
        public void GetBitArrayTest()
        {
            var board = new CheckersBoard(0b01010011, 0b100, 0, 0);//0b100, 0b01001, 0b101);
            var actual = GetBoolArrayBench.GetBoolArray(board);
            var actual2 = UintHelper.GetBoolArray(board);
            CollectionAssert.AreEqual(actual, actual2);
        }
        [TestMethod()]
        public void Benchs()
        {
            BenchmarkRunner.Run<GetBoolArrayBench>();
        }
    }
}