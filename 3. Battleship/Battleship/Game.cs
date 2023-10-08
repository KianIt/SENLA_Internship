using Battleship.Settings;
using Battleship.Settings.Default;

namespace Battleship {
    public static class Game {
        private static SessionSettings sessionSettings = null!;
        public static Session CreateSession(int? fieldHeight = null,
                                            int? fieldWidth = null,
                                            Dictionary<int, int>? shipDict = null) {

            var settingsFieldHeight = fieldHeight != null ? (int) fieldHeight :
                                                  DefaultSessionSettings.FieldHeight;
            var settingsFieldWidth = fieldWidth != null ? (int) fieldWidth :
                                                  DefaultSessionSettings.FieldWidth;
            var settingsShipDict = shipDict != null ? shipDict :
                                                  DefaultSessionSettings.ShipDict;

            sessionSettings = new SessionSettings(settingsFieldHeight,
                                                  settingsFieldWidth,
                                                  settingsShipDict);

            return new Session(sessionSettings);
        }
    }
}
