namespace Battleship.Models {
    public class FieldCell {
        public int Row { get; init; }
        public int Column { get; init; }
        public FieldCell(int row, int column) {
            Row = row;
            Column = column;
        }
        public static FieldCell operator +(FieldCell left, FieldCell right) {
            return new FieldCell(left.Row + right.Row, left.Column + right.Column);
        }
        public static FieldCell operator - (FieldCell left, FieldCell right) {
            return new FieldCell(left.Row - right.Row, left.Column - right.Column);
        }
    }
}
