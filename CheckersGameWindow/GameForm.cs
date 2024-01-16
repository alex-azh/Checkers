using CheckersGame;
using CheckersGame.GameSpace;

namespace CheckersGameWindow
{
    public partial class GameForm : Form
    {
        List<Button> btns = new();
        IEnumerable<(Board, bool)> _moves;
        public GameForm()
        {
            InitializeComponent();
        }
        public void InitBoard()
        {
            int top = 350;
            int left = 10;
            int btnSize = 50;
            Dictionary<Color, Color> colors = new() { { Color.DarkGray, Color.White }, { Color.White, Color.DarkGray } };
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
                    if (lastColor == Color.DarkGray)
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
                btns[index].ForeColor = Color.White;
            }
            foreach (int index in board.WhiteD.IndexesOfOnesPositions())
            {
                btns[index].Text = "Q";
                btns[index].ForeColor = Color.White;
            }
            foreach (int index in board.BlackP.IndexesOfOnesPositions())
            {
                btns[index].Text = "C";
                btns[index].ForeColor = Color.Black;
            }
            foreach (int index in board.BlackD.IndexesOfOnesPositions())
            {
                btns[index].Text = "Q";
                btns[index].ForeColor = Color.Black;
            }
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            InitBoard();
            Game game = new Game(new ComputerPlayer(), new ComputerPlayer());
            ShowBoard(game.CheckersBoard);
            _moves = game.Start();
            //foreach ((Board board, bool reversed) in game.Start())
            //{
            //    await Task.Delay(1200);
            //    //Thread.Sleep(500);
            //    ShowBoard(reversed ? board.Reverse() : board);
            //}
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var enumerator = _moves.GetEnumerator();
            if (enumerator.MoveNext())
            {
                // 9- 12 white
                (Board board, bool reversed) = enumerator.Current;
                string whitePoses = string.Join(",", board.Whites.IndexesOfOnesPositions());
                string blacksPoses = string.Join(",", board.Blacks.IndexesOfOnesPositions());
                ShowBoard(reversed ? board.Reverse() : board);
            }
        }
    }
}
