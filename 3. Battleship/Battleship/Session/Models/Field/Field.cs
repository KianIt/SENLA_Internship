using Battleship.Models.Exceptions;

namespace Battleship.Models {
    internal class Field {
        private static FieldCell[] Shifts { get; } = {
            new FieldCell(-1, 0),
            new FieldCell(0, -1),
            new FieldCell(1, 0),
            new FieldCell(0, 1)
        };
        private static FieldMark[,] CreateTable(int height, int width) => new FieldMark[height, width];
        public int Height { get; init; }
        public int Width { get; init; }
        private FieldMark[,] Table { get; init; }
        private bool[,] Used { get; set; } = null!;
        internal Field(int height, int width) {
            Height = height;
            Width = width;
            Table = CreateTable(Height, Width);
        }
        // Finds any cell around current with the given mark
        public FieldCell? GetAroundCell(FieldCell cell, FieldMark mark) {
            for (int shiftIndex = 0; shiftIndex < Shifts.Count(); shiftIndex++) {
                var nextCell = cell + Shifts[shiftIndex];
                if (IsCellNotValid(nextCell)) continue;
                if (GetMark(nextCell) == mark) {
                    return nextCell;
                }
            }
            return null;
        }
        // Checks whether ship has hitless decks
        public bool IsShipNotKilled(FieldCell cell) {
            InitializeUsed();
            return IsShipNotKilled(cell, null);
        }
        private bool IsShipNotKilled(FieldCell cell, FieldCell? prevCell) {
            SetUsed(cell, true);
            var shipAround = GetAroundCell(cell, FieldMark.Ship);
            if (shipAround != null) return true;
            for (int shiftIndex = 0; shiftIndex < Shifts.Count(); shiftIndex++) {
                var nextCell = cell + Shifts[shiftIndex];
                if (IsCellNotValid(nextCell)) continue;
                if (nextCell != prevCell && GetMark(nextCell) == FieldMark.Hit && 
                    !GetUsed(nextCell) && IsShipNotKilled(nextCell, cell)) {
                        return true;
                }
            }
            return false;
        }
        // Counts hit decks if they were hit continuosly
        public int CountHitShipDecks(FieldCell cell) {
            InitializeUsed();
            return CountHitShipDecks(cell, null);
        }
        private int CountHitShipDecks(FieldCell cell, FieldCell? prevCell) {
            SetUsed(cell, true);
            int decks = 1;
            for (int shiftIndex = 0; shiftIndex < Shifts.Count(); shiftIndex++) {
                var nextCell = cell + Shifts[shiftIndex];
                if (IsCellNotValid(nextCell)) continue;
                if (nextCell != prevCell && GetMark(nextCell) == FieldMark.Hit && !GetUsed(nextCell)) {
                    decks += CountHitShipDecks(nextCell, cell);
                }
            }
            return decks;
        }
        // Checks whether the planned ship placement is valid
        public bool IsPlacementValid(Ship ship) {
            var cell = ship.Cell;
            var shift = GetDirectedShift(ship.Direction);
            for (int deck = 0; deck < ship.Decks; deck++) {
                if (IsCellNotValid(cell) || IsShipAround(cell)) {
                    return false;
                }
                cell = cell + shift;
            }
            return true;
        }
        // Places ship on the field
        public void PlaceShip(Ship ship) {
            CheckPlacementValidity(ship);
            var cell = ship.Cell;
            var shiftCell = GetDirectedShift(ship.Direction);
            for (int deck = 0; deck < ship.Decks; deck++) {
                SetMark(cell, FieldMark.Ship);
                cell = cell + shiftCell;
            }
        }
        // Chooses a random cell available for move
        public FieldCell GetRandomAvailableCell() {
            Random random = new Random();
            List<FieldCell> avaiableCells = new List<FieldCell>();
            for (int row = 0; row < Height; row++)
                for (int column = 0; column < Height; column++) {
                    var cell = new FieldCell(row, column);
                    if (IsCellAvailable(cell)) {
                        avaiableCells.Add(cell);
                    }
                }
            int count = avaiableCells.Count;
            if (count == 0)
                throw new NoAvailableCellsException();
            return avaiableCells[random.Next(count)];
        }
        // Chooses a random cell around available for move
        public FieldCell? GetAroundAvailableCell(FieldCell cell, bool row = false, bool column = false) {
            Random random = new Random();
            List<FieldCell> avaiableCells = new List<FieldCell>();
            for (int shiftIndex = 0; shiftIndex < Shifts.Count(); shiftIndex++) {
                var nextCell = cell + Shifts[shiftIndex];
                if (IsCellNotValid(nextCell)) continue;
                if (row && nextCell.Row != cell.Row) continue;
                if (column && nextCell.Column != cell.Column) continue;
                if (IsCellConsecutiveAvailable(nextCell)) {
                    avaiableCells.Add(nextCell);
                }
            }
            int count = avaiableCells.Count;
            if (count == 0) return null;
            return avaiableCells[random.Next(count)];
        }
        public void Clear() {
            for (int row = 0; row < Height; row++)
                for (int column = 0; column < Height; column++) {
                    var cell = new FieldCell(row, column);
                    SetMark(cell, FieldMark.Empty);
                }
        }
        public FieldMark GetMark(FieldCell cell) {
            CheckCellValidity(cell);
            return Table[cell.Row, cell.Column];
        }
        public void SetMark(FieldCell cell, FieldMark mark) {
            CheckCellValidity(cell);
            Table[cell.Row, cell.Column] = mark;
        }
        private FieldCell GetDirectedShift(ShipDirection direction) {
            return Shifts[(int) direction];
        }
        private void InitializeUsed() {
            Used = new bool[Height, Width];
        }
        private bool GetUsed(FieldCell cell) {
            return Used[cell.Row, cell.Column];
        }
        private void SetUsed(FieldCell cell, bool flag) {
            Used[cell.Row, cell.Column] = flag;
        }
        private bool IsCellConsecutiveAvailable(FieldCell cell) =>
            IsCellEmptyOrShip(cell);
        private bool IsCellAvailable(FieldCell cell) =>
            IsCellEmptyOrShip(cell) && IsNotHitAround(cell);
        private bool IsCellEmptyOrShip(FieldCell cell) =>
            GetMark(cell) == FieldMark.Empty ||
            GetMark(cell) == FieldMark.Ship;
        private bool IsPlacementInvalid(Ship ship) {
            return !IsPlacementValid(ship);
        }
        private bool IsShipAround(FieldCell cell) {
            return GetAroundCell(cell, FieldMark.Ship) != null;
        }
        private bool IsHitAround(FieldCell cell) {
            return GetAroundCell(cell, FieldMark.Hit) != null;
        }
        private bool IsNotHitAround(FieldCell cell) {
            return !IsHitAround(cell);
        }
        private void CheckCellValidity(FieldCell cell) {
            if (IsCellNotValid(cell))
                throw new InvalidCellException();
        }
        private void CheckPlacementValidity(Ship ship) {
            if (IsPlacementInvalid(ship))
                throw new InvalidShipPlacementException();
        }
        private bool IsCellNotValid(FieldCell cell) {
            return !IsCellValid(cell);
        }
        private bool IsCellValid(FieldCell cell) {
            return cell.Row >= 0 && cell.Row < Height &&
                   cell.Column >= 0 && cell.Column < Width;
        }
    }
}
