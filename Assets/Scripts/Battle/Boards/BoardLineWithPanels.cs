using System;
using UnityEngine;

namespace Battle.Boards {
    public class BoardLineWithPanels : BoardLine {

        [SerializeField] private Material waterMat;
        
        [SerializeField] private Material warshipDestroyedMat;
        
        [SerializeField] private Material unknownMat;
        
        private readonly MeshRenderer[] _meshRenderers = new MeshRenderer[10];

        private void Awake() {
            for (int i = 0; i < this.coordinates.Length; i++) {
                _meshRenderers[i] = this.coordinates[i].GetComponent<MeshRenderer>();
            }
        }

        private void Start() {
            for (int i = 0; i < this.coordinates.Length; i++) {
                SetColor(i, TileType.UNKNOWN);
            }
        }
        
        public override void SetColor(int column, TileType tileType) {
            _meshRenderers[column].material = tileType switch {
                TileType.WATER => waterMat,
                TileType.WARSHIP_DESTROYED => warshipDestroyedMat,
                TileType.UNKNOWN => unknownMat,
                TileType.WARSHIP_ALIVE => unknownMat,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}