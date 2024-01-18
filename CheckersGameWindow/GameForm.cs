using CheckersGame;
using CheckersGame.GameSpace;

namespace CheckersGameWindow
{
    public static class ColorsPallete
    {
        public static Color Cell = Color.DarkGray;
        public static Color WhiteCell = Color.White;
        public static Color DeletedFigure = Color.DarkRed;
        public static Color StartPosition = Color.DarkBlue;
        public static Color EndPosition = Color.Blue;
        public static Color Black = Color.Black;
        public static Color White = Color.White;
    }
    public partial class GameForm : Form
    {
        List<Button> btns = new();
        IEnumerator<(Board, bool)> _moves;
        Board _lastBoard;
        List<Button> _lightsBtns = new();

        public GameForm()
        {
            InitializeComponent();
        }
        public void InitBoard()
        {
            int top = 350;
            int left = 10;
            int btnSize = 50;
            Dictionary<Color, Color> colors = new() { { ColorsPallete.Cell, ColorsPallete.WhiteCell }, { ColorsPallete.WhiteCell, ColorsPallete.Cell } };
            Color lastColor = Color.DarkGray;
            int tabIndex = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Button button = new()
                    {
                        BackColor = lastColor,
                        Left = left,
                        Top = top,
                        Height = btnSize,
                        Width = btnSize,
                        Font = new Font("Arial", 13),
                        Enabled = false,
                        TabIndex = int.MaxValue
                    };
                    if (lastColor == ColorsPallete.Cell)
                    {
                        button.TabIndex = tabIndex++;
                        button.Enabled = true;
                        btns.Add(button);
                    }
                    button.Click += (object sender, EventArgs e) => SendPositionIndex(((Button)sender).TabIndex);
                    lastColor = colors[lastColor];
                    this.Controls.Add(button);
                    left += btnSize;
                }
                lastColor = colors[lastColor];
                left = 10;
                top -= btnSize;
            }
        }

        private void SendPositionIndex(int tabIndex)
        {
        }

        void ShowBoard(Board board)
        {
            foreach (Button btn in btns) btn.Text = string.Empty;
            foreach (int index in board.WhiteP.IndexesOfOnesPositions())
            {
                btns[index].Text = "C";
                btns[index].ForeColor = ColorsPallete.White;
            }
            foreach (int index in board.WhiteD.IndexesOfOnesPositions())
            {
                btns[index].Text = "Q";
                btns[index].ForeColor = ColorsPallete.White;
            }
            foreach (int index in board.BlackP.IndexesOfOnesPositions())
            {
                btns[index].Text = "C";
                btns[index].ForeColor = ColorsPallete.Black;
            }
            foreach (int index in board.BlackD.IndexesOfOnesPositions())
            {
                btns[index].Text = "Q";
                btns[index].ForeColor = ColorsPallete.Black;
            }
        }
        void ChangeBoard(int fromPos, int toPos, int deletedPos)
        {
            UpdateLightsBtns();
            btns[toPos].Text = (toPos > 27 || toPos < 4) ? "Q" : btns[fromPos].Text;
            btns[toPos].ForeColor = btns[fromPos].ForeColor;
            btns[fromPos].Text = string.Empty;
            btns[fromPos].BackColor = Color.DarkBlue;
            btns[toPos].BackColor = Color.Blue;

            if (deletedPos > 0)
            {
                btns[deletedPos].Text = string.Empty;
                btns[deletedPos].BackColor = Color.DarkRed;
                _lightsBtns.Add(btns[deletedPos]);
            }
            _lightsBtns.Add(btns[toPos]);
            _lightsBtns.Add(btns[fromPos]);
            void UpdateLightsBtns()
            {
                foreach (Button btn in _lightsBtns)
                {
                    btn.BackColor = ColorsPallete.Cell;
                }
                _lightsBtns.Clear();
            }
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            InitBoard();
            Game game = new Game(new ComputerPlayer(), new ComputerPlayer());
            ShowBoard(game.CheckersBoard);
            _lastBoard = game.CheckersBoard;
            _moves = game.Start().GetEnumerator();
            //foreach ((Board board, bool reversed) in game.Start())
            //{
            //    await Task.Delay(1200);
            //    //Thread.Sleep(500);
            //    ShowBoard(reversed ? board.Reverse() : board);
            //}
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            if (_moves.MoveNext())
            {
                (Board board, bool reversed) = _moves.Current;
                // _lastBoard не реверснута!
                if (reversed)
                {
                    Board presentedBoard = board.Flip();
                    // ходил противник
                    (int fromPos, int toPos, int deletedPos) = Board.WhoMovedBlacks(_lastBoard, presentedBoard);
                    ChangeBoard(fromPos, toPos, deletedPos);
                    _lastBoard = presentedBoard;
                }
                else
                {
                    Board presentedBoard = board;
                    // ходил белый игрок
                    (int fromPos, int toPos, int deletedPos) = Board.WhoMovedWhites(_lastBoard, presentedBoard);
                    ChangeBoard(fromPos, toPos, deletedPos);
                    _lastBoard = presentedBoard;
                }
            }
        }
    }
}
