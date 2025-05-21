using System;
using Scriptable_Objects;
using UnityEngine;
using UnityEngine.UI;

namespace Placement {
    public class PlacementOverlay : MonoBehaviour {
        
        [SerializeField]
        private PlacementSystem placementSystem;

        [SerializeField] 
        private GameObject scrollContent;
        
        [SerializeField]
        private WarshipSelector warshipSelectorPrefab;
        
        [SerializeField]
        private WarshipsDatabaseSO warshipsDatabase;

        [Header("Control Buttons")] 
        [SerializeField]
        private Button placeButton;

        [SerializeField] 
        private Button rotateButton;

        private void Start() {
            foreach (WarshipDataSO warshipData in warshipsDatabase.warshipsData) {
                WarshipSelector newInstance = Instantiate(warshipSelectorPrefab, scrollContent.transform);
                newInstance.Init(warshipData, () => placementSystem.StartPlacement(warshipData.id));
            }
            
            placeButton.onClick.AddListener(() => {
                placementSystem.PlaceWarship();
            });
            
            rotateButton.onClick.AddListener(() => {
                placementSystem.RotateWarship();
            });
        }

        private void OnDisable() {
            placementSystem.StopPlacement();
        }
    }
}