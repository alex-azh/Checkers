using System.Collections.Concurrent;

namespace Chess2.Modules
{
    public record Tree(Tree ParentTree, CheckersBoard Board, bool ImPlayer)
    {
        public static ConcurrentBag<Tree> Leaves = new();
        public static float MaxDeep = 1;
        public List<Tree> Nodes { get; private set; } = new();
        public float Evaluation { get; private set; }
        public float Deep { get; set; } = 1f;
        public bool CanContinue => (Deep > MaxDeep)
            &&
            // и не "кто-то" выиграл
            !(Board.Whites == 0 || Board.Blacks == 0);

        public static void CreateLevel(Tree tree)
        {
            if (!tree.CanContinue)
            {
                Leaves.Add(tree);
                return;
            }
            var boards = tree.Board.Variants().Select(x => x.Reverse());
            foreach (CheckersBoard board in boards)
            {
                tree.Nodes.Add(new(ParentTree: tree, Board: board, ImPlayer: !tree.ImPlayer));
            }
            tree.Deep /= tree.Nodes.Count;
        }
      
        public void Init()
        {
            CreateLevel(this);
            List<Task> tasks = new();
            foreach (var node in Nodes)
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
                    CreateLevel(poped); // при этом у текущего poped обновился Deep
                    foreach (var n in poped.Nodes)
                    {
                        n.Deep = poped.Deep;
                        stack.Push(n);
                    }
                }
            }
        }

        public IEnumerable<Tree> Forward()
        {
            Stack<Tree> stack = new();
            stack.Push(this);
            while (stack.Count > 0)
            {
                Tree poped = stack.Pop();
                yield return poped;
                foreach (Tree tree in poped.Nodes)
                {
                    stack.Push(tree);
                }
            }
        }

        public bool[][] GetArrayForNeuro() =>
            Forward().Select(tree => UintHelper.GetBoolArray(tree.Board)).ToArray();
    }
}
