using System;
using UnityEngine;

namespace Placement {
    public class PlacementSystem : MonoBehaviour {

        [SerializeField] 
        private GameObject mouseIndicator;

        [SerializeField] 
        private GameObject cellIndicator;

        [SerializeField] 
        private Grid grid;
        
        private PlacementInputManager _placementInputManager;

        private void Awake() {
            _placementInputManager = this.GetComponent<PlacementInputManager>();
        }

        private void Update() {
            if (!Input.GetMouseButtonDown(0)) return;
            Vector3 mousePosition = _placementInputManager.GetWorldPointClick();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);
            Vector3 worldPosition = grid.CellToWorld(gridPosition);
            mouseIndicator.transform.position = mousePosition;
            cellIndicator.transform.position = new Vector3(worldPosition.x, cellIndicator.transform.position.y, worldPosition.z);
        }
    }
}