using System;
using UnityEngine;

namespace Battle.Boards {
    public abstract class BattleBoard {

        private readonly TileType[,] _board = new TileType[10, 10];

        public void Attack(int x, int y) {
            TileType tileType = _board[x,y];

            switch (tileType) {
                case TileType.WATER:
                    Debug.Log("[BattleBoard] Attack missed!");
                    break;
                case TileType.WARSHIP_ALIVE:
                    Debug.Log("[BattleBoard] Warship hit!");
                    SetTile(x, y, TileType.WARSHIP_DESTROYED);
                    break;
                case TileType.WARSHIP_DESTROYED:
                    Debug.Log("[BattleBoard] Warship already destroyed!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void SetTile(int x, int y, TileType value) {
            _board[x, y] = value;
        }

        protected void InitWater() {
            // Fill the board with water tiles
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    _board[i, j] = TileType.WATER;
                }
            }
        }

        protected void PrintBoard() {
            // Print the board in the console
            for (int i = 0; i < 10; i++) {
                string line = "";
                for (int j = 0; j < 10; j++) {
                    line += (int) _board[i, j] + " ";
                }
                Debug.Log(line);
            }
        }
    }
}