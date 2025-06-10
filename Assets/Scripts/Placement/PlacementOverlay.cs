using System;
using Scriptable_Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Placement {
    public class PlacementOverlay : MonoBehaviour {
        
        [SerializeField]
        private PlacementSystem placementSystem;
        
        [SerializeField]
        private WarshipSelector warshipSelectorPrefab;

        [SerializeField]
        private WarshipsDatabaseSO warshipsDatabase;
        
        [Header("Warship selectors location")]
        [SerializeField] 
        private GameObject scrollContent;

        [Header("Control Buttons")] 
        [SerializeField]
        private Button placeButton;

        [SerializeField] 
        private Button rotateLeftButton;

        [SerializeField] 
        private Button rotateRightButton;

        [SerializeField] 
        private Button cancelButton;

        private void Start() {
            foreach (WarshipDataSo warshipData in warshipsDatabase.warshipsData) {
                WarshipSelector newInstance = Instantiate(warshipSelectorPrefab, scrollContent.transform);
                newInstance.Init(warshipData, () => placementSystem.StartPlacement(warshipData.id));
            }
            
            placeButton.onClick.AddListener(() => {
                placementSystem.PlaceWarship();
            });
            
            rotateLeftButton.onClick.AddListener(() => {
                placementSystem.RotateWarship(RotateDirection.LEFT);
            });
            
            rotateRightButton.onClick.AddListener(() => {
                placementSystem.RotateWarship(RotateDirection.RIGHT);
            });
            
            cancelButton.onClick.AddListener(() => {
                placementSystem.StopPlacement();
            });
        }

        private void OnDisable() {
            placementSystem.StopPlacement();
        }
    }
}