using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Boards {
    public abstract class BattleBoard : MonoBehaviour {

        [SerializeField] 
        private Grid grid;
        
        [SerializeField]
        private BoardCell gridCellPrefab;
        
        private readonly BoardCell[,] _gridCells = new BoardCell[10, 10];

        protected void InstantiateBoardCells() {
            for (int l = 0; l < 10; l++) {
                for (int c = 0; c < 10; c++) {
                    BoardCell cell = Instantiate(gridCellPrefab, this.transform);
                    cell.name = $"Cell_{l}_{c}";
                    cell.transform.position = grid.CellToWorld(new Vector3Int(9 - l, 0, c));
                    _gridCells[l, c] = cell;
                }
            }
        }

        public void Attack(int x, int y) {
            TileType tileType = _gridCells[x,y].TileType;

            switch (tileType) {
                case TileType.WATER:
                    Debug.Log("[BattleBoard] Attack missed!");
                    SetInterfaceTile(x, y, TileType.WATER);
                    break;
                case TileType.WARSHIP_ALIVE:
                    Debug.Log("[BattleBoard] Warship hit!");
                    SetBoardTile(x, y, TileType.WARSHIP_DESTROYED);
                    SetInterfaceTile(x, y, TileType.WARSHIP_DESTROYED);
                    break;
                case TileType.WARSHIP_DESTROYED:
                    Debug.Log("[BattleBoard] Warship already destroyed!");
                    SetInterfaceTile(x, y, TileType.WARSHIP_DESTROYED);
                    break;
                case TileType.UNKNOWN:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetBoardTile(int x, int y, TileType value) {
            _gridCells[x, y].TileType = value;
        }

        private void SetInterfaceTile(int x, int y, TileType value) {
            //boardInterface.SetInterfaceTile(x, y, value);
        }

        protected void SetBoardTilesForWarships(List<Tuple<int, int>> warshipCoordinates) {
            foreach (Tuple<int, int> coordinate in warshipCoordinates) {
                SetBoardTile(coordinate.Item1, coordinate.Item2, TileType.WARSHIP_ALIVE);
            }
        }
    }
}