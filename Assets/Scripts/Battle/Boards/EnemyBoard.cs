using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Boards {
    public class EnemyBoard : BattleBoard {

        public static EnemyBoard Instance { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }

        public void Init() {
            this.InitWater();
            InitWarships();
            this.PrintBoard();
        }
        
        private void InitWarships() {
            List<Tuple<int, int>> warshipCoordinates = new List<Tuple<int, int>> {
                // WARSHIP 1 (5 tiles)
                new (0, 0),
                new (0, 1),
                new (0, 2),
                new (0, 3),
                new (0, 4),
                // WARSHIP 4 (5 tiles)
                new (1, 8),
                new (2, 8),
                new (3, 8),
                new (4, 8),
                new (5, 8),
                // WARSHIP 2 (3 tiles)
                new (6, 5),
                new (6, 6),
                new (6, 7),
                // WARSHIP 3 (3 tiles
                new (6, 2),
                new (7, 2),
                new (8, 2),
                // WARSHIP 5 (2 tiles)
                new (9, 0),
                new (9, 1),
                // WARSHIP 6 (2 tiles)
                new (9, 8),
                new (9, 9)
            };
            
            foreach (Tuple<int, int> coordinate in warshipCoordinates) {
                this.SetBoardTile(coordinate.Item1, coordinate.Item2, TileType.WARSHIP_ALIVE);
            }
        }
    }
}