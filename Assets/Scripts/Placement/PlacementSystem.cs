using System;
using Battle.Boards;
using Battle.Warship;
using Camera;
using Exception;
using Scriptable_Objects;
using UnityEngine;

namespace Placement {
    [RequireComponent(typeof(PlacementInputManager))]
    [RequireComponent(typeof(PlacementGridManager))]
    public class PlacementSystem : MonoBehaviour {
            
        [Header("Warships")]
        [SerializeField]
        private WarshipsDatabaseSO warshipsDatabase;

        [SerializeField] 
        private GameObject warshipsInstanceParent;
        
        [Header("Board")]
        [SerializeField]
        private PlayerBoard playerBoard;
        
        [Header("Error Handling")]
        [SerializeField]
        private PlacementErrorOverlay placementErrorOverlay;
        
        private PlacementInputManager _placementInputManager;
        
        private PlacementGridManager _placementGridManager;

        private int _selectedObjectIndex;

        private GameObject _selectedParentObject;
        
        private Warship _selectedWarship;

        private static bool IsPointerOverUI => ScreenMovementController.IsPointerOverUI();

        private void Awake() {
            _placementInputManager = this.GetComponent<PlacementInputManager>();
            _placementGridManager = this.GetComponent<PlacementGridManager>();
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
            
            _placementGridManager.StartPlacement();
            _placementInputManager.onScreenClick.AddListener(PositionWarship);
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
            Vector3Int gridPosition = _placementGridManager.WorldToCell(mousePosition);
            Vector3 worldPosition = _placementGridManager.CellToWorld(gridPosition);
            
            Vector3 newPosition = new (worldPosition.x, this.transform.position.y, worldPosition.z);

            try {
                _selectedWarship.CheckNewWarshipPosition(gridPosition);
                _placementGridManager.SetCellIndicatorPosition(worldPosition);
                _selectedParentObject.transform.position = newPosition;
                _selectedWarship.SetBowCoordinates(gridPosition);
            } catch (UserException exception) {
                _placementGridManager.SetErrorCellIndicatorPosition(worldPosition);
                placementErrorOverlay.ShowError(exception);
            }
        }
        
        public void RotateWarship(RotateDirection direction) {
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
            _placementGridManager.AddOccupiedCells(_selectedWarship.Coordinates);
            playerBoard.AddWarship(_selectedWarship);
            
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
                _placementGridManager.RemoveOccupiedCells(_selectedWarship.Coordinates);
                Destroy(_selectedParentObject);
                _selectedParentObject = null;
            }
            
            _placementGridManager.StopPlacement();
            _placementInputManager.onScreenClick.RemoveListener(PositionWarship);
        }
    }
}