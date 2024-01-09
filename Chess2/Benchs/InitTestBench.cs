using BenchmarkDotNet.Attributes;
using Chess2.Modules;
using System.Collections.Concurrent;

/*
| Method             | Mean       | Error     | StdDev   | Gen0     | Gen1    | Allocated  |
|------------------- |-----------:|----------:|---------:|---------:|--------:|-----------:|
| InitParallel2_54   |   115.6 us | 116.05 us |  6.36 us |   8.5449 |  4.1504 |   74.15 KB |
| InitParallel2_2048 |   135.2 us | 197.80 us | 10.84 us |  11.5967 |  5.6152 |   99.99 KB |
| InitParallel2_512  |   190.0 us | 180.59 us |  9.90 us |  16.1133 |  7.8125 |  140.35 KB |
| InitParallel_54    |   206.4 us |  56.84 us |  3.12 us |  19.5313 |  6.3477 |  165.86 KB |
| Init_54            |   242.8 us |  68.16 us |  3.74 us |  19.0430 |  9.2773 |  157.32 KB |
| InitParallel_512   | 1,567.6 us | 220.04 us | 12.06 us | 144.5313 | 46.8750 | 1282.75 KB |
| InitParallel_2048  | 1,588.5 us | 670.73 us | 36.77 us | 144.5313 | 46.8750 | 1238.77 KB |
| Init_512           | 1,746.1 us | 525.69 us | 28.81 us | 144.5313 | 46.8750 | 1204.12 KB |
 */
namespace Chess2.Benchs
{
    [MemoryDiagnoser]
    [ShortRunJob]
    [BenchmarkDotNet.Attributes.Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    public class InitTestBench
    {
        static void Init(Tree tree)
        {
            Tree.CreateLevel(tree);
            foreach (var node in tree.Nodes)
            {
                node.Deep = node.ParentTree.Deep;
                ConcurrentStack<Tree> stack = new();
                stack.Push(node);
                while (stack.TryPop(out Tree poped))
                {
                    Tree.CreateLevel(poped); // при этом у текущего poped обновился Deep
                    foreach (var n in poped.Nodes)
                    {
                        n.Deep = poped.Deep;
                        stack.Push(n);
                    }
                }
            }
        }
        static void InitParallel(Tree tree)
        {
            Tree.CreateLevel(tree);
            Parallel.ForEach(tree.Nodes, node =>
            {
                node.Deep = node.ParentTree.Deep;
                ConcurrentStack<Tree> stack = new();
                stack.Push(node);
                while (stack.TryPop(out Tree poped))
                {
                    Tree.CreateLevel(poped); // при этом у текущего poped обновился Deep
                    foreach (var n in poped.Nodes)
                    {
                        n.Deep = poped.Deep;
                        stack.Push(n);
                    }
                }
            });
        }
        static void InitParallel2(Tree tree)
        {
            Tree.CreateLevel(tree);
            List<Task> tasks = new();
            foreach (var node in tree.Nodes)
            {
                tasks.Add(Task.Factory.StartNew(() => Method(node)));
            }
            Task.WhenAll(tasks);

            static void Method(Tree node)
            {
                node.Deep = node.ParentTree.Deep;
                ConcurrentStack<Tree> stack = new();
                stack.Push(node);
                while (stack.TryPop(out Tree poped))
                {
                    Tree.CreateLevel(poped); // при этом у текущего poped обновился Deep
                    foreach (var n in poped.Nodes)
                    {
                        n.Deep = poped.Deep;
                        stack.Push(n);
                    }
                }
            }
        }
        static CheckersBoard board = new(UintHelper.AllWhite, 0, UintHelper.AllBlack, 0);

        [Benchmark]
        public void Init_54()
        {
            Tree t = new(null, board, true);
            Tree.MaxDeep = 1 / 54f;
            Init(t);
        }
        [Benchmark]
        public void InitParallel_54()
        {
            Tree t = new(null, board, true);
            Tree.MaxDeep = 1 / 54f;
            InitParallel(t);
        }
        [Benchmark]
        public void InitParallel2_54()
        {
            Tree t = new(null, board, true);
            Tree.MaxDeep = 1 / 54f;
            InitParallel2(t);
        }
        [Benchmark]
        public void Init_512()
        {
            Tree t = new(null, board, true);
            Tree.MaxDeep = 1 / 512f;
            Init(t);
        }
        [Benchmark]
        public void InitParallel_512()
        {
            Tree t = new(null, board, true);
            Tree.MaxDeep = 1 / 512f;
            InitParallel(t);
        }
        [Benchmark]
        public void InitParallel2_512()
        {
            Tree t = new(null, board, true);
            Tree.MaxDeep = 1 / 512f;
            InitParallel2(t);
        }

        [Benchmark]
        public void InitParallel_2048()
        {
            Tree t = new(null, board, true);
            Tree.MaxDeep = 1 / 2048f;
            InitParallel(t);
        }
        [Benchmark]
        public void InitParallel2_2048()
        {
            Tree t = new(null, board, true);
            Tree.MaxDeep = 1 / 2048f;
            InitParallel2(t);
        }
    }
}
