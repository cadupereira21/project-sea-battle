using System;
using System.Collections.Generic;
using UnityEngine;

namespace Placement {
    public abstract class GridCellsManager {
        
        private static readonly List<Tuple<int, int>> OccupiedCells = new (); 
        
        public static void AddOccupiedCells(List<Tuple<int, int>> occupiedCells) {
            OccupiedCells.AddRange(occupiedCells);

            PrintOccupiedCells();
        }
        
        public static void RemoveOccupiedCells(List<Tuple<int, int>> occupiedCells) {
            foreach (Tuple<int, int> cell in occupiedCells) {
                OccupiedCells.Remove(cell);
            }

            PrintOccupiedCells();
        }
        
        public static bool IsCellOccupied(Tuple<int, int> cell) {
            PrintOccupiedCells();
            return OccupiedCells.Contains(cell);
        }
        
        public static void ClearOccupiedCells() {
            OccupiedCells.Clear();
        }

        private static void PrintOccupiedCells() {
            foreach (Tuple<int, int> cell in OccupiedCells) {
                Debug.Log($"[GridCellsManager] Occupied Cell: {cell.Item1}, {cell.Item2}");
            }
        }
    }
}