using Battleship.Models;
using Battleship.Settings;

namespace Battleship.Players {
    internal class AIPlayer : AbstractPlayer {
        private static int MAX_ATTEMPTS_TO_PLACE_ONE_SHIP = 5;
        private Dictionary<int, int> ShipsToPlace { get; set; } = null!;
        public AIPlayer(SessionSettings settings) : base(settings) {
            ShipsToPlace = new Dictionary<int, int>(settings.ShipDict);
            Configure();
        }
        public override Move MakeMove(FieldCell cell) => new Move(cell, HandleCell(cell));
        // While not all ships placed tries random ship placement
        private void Configure() {
            var shipsToPlace = new Dictionary<int, int>(ShipsToPlace);
            while (ShipsToPlace.Count > 0) {
                ShipsToPlace = new Dictionary<int, int>(shipsToPlace);
                ClearField();
                PlaceAllShips();
            }
        }
        // Attempt to place all ships
        private void PlaceAllShips() {
            foreach (var item in ShipsToPlace) {
                int decks = item.Key, shipsCount = item.Value;
                for (int count = 0; count < shipsCount; count++) {
                    for (int attempt = 0; attempt < MAX_ATTEMPTS_TO_PLACE_ONE_SHIP; attempt++) {
                        var ship = GetRandomShip(decks);
                        try {
                            PlaceOneShip(ship);
                            break;
                        }
                        catch {}
                    }
                }
            }
        }
        // Attempt to place one ship
        private void PlaceOneShip(Ship ship) {
            PlaceShip(ship);
            EraseFromDict(ShipsToPlace, ship.Decks);
        }
        private Ship GetRandomShip(int decks) {
            var random = new Random();
            var cell = new FieldCell(random.Next(GetHeight()),
                                     random.Next(GetWidht()));
            var direction = (ShipDirection) random.Next(4);
            return new Ship(decks, cell, direction);
        }
    }
}
