using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Placement {
    public class PlacementGridManager : MonoBehaviour {
        
        [SerializeField] 
        private Grid grid;
        
        [Header("Grid Visualization")]
        [SerializeField] private GameObject gridVisualization;
        
        [Header("Indicators")]
        [SerializeField] 
        private GameObject cellIndicator;

        [SerializeField] 
        private GameObject errorCellIndicator;
        
        private static readonly List<Tuple<int, int>> _occupiedCells = new ();
        
        public static ReadOnlyCollection<Tuple<int, int>> OccupiedCells => _occupiedCells.AsReadOnly();

        private void Awake() {
            cellIndicator.SetActive(false);
            errorCellIndicator.SetActive(false);
            gridVisualization.SetActive(false);
        }
        
        public Vector3Int WorldToCell(Vector3 worldPosition) => grid.WorldToCell(worldPosition);
        
        public Vector3 CellToWorld(Vector3Int cellPosition) => grid.CellToWorld(cellPosition);
        
        public void SetCellIndicatorPosition(Vector3 worldPosition) {
            errorCellIndicator.SetActive(false);
            cellIndicator.transform.position =
                new Vector3(worldPosition.x, cellIndicator.transform.position.y, worldPosition.z);
        }
        
        public void SetErrorCellIndicatorPosition(Vector3 worldPosition) {
            errorCellIndicator.SetActive(true);
            errorCellIndicator.transform.position =
                new Vector3(worldPosition.x, errorCellIndicator.transform.position.y, worldPosition.z);
        }
        
        public void AddOccupiedCells(List<Tuple<int, int>> cells) {
            _occupiedCells.AddRange(cells);
        }
        
        public void RemoveOccupiedCells(List<Tuple<int, int>> cells) {
            _occupiedCells.RemoveAll(cells.Contains);
        }

        public void StartPlacement() {
            gridVisualization.SetActive(true);
            cellIndicator.SetActive(true);
        }

        public void StopPlacement() {
            gridVisualization.SetActive(false);
            errorCellIndicator.SetActive(false);
            cellIndicator.SetActive(false);
        }

        public static bool IsCellOccupied(Tuple<int, int> cell) => _occupiedCells.Contains(cell);
    }
}