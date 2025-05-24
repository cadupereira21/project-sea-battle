using System;
using Battle.Warship;
using Camera;
using Exception;
using Scriptable_Objects;
using UnityEngine;

namespace Placement {
    [RequireComponent(typeof(PlacementInputManager))]
    public class PlacementSystem : MonoBehaviour {
        
        [Header("Grid")]
        [SerializeField] 
        private Grid grid;

        [SerializeField] 
        private GameObject cellIndicator;

        [SerializeField] 
        private GameObject errorCellIndicator;

        [SerializeField] private GameObject gridVisualization;
            
        [Header("Warships")]
        [SerializeField]
        private WarshipsDatabaseSO warshipsDatabase;

        [SerializeField] 
        private GameObject warshipsInstanceParent;
        
        [Header("Error Handling")]
        [SerializeField]
        private PlacementErrorOverlay placementErrorOverlay;
        
        private PlacementInputManager _placementInputManager;

        private int _selectedObjectIndex;

        private GameObject _selectedParentObject;
        
        private Warship _selectedWarship;

        private static bool IsPointerOverUI => ScreenMovementController.IsPointerOverUI();

        private void Awake() {
            _placementInputManager = this.GetComponent<PlacementInputManager>();
        }

        private void Start() {
            StopPlacement();
        }

        public void StartPlacement(int id) {
            Debug.Log($"[PlacementSystem] Starting placement of warship with id '{id}'");
            StopPlacement();
            _selectedObjectIndex = warshipsDatabase.warshipsData.FindIndex(data => data.id == id);

            if (_selectedObjectIndex < 0) {
                Debug.LogError($"[PlacementSystem] The id '{id}' does not exist in the WarshipsDatabase");
                return;
            }
            
            gridVisualization.SetActive(true);
            cellIndicator.SetActive(true);
            _placementInputManager.OnClick.AddListener(PositionWarship);
            _placementInputManager.OnExit.AddListener(StopPlacement);
        }
        
        private void PositionWarship() {
            if (IsPointerOverUI) return;

            if (_selectedParentObject == null) {
                GameObject warshipInstance = Instantiate(warshipsDatabase.warshipsData[_selectedObjectIndex].prefab,
                                                         warshipsInstanceParent.transform);   
                _selectedParentObject = warshipInstance;
                _selectedWarship = warshipInstance.GetComponentInChildren<Warship>();
            }
            
            Vector3 mousePosition = _placementInputManager.GetWorldPointClick();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            Vector3 worldPosition = grid.CellToWorld(gridPosition);
            
            Vector3 newPosition = new (worldPosition.x, this.transform.position.y, worldPosition.z);

            try {
                _selectedWarship.CheckNewWarshipPosition(gridPosition);
                errorCellIndicator.SetActive(false);
                cellIndicator.transform.position =
                    new Vector3(worldPosition.x, cellIndicator.transform.position.y, worldPosition.z);
                _selectedParentObject.transform.position = newPosition;
                _selectedWarship.SetBowCoordinates(gridPosition);
            } catch (UserException exception) {
                errorCellIndicator.SetActive(true);
                errorCellIndicator.transform.position =
                    new Vector3(worldPosition.x, errorCellIndicator.transform.position.y, worldPosition.z);
                placementErrorOverlay.ShowError(exception);
            }
        }
        
        public void RotateWarship() {
            if (_selectedParentObject == null) {
                Debug.LogError($"[PlacementSystem] No warship selected to rotate");
                return;
            }

            try {
                _selectedWarship.RotateShip();
            } catch (UserException exception) {
                placementErrorOverlay.ShowError(exception);
            }
        }

        public void PlaceWarship() {
            _selectedWarship.SetWarshipCoordinatesBasedOnBowCoordinates();
            GridCellsManager.AddOccupiedCells(_selectedWarship.Coordinates);
            
            if (_selectedParentObject == null) {
                Debug.LogError($"[PlacementSystem] No warship selected to place");
                return;
            }
            
            _selectedObjectIndex = -1;
            _selectedParentObject = null;

            StopPlacement();
        }

        public void StopPlacement() {
            Debug.Log($"[PlacementSystem] Stopping placement of warship with id '{_selectedObjectIndex}'");
            _selectedObjectIndex = -1;
            if (_selectedParentObject != null) {
                GridCellsManager.RemoveOccupiedCells(_selectedWarship.Coordinates);
                Destroy(_selectedParentObject);
                _selectedParentObject = null;
            }
            
            gridVisualization.SetActive(false);
            errorCellIndicator.SetActive(false);
            cellIndicator.SetActive(false);
            _placementInputManager.OnClick.RemoveListener(PositionWarship);
            _placementInputManager.OnExit.RemoveListener(StopPlacement);
        }
    }
}