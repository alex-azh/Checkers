using CheckersGame;
using CheckersGame.GameSpace;

namespace CheckersGameWindow;

public partial class GameForm : Form
{
    List<Button> btns = new();
    IEnumerator<Board> _moves;
    Board _lastBoard;
    Board _currentBoard;
    bool _reversed = false;

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
        btns[fromPos].BackColor = ColorsPallete.StartPosition;
        btns[toPos].BackColor = ColorsPallete.EndPosition;

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
        _currentBoard = game.CheckersBoard;
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
            _lastBoard = _currentBoard;
            _currentBoard = _reversed ? _moves.Current.Flip() : _moves.Current;
            ShowBoard(_currentBoard);
            _reversed = !_reversed;
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        var form = new GameForm();
        form.Show();
        form.ShowBoard(_lastBoard);
    }
}
