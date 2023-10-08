namespace BattleshipGUI.Settings {
    internal static class FormSettings {
        // Form Settings
        public static Size FormSize { get; } = new Size(900, 600);
        public static string FormTitle { get; } = "Battleship";
        // Start/Stop Buttons Settings
        public static Size StartStopButtonSize { get; } = new Size(99, 34);
        public static Point StartStopButtonCoords { get; } = new Point(394, 515);
        public static Font StartStopButtonFont { get; } = new Font("Segoe UI",
                                                           12f,
                                                           FontStyle.Bold);
        public static string StartButtonText { get; } = "Start";
        public static string StopButtonText { get; } = "Stop";
        // Player Panels Settings
        public static Size PlayerPanelSize { get; } = new Size(362, 362);
        public static Point UserPanelCoords { get; } = new Point(74, 133);
        public static Point AIPanelCoords { get; } = new Point(492, 133);
        // Number Panels Settings
        public static Size NumberPanelSize { get; } = new Size(38, 362);
        public static Point UserNumberPanelCoords { get; } = new Point(34, 133);
        public static Point AINumberPanelCoords { get; } = new Point(452, 133);
        public static Size NumberLabelSize { get; } = new Size(30, 30);
        public static Padding NumberLabelMargin = new Padding(3, 3, 3, 3);
        public static Font NumberLabelFont { get; } = new Font("Segoe UI",
                                                               12f,
                                                               FontStyle.Bold);
        // Letter Panels Settings
        public static Size LetterPanelSize { get; } = new Size(362, 38);
        public static Point UserLetterPanelCoords { get; } = new Point(74, 93);
        public static Point AILetterPanelCoords { get; } = new Point(492, 93);
        public static Size LetterLabelSize { get; } = new Size(30, 30);
        public static Padding LetterLabelMargin = new Padding(3, 3, 3, 3);
        public static Font LetterLabelFont { get; } = new Font("Segoe UI",
                                                               12f,
                                                               FontStyle.Bold);
        // Player Labels Settings
        public static Size PlayerLabelSize { get; } = new Size(362, 29);
        public static Font PlayerLabelFont { get; } = new Font("Segoe UI",
                                                               16f,
                                                               FontStyle.Bold);
        public static Point UserPlayerLabelCoords { get; } = new Point(74, 61);
        public static string UserPlayerLabelText { get; } = "User";
        public static Point AIPlayerLabelCoords { get; } = new Point(492, 61);
        public static string AIPlayerLabelText { get; } = "AI";
        // Message Label Settings
        public static Size MessageLabelSize { get; } = new Size(780, 30);
        public static Point MessageLabelCoords { get; } = new Point(74, 20);
        public static string MessageLabelText { get; } = "Battleship Game";
        public static Font MessageLabelFont { get; } = new Font("Segoe UI",
                                                                15f);
        public static int MessageLabelSleep { get; } = 1000;
        // Field Buttons Settings
        public static Size FieldButtonSize { get; } = new Size(30, 30);
        public static Font FieldButtonFont { get; } = new Font("Segoe UI",
                                                                12f);
        public static string EmptyFieldButtonMark { get; } = " ";
        public static string ShipFieldButtonMark { get; } = "⌂";
        public static string MissFieldButtonMark { get; } = "●";
        public static string HitFieldButtonMark { get; } = "X";
    }
}
