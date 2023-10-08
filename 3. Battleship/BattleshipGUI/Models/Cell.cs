using Battleship.Models;

namespace BattleshipGUI.Models {
    internal class Cell {
        public int Row { get; init; }
        public int Column { get; init; }
        public Cell(int row, int column) {
            Row = row;
            Column = column;
        }
        public static Cell operator +(Cell left, Cell right) {
            return new Cell(left.Row + right.Row, left.Column + right.Column);
        }
        public static Cell operator -(Cell left, Cell right) {
            return new Cell(left.Row - right.Row, left.Column - right.Column);
        }
        public static bool operator ==(Cell left, Cell right) {
            return left.Row == right.Row && left.Column == right.Column;
        }
        public static bool operator !=(Cell left, Cell right) {
            return !(left == right);
        }
    }
}
