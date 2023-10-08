using Battleship.Models;
using Battleship.Settings;
using Battleship.Players.Exceptions;

namespace Battleship.Players {
    internal abstract class AbstractPlayer {
        protected static void EraseFromDict(Dictionary<int, int> dict, int key) {
            if (dict[key] > 0) dict[key]--;
            if (dict[key] == 0) dict.Remove(key);
        }
        private Field PlayerField { get; set; }
        private Dictionary<int, int> Ships { get; set; }
        protected AbstractPlayer(SessionSettings settings) {
            PlayerField = new Field(settings.FieldHeight, settings.FieldWidth);
            Ships = new Dictionary<int, int>(settings.ShipDict);
        }
        public abstract Move MakeMove(FieldCell cell);
        protected void PlaceShip(Ship ship) {
            PlayerField.PlaceShip(ship);
        }
        // Chooses a proper handle method
        protected MoveResponse HandleCell(FieldCell cell) {
            switch (PlayerField.GetMark(cell)) {
                case FieldMark.Ship:
                    return HandleHit(cell);
                case FieldMark.Empty:
                    return HandleMiss(cell);
                default:
                    throw new RepeatedMoveException();
            }
        }
        protected FieldCell? GetAroundAvailableCell(FieldCell cell, bool row = false, bool column = false) {
            return PlayerField.GetAroundAvailableCell(cell, row, column);
        }
        protected FieldCell GetRandomAvailableCell() {
            return PlayerField.GetRandomAvailableCell();
        }
        protected int GetHeight() {
            return PlayerField.Height;
        }
        protected int GetWidht() {
            return PlayerField.Width;
        }
        private MoveResponse HandleHit(FieldCell cell) {
            PlayerField.SetMark(cell, FieldMark.Hit);
            return HitResponse(cell);
        }
        private MoveResponse HitResponse(FieldCell cell) {
            if (PlayerField.IsShipNotKilled(cell)) {
                return MoveResponse.Hit;
            }
            else {
                PerformKill(cell);
                return KillResponse();
            }
        }
        private void PerformKill(FieldCell cell) {
            int decks = PlayerField.CountHitShipDecks(cell);
            EraseFromDict(Ships, decks);
        }
        private MoveResponse KillResponse() {
            if (IsGameFinished())
                return MoveResponse.Win;
            else
                return MoveResponse.Kill;
        }
        private bool IsGameFinished() => Ships.Count == 0;
        private MoveResponse HandleMiss(FieldCell cell) {
            PlayerField.SetMark(cell, FieldMark.Miss);
            return MoveResponse.Miss;
        }
        protected void ClearField() {
            PlayerField.Clear();
        }
    }
}
