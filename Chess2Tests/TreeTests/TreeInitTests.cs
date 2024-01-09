using Chess2.Modules;
using Chess2;
using System.Numerics;

namespace Chess2Tests.TreeTests;

[TestClass]
public class TreeInitTests
{
    [TestMethod]
    public void Init1()
    {
        CheckersBoard board = new(UintHelper.AllWhite, 0, UintHelper.AllBlack, 0);
        Tree t = new(null, board, true);
        Tree.MaxDeep = 1 / 512f;
        t.Init();
        //Console.WriteLine(t.GetArrayForNeuro().Count());
        Console.WriteLine(Tree.Leaves.Count);
        //var first = Tree.Leaves.First();
        foreach (var node in t.Forward().Skip(1))
        {
            string who = node.ImPlayer ? "Противник сходил" : "Я сходил";
            //CheckersBoard b = node.ImPlayer ? node.Board : node.Board.Reverse();
            //var whites = from number in UintHelper.Moves(b.Whites)
            //             let index = BitOperations.Log2(number)
            //             select index;
            //var blacksIds = from number in UintHelper.Moves(b.Blacks)
            //                let index = BitOperations.Log2(number)
            //                select index;

            //Console.WriteLine("Whites: {0}", string.Join(",", whites));
            //Console.WriteLine("Blacks: {0}", string.Join(",", blacksIds));
            // ИНТЕРЕСУЕТ, КАК ХОДИЛ ПРЕДЫДУЩИЙ ИГРОК
            //int oldPos, newPos, dead;
            //if (node.ImPlayer)
            //{
            //    var rev = node.ParentTree.Board.Reverse();
            //    oldPos = BitOperations.Log2(rev.Blacks & ~node.Board.Blacks);
            //    newPos = BitOperations.Log2(node.Board.Blacks & ~rev.Blacks);
            //    dead = BitOperations.Log2(rev.Whites & ~node.Board.Whites);
            //}
            //else
            //{
            //    var rev = node.Board.Reverse();
            //    oldPos = BitOperations.Log2(node.ParentTree.Board.Whites & ~rev.Whites);
            //    newPos = BitOperations.Log2(rev.Whites & ~node.ParentTree.Board.Whites);
            //    dead = BitOperations.Log2(node.ParentTree.Board.Blacks & ~rev.Blacks);
            //}
            //if (dead != 0)
            //{
            //    Console.WriteLine($"{who}: {oldPos}-{newPos}-{dead}, deep={node.Deep}");
            //}
            //Console.WriteLine($"{who}: {oldPos}-{newPos}-{dead}, deep={node.Deep}");

        }
    }
}
