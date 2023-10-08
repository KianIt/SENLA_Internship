namespace Battleship.Models {
    internal class Ship {
        public int Decks { get; init; }
        public FieldCell Cell { get; init; }
        public ShipDirection Direction { get; init; }
        public Ship(int decks, FieldCell cell, ShipDirection direction) {
            Decks = decks;
            Cell = cell;
            Direction = direction;
        }
    }
}
