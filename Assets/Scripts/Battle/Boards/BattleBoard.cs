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
                    SetTile(x, y, TileType.WARSHIP_DESTROYED);
                    break;
                case TileType.WARSHIP_DESTROYED:
                    Debug.Log("[BattleBoard] Warship already destroyed!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetTile(int x, int y, TileType value) {
            _board[x, y] = value;
        }
    }
}