using System;
using UnityEngine;

namespace Battle.Boards {
    public class BoardCell : MonoBehaviour {

        [Header("Cell Materials")] [SerializeField]
        private Material undiscoveredMaterial;
        
        [SerializeField]
        private Material waterMaterial;
        
        [SerializeField]
        private Material warshipDestroyedMaterial;
        
        private Material _cellMaterial;
        
        private TileType _tileType = TileType.WATER;
        
        public TileType TileType {
            get => _tileType;
            set {
                _tileType = value;
                SetMaterial(_tileType);
            }
        }

        private void Awake() {
            _cellMaterial = this.GetComponentInChildren<MeshRenderer>().material;
        }
        
        private void SetMaterial(TileType tileType) {
            if (_cellMaterial == null) {
                throw new NullReferenceException("Cell material is not assigned.");
            }

            _cellMaterial = GetMaterialByTileType(tileType);
        }

        private Material GetMaterialByTileType(TileType tileType) {
            return tileType switch {
                TileType.WATER => waterMaterial,
                TileType.WARSHIP_ALIVE => waterMaterial,
                TileType.WARSHIP_DESTROYED => warshipDestroyedMaterial,
                TileType.UNKNOWN => undiscoveredMaterial,
                _ => throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null)
            };
        }
    }
}