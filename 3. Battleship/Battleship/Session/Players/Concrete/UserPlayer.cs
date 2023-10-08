using Battleship.Models;
using Battleship.Settings;
using Battleship.Players.Exceptions;

namespace Battleship.Players {
    internal class UserPlayer : AbstractPlayer {
        private List<FieldCell> LastSuccesfulCells { get; set; }
        private Dictionary<int, int> ShipsToPlace { get; set; }
        public UserPlayer(SessionSettings settings) : base(settings) {
            LastSuccesfulCells = new List<FieldCell>();
            ShipsToPlace = new Dictionary<int, int>(settings.ShipDict);
        }
        public override Move MakeMove(FieldCell cell) {
            var response = HandleCell(cell);
            if (response == MoveResponse.Hit)
                LastSuccesfulCells.Add(cell);
            else if (response == MoveResponse.Kill)
                LastSuccesfulCells.Clear();
            return new Move(cell, response);
        }
        public int GetNextShipDeck() {
            CheckNextShipExistence();
            return ShipsToPlace.First().Key;
        }
        public void PlaceNextShip(Ship ship) {
            CheckNextShipExistence();
            PlaceShip(ship);
            EraseFromDict(ShipsToPlace, ship.Decks);
        }
        private bool IsNextShipDoesntExist() => ShipsToPlace.Count == 0;
        public bool IsConfigured() => IsNextShipDoesntExist();
        // Chooses a cell for move according to an optimal strategy
        public FieldCell FindOptimalCell() {
            FieldCell? resultCell = null;
            int count = LastSuccesfulCells.Count;
            if (count == 1) {
                var cell = LastSuccesfulCells[0];
                resultCell = GetAroundAvailableCell(cell);
            }
            else if (count >= 2) {
                resultCell = GetNextDirectedCell();
            }
            if (resultCell != null) return resultCell;
            LastSuccesfulCells.Clear();
            return GetRandomAvailableCell();
        }
        // Chooses a cell to kill a hit ship
        private FieldCell? GetNextDirectedCell() {
            CheckCellConsequtiveOrder();
            for (int cellIndex = 0; cellIndex < LastSuccesfulCells.Count; cellIndex++) {
                var cell = LastSuccesfulCells[cellIndex];
                FieldCell? aroundCell;
                if (LastSuccesfulCells[0].Row == LastSuccesfulCells[1].Row)
                    aroundCell = GetAroundAvailableCell(cell, row: true);
                else
                    aroundCell = GetAroundAvailableCell(cell, column: true);
                if (aroundCell != null) return aroundCell;
            }
            return null;
        }
        private void CheckCellConsequtiveOrder() {
            var cell1 = LastSuccesfulCells[0];
            var cell2 = LastSuccesfulCells[1];
            if (cell1.Row != cell2.Row && cell1.Column != cell2.Column)
                throw new AutomateMoveSequenceException();
            if (cell1.Row == cell2.Row) {
                for (int cellIndex = 2; cellIndex < LastSuccesfulCells.Count; cellIndex++)
                    if (LastSuccesfulCells[cellIndex].Row != cell1.Row)
                        throw new AutomateMoveSequenceException();
            }
            else {
                for (int cellIndex = 2; cellIndex < LastSuccesfulCells.Count; cellIndex++)
                    if (LastSuccesfulCells[cellIndex].Column != cell1.Column)
                        throw new AutomateMoveSequenceException();
            }
        }
        private void CheckNextShipExistence() {
            if (IsNextShipDoesntExist())
                throw new NextShipDoesntExistException();
        }
    }
}
