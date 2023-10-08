using BattleshipGUI.Settings;
using System.Windows.Forms;

namespace BattleshipGUI.Models {
    internal class FieldView {
        private int Height { get; set; }
        private int Width { get; set; }
        private Button[,] Table { get; set; } = null!;
        public List<Cell> Clicked { get; set; } = null!;
        public FieldView(Button[,] table, int height = 10, int width = 10) {
            Height = height;
            Width = width;
            Table = table;
            InitializeTable();
            Clicked = new List<Cell>();
        }
        private void InitializeTable() {
            for (int row = 0; row < Height; row++)
                for (int column = 0; column < Width; column++) {
                    Table[row, column].Text = FormSettings.EmptyFieldButtonMark;
                }
        }
        private void ButtonClick(object sender, EventArgs e) {
            Cell cell = (Cell) ((Button) sender).DataContext!;
            Clicked.Add(cell);
        }
        public void Enable() {
            for (int row = 0; row < Height; row++)
                for (int column = 0; column < Width; column++) {
                    Table[row, column].Enabled = true;
                }
        }
        public void Disable() {
            for (int row = 0; row < Height; row++)
                for (int column = 0; column < Width; column++) {
                    Table[row, column].Enabled = false;
                }
        }
        public void Clickable() {
            for (int row = 0; row < Height; row++)
                for (int column = 0; column < Width; column++) {
                    Table[row, column].Click += ButtonClick!;
                }
        }
        public void Disclickabe() {
            for (int row = 0; row < Height; row++)
                for (int column = 0; column < Width; column++) {
                    Table[row, column].Click -= ButtonClick!;
                }
        }
        public void Miss(Cell cell) {
            Table[cell.Row, cell.Column].Text = FormSettings.MissFieldButtonMark;
        }
        public void Miss(Cell cell1, Cell cell2, int cellCount = 0) {
            var shift = GetOneShift(cell1, cell2);
            if (cellCount == 0) {
                for (; cell1 != cell2;) {
                    Table[cell1.Row, cell1.Column].Text = FormSettings.MissFieldButtonMark;
                    cell1 = cell1 + shift;
                }
                Table[cell2.Row, cell2.Column].Text = FormSettings.MissFieldButtonMark;
            }
            else
                for (int count = 0; count < cellCount; count++) {
                    Table[cell1.Row, cell1.Column].Text = FormSettings.MissFieldButtonMark;
                    cell1 = cell1 + shift;
                }
        }
        public void Hit(Cell cell) {
            Table[cell.Row, cell.Column].Text = FormSettings.HitFieldButtonMark;
        }
        public void Hit(Cell cell1, Cell cell2, int cellCount = 0) {
            var shift = GetOneShift(cell1, cell2);
            if (cellCount == 0) {
                for (; cell1 != cell2;) {
                    Table[cell1.Row, cell1.Column].Text = FormSettings.HitFieldButtonMark;
                    cell1 = cell1 + shift;
                }
                Table[cell2.Row, cell2.Column].Text = FormSettings.HitFieldButtonMark;
            }
            else
                for (int count = 0; count < cellCount; count++) {
                    Table[cell1.Row, cell1.Column].Text = FormSettings.HitFieldButtonMark;
                    cell1 = cell1 + shift;
                }
        }
        public void Ship(Cell cell) {
            Table[cell.Row, cell.Column].Text = FormSettings.ShipFieldButtonMark;
        }
        public void Ship(Cell cell1, Cell cell2, int cellCount = 0) {
            var shift = GetOneShift(cell1, cell2);
            if (cellCount == 0) {
                for (; cell1 != cell2;) {
                    Table[cell1.Row, cell1.Column].Text = FormSettings.ShipFieldButtonMark;
                    cell1 = cell1 + shift;
                }
                Table[cell2.Row, cell2.Column].Text = FormSettings.ShipFieldButtonMark;
            }
            else
                for (int count = 0; count < cellCount; count++) {
                    Table[cell1.Row, cell1.Column].Text = FormSettings.ShipFieldButtonMark;
                    cell1 = cell1 + shift;
                }
        }
        private Cell GetOneShift(Cell cell1, Cell cell2) {
            if (cell1.Row == cell2.Row) {
                if (cell1.Column < cell2.Column)
                    return new Cell(0, 1);
                else
                    return new Cell(0, -1);
            }
            else {
                if (cell1.Row < cell2.Row)
                    return new Cell(1, 0);
                else
                    return new Cell(-1, 0);
            }
        }
    }
}
