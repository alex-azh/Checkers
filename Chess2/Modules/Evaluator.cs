namespace Chess2.Modules
{
    public interface IPredictor
    {
        bool[][] PredictValues();
    }
    /// <summary>
    /// Оценивает каждую доску как вектор. Предполагается, что оценивает несколько досок из дерева решений.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="Worker">ML model for predict.</param>
    public record Evaluator(IPredictor Worker);

    /* Алгоритим:
     * 1. Проверить, можно ли дальше.
     * 2. Получить ход из данной точки.
     * 3. Для него записать глубину в данной точке.
     */

    public static class Constants
    {
        public static float DeepControl = 1f / 32;
    }

    public record Tree(CheckersBoard Board, bool ImPlayer)
    {
        public List<Tree> Nodes { get; private set; } = new();
        public float Cost { get; private set; }

        public float Deep { get; private set; }
        public bool CanContinue => (Deep >= Constants.DeepControl) &&
            Board.WhiteP * Board.WhiteD != 0;

        public void Create()
        {
            if (!CanContinue) return;
            var boards = Board.Variants();
            int count = 0;
            foreach (CheckersBoard board in boards)
            {
                Nodes.Add(new(board.Reverse(), !ImPlayer));
                count++;
            }
            float deep = Deep / count;
            foreach (Tree node in Nodes)
            {
                //Board.BoolArray();
                node.Deep = deep;
                node.Create();
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

        public void ContinueTree()
        {
            foreach (Tree node in Forward())
            {
                if (node.Nodes.Count != 0)
                {
                    continue;
                }
                // в листе находимся
                node.Deep /= Deep;
                node.Create();
                // массивы для нейронки
                bool[][] array = GetArrayForNeuro();

            }
        }
        public bool[][] GetArrayForNeuro() =>
            //from tree in Forward()
            //select UintHelper.GetBoolArray(tree.Board)
            Forward().Select(tree => UintHelper.GetBoolArray(tree.Board)).ToArray();
    }
}
