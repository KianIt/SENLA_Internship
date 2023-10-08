using Battleship;
using Battleship.Models;
using BattleshipGUI.Models;
using BattleshipGUI.Settings;

namespace BattleshipGUI {
    public partial class BattleshipForm : Form {
        private Button StartButton { get; set; } = null!;
        private Button StopButton { get; set; } = null!;
        private FlowLayoutPanel UserPanel { get; set; } = null!;
        private FlowLayoutPanel AIPanel { get; set; } = null!;
        private Button[,] UserButtons { get; set; } = null!;
        private Button[,] AIButtons { get; set; } = null!;
        private Label MessageLabel { get; set; } = null!;
        private FieldView UserView { get; set; } = null!;
        private FieldView AIView { get; set; } = null!;
        private Session GameSession { get; set; } = null!;
        private System.Windows.Forms.Timer GameTimer { get; set; } = null!;
        private bool IsConfigured { get; set; }
        private bool IsNextShipGot { get; set; }
        private int NextShipDecks { get; set; }
        private bool IsUsersTurn { get; set; }
        private bool TemporaryMessageSent { get; set; }
        private string OldMessage { get; set; } = null!;
        private bool MessageSent { get; set; }
        public BattleshipForm() {
            InitializeForm();
            InitializeGameTimer();
            InitializeGameSession();
        }
        private async void InitializeGameSession() {
            GameSession = await Task.Run(() => { return Game.CreateSession(); });
            StartButton.Click += StartSession!;
            StopButton.Click += StopSession!;
        }
        private async void StartSession(Object sender, EventArgs args) {
            UserView = new FieldView(UserButtons);
            AIView = new FieldView(AIButtons);
            await Task.Run(() => { GameSession.Start(); });
            UserView.Enable();
            UserView.Clickable();
            AIView.Disable();
            AIView.Disclickabe();
            GameTimer.Start();
            IsConfigured = false;
            IsNextShipGot = false;
            IsUsersTurn = true;
            TemporaryMessageSent = false;
            MessageSent = false;
        }
        private void StopSession(Object sender, EventArgs args) {
            GameTimer.Stop();
            GameSession.Stop();
            UserView.Disclickabe();
            AIView.Disclickabe();
        }
        private void InitializeGameTimer() {
            GameTimer = new System.Windows.Forms.Timer();
            GameTimer.Interval = 100;
            GameTimer.Tick += TimerTick!;
        }
        private void TimerTick(Object sender, EventArgs args) {
            if (TemporaryMessageSent) {
                RestoreTemporaryMessage();
            }

            bool isSessionConfigured = GameSession.IsConfigured();;
            if (!isSessionConfigured)
                if (!IsNextShipGot)
                    GetNextShipDeck();
                else
                    PlaceNextShip();
            else {
                if (!IsConfigured) {
                    SendTimelessMessage("User's turn. Choose an AI's cell");
                    EndConfigurating();
                }
                PerformNextMove();
            }
        }
        private void EndConfigurating() {
            IsConfigured = true;
            UserView.Disclickabe();
            AIView.Enable();
            AIView.Clickable();
        }
        private void GetNextShipDeck() {
            NextShipDecks = GameSession.GetNextShipDeck();;
            IsNextShipGot = true;
            SendTimelessMessage($"Place a {NextShipDecks}-ship. Choose two cells for the ship");
        }
        private void PlaceNextShip() {
            if (UserView.Clicked.Count < 2) return;

            var cell1 = UserView.Clicked[0];
            var cell2 = UserView.Clicked[1];

            if (AreCellsNotOnOneLine(cell1, cell2)) {
                SendTemporaryMessage("Bad cells chosen. Try again");
                UserView.Clicked.Clear();
                return;
            }

            (int row, int column, int direction) =
                GetShipFromTwoCells(cell1, cell2);

            try {
                GameSession.PlaceNextShip(NextShipDecks, row, column, direction);
            }
            catch {
                SendTemporaryMessage("Bad ship placement. Try again");
                return;
            }
            finally {
                UserView.Clicked.Clear();
            }

            UserView.Ship(cell1, cell2, NextShipDecks);

            IsNextShipGot = false;
        }
        private bool AreCellsNotOnOneLine(Cell cell1, Cell cell2) =>
            AreCellsOnDifferentLines(cell1, cell2) ||
                AreCellsEqual(cell1, cell2);
        private bool AreCellsOnDifferentLines(Cell cell1, Cell cell2) {
            return cell1.Row != cell2.Row && cell1.Column != cell2.Column;
        }
        private bool AreCellsEqual(Cell cell1, Cell cell2) {
            return cell1.Row == cell2.Row && cell1.Column == cell2.Column;
        }
        private (int row, int column, int direction)
            GetShipFromTwoCells(Cell cell1, Cell cell2) {
            int row, column, direction;
            row = cell1.Row;
            column = cell1.Column;
            if (cell1.Row == cell2.Row) {
                if (cell1.Column < cell2.Column) {
                    direction = 3;
                }
                else {
                    direction = 1;
                }
            }
            else {
                if (cell1.Row < cell2.Row) {
                    direction = 2;
                }
                else {
                    direction = 0;
                }
            }
            return (row, column, direction);
        }
        private void PerformNextMove() {
            if (IsUsersTurn) {
                PerformUserMove();
            }
            else {
                PerformAIMove();
            }
        }
        private void PerformUserMove() {
            SendTimelessMessage("User's turn. Choose a AI's cell");
            if (AIView.Clicked.Count < 1) return;

            Move move;
            try {
                var clickedCell = AIView.Clicked[0];
                AIView.Clicked.Clear();
                move = GameSession.MakeUserMove(clickedCell.Row, clickedCell.Column);
            }
            catch {
                SendTemporaryMessage("Bad move. Try again");
                return;
            }

            IsUsersTurn = !IsUsersTurn;

            Cell cell =  new Cell(move.Cell.Row, move.Cell.Column);
            if (move.IsMiss()) {
                AIView.Miss(cell);
                SendTemporaryMessage("MISS :(");
            }
            else {
                AIView.Hit(cell);
                if (move.IsHit()) {
                    SendTemporaryMessage("HIT!");
                }
                else if (move.IsKill()) {
                    SendTemporaryMessage("KILL!!");
                }
                else {
                    SendTemporaryMessage("WIN!!! User WINS!!!");
                    StopButton.PerformClick();
                }
            }         
        }

