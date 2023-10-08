namespace Battleship.Settings.Default {
    internal static class DefaultSessionSettings {
        public static int FieldHeight { get; } = 10;
        public static int FieldWidth { get; } = 10;
        public static Dictionary<int, int> ShipDict { get; } =
            new Dictionary<int, int>() {
                { 4, 1 },
                { 3, 2 },
                { 2, 3 },
                { 1, 4 },
        };
    }
}
