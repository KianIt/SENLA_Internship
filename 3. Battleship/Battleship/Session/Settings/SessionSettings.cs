namespace Battleship.Settings {
    internal class SessionSettings {
        public int FieldHeight { get; init; }
        public int FieldWidth { get; init; }
        public Dictionary<int, int> ShipDict { get; init; }
        public SessionSettings(int fieldHeight,
                              int fieldWidth,
                              Dictionary<int, int> shipDict) {
            FieldHeight = fieldHeight;
            FieldWidth = fieldWidth;
            ShipDict = new Dictionary<int, int>(shipDict);
        }
    }
}
