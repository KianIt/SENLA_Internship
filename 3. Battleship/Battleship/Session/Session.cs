using Battleship.Models;
using Battleship.Players;
using Battleship.Settings;
using Battleship.Exceptions;

namespace Battleship {
    public class Session {
        private SessionSettings Settings { get; init; }
        private UserPlayer User { get; set; } = null!;
        private AIPlayer AI { get; set; } = null!;
        private SessionStatus Status { get; set; } = SessionStatus.Stopped;
        internal Session(SessionSettings settings) {
            Settings = settings;
        }
        public void Start() {
            CheckSessionStopped();
            InitializePlayers();
            SetStatusStarted();
        }
        private void InitializePlayers() {
            User = new UserPlayer(Settings);
            AI = new AIPlayer(Settings);
        }
        public void Stop() {
            CheckSessionStarted();
            SetStatusStop();
        }
        public bool IsStarted() => Status == SessionStatus.Started;
        public bool IsStopped() => !IsStarted();
        public bool IsConfigured() => IsStarted() && User.IsConfigured();
        public int GetNextShipDeck() {
            CheckSessionStarted();
            return User.GetNextShipDeck();
        }
        public void PlaceNextShip(int decks, int row, int column, int direction) {
            CheckSessionStarted();
            var cell = new FieldCell(row, column);
            var shipDirection = (ShipDirection) direction;
            var ship = new Ship(decks, cell, shipDirection);
            User.PlaceNextShip(ship);
        }
        public Move MakeUserMove(int row, int column) {
            CheckSessionStarted();
            var cell = new FieldCell(row, column);
            return AI.MakeMove(cell);
        }
        public Move MakeAIMove() {
            CheckSessionStarted();
            var cell = User.FindOptimalCell();
            return User.MakeMove(cell);
        }
        private void SetStatusStarted() {
            Status = SessionStatus.Started;
        }
        private void SetStatusStop() {
            Status = SessionStatus.Stopped;
        }
        private void CheckSessionStarted() {
            if (IsStopped()) throw new StoppedSessionActionException();
        }
        private void CheckSessionStopped() {
            if (IsStarted()) throw new StartedSessionActionException();
        }
    }
}