        private void PerformAIMove() {
            if (!MessageSent) {
                SendTimelessMessage("AI's turn. Choose a User's cell");
                MessageSent = true;
                return;
            }
            else {
                Thread.Sleep(500);
            }

            Move move;
            try {
                move = GameSession.MakeAIMove();
            }
            catch {
                SendTemporaryMessage("Bad move. Try again");
                return;
            }

            IsUsersTurn = !IsUsersTurn;

            Cell cell = new Cell(move.Cell.Row, move.Cell.Column);
            if (move.IsMiss()) {
                UserView.Miss(cell);
                SendTemporaryMessage("MISS :(");
            }
            else {
                UserView.Hit(cell);
                if (move.IsHit()) {
                    SendTemporaryMessage("HIT!");
                }
                else if (move.IsKill()) {
                    SendTemporaryMessage("KILL!!");
                }
                else {
                    SendTemporaryMessage("WIN!!! AI WINS!!!");
                    StopButton.PerformClick();
                    return;
                }
            }

            MessageSent = false;
        }

        private void SendTimelessMessage(string message) {
            MessageLabel.Text = message;
        }
        private void SendTemporaryMessage(string newMessage) {
            OldMessage = MessageLabel.Text;
            MessageLabel.Text = newMessage;
            TemporaryMessageSent = true;
        }
        private void RestoreTemporaryMessage() {
            Thread.Sleep(500);
            MessageLabel.Text = OldMessage;
            TemporaryMessageSent = false;
        }
    }
}
