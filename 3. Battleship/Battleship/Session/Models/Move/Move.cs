namespace Battleship.Models {
    public class Move {
        public FieldCell Cell { get; init; }
        internal MoveResponse Response { get; init; }
        internal Move(FieldCell cell, MoveResponse response = MoveResponse.Undefined) {
            Cell = cell;
            Response = response;
        }
        public bool IsMiss() => Response == MoveResponse.Miss;
        public bool IsHit() => Response == MoveResponse.Hit;
        public bool IsKill () => Response == MoveResponse.Kill;
        public bool IsWin() => Response == MoveResponse.Win;
    }
}
