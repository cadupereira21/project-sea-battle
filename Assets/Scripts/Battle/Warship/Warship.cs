using System;
using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;

namespace Battle.Warship {
    public class Warship : MonoBehaviour {
        
        [SerializeField]
        private WarshipDataSo warshipDataSo;
        
        [Header("Warship Position")]
        [SerializeField]
        [Tooltip("The coordinate of the front side of the warship")]
        private Vector2Int bowCoordinates;

        [SerializeField]
        [Tooltip("The direction where the boat is facing")]
        private WarshipDirection warshipDirection = WarshipDirection.WEST;

        public List<Tuple<int, int>> Coordinates { get; private set; } = new ();

        private const float POSITION_CONST = 0.5f;

        private Transform _childObjectTransform;

        private bool IsHorizontal => WarshipDirection.EAST.Equals(warshipDirection) ||
                                      WarshipDirection.WEST.Equals(warshipDirection);

        private int _warshipTileSize;
        
        public Vector2 BowCoordinates => bowCoordinates;

        private void Awake() {
            _childObjectTransform = this.transform.GetChild(0);
        }

        private void Start() {
            //SetWarshipSize();
            //SetWarshipPositionByWarshipSize();
            //SetWarshipCoordinates();
        }

        public void RotateShip() {
            warshipDirection = warshipDirection switch {
                WarshipDirection.WEST => WarshipDirection.SOUTH,
                WarshipDirection.SOUTH => WarshipDirection.EAST,
                WarshipDirection.EAST => WarshipDirection.NORTH,
                WarshipDirection.NORTH => WarshipDirection.WEST,
                _ => warshipDirection
            };
            
            Coordinates.Clear();

            if (Mathf.Approximately(this.transform.rotation.y, 270)) {
                _childObjectTransform.transform.Rotate(Vector3.up, -270);
            } else {
                _childObjectTransform.transform.Rotate(Vector3.up, 90);
            }
            
            SetPositionByDirection();
        }

        private void SetPositionByDirection() {
            float x = (float) warshipDataSo.Size/2;
            float z = (float) warshipDataSo.Size/2;
            
            if (IsHorizontal) {
                if (warshipDirection.Equals(WarshipDirection.EAST)) {
                    Debug.Log(Mathf.Ceil(warshipDataSo.Size-1));
                    x -= Mathf.Ceil(warshipDataSo.Size-1);
                }
                z = 0.5f;
            } else {
                if (warshipDirection.Equals(WarshipDirection.SOUTH)) {
                    Debug.Log(Mathf.Ceil(warshipDataSo.Size-1));
                    z -= Mathf.Ceil(warshipDataSo.Size-1);
                }
                x = 0.5f;
            }
            
            _childObjectTransform.transform.localPosition = new Vector3(x, _childObjectTransform.transform.position.y, z);
        }

        public void SetBowCoordinates(Vector3Int coordinates) {
            Debug.Log($"[Warship] Setting bow coordinates to ({Mathf.Abs(coordinates.x - 4)}, {coordinates.z + 5})");
            bowCoordinates = new Vector2Int(Mathf.Abs(coordinates.x - 4), coordinates.z + 5);
        }

        private void SetWarshipCoordinates() {
            int directionModifier = WarshipDirection.WEST.Equals(warshipDirection) || WarshipDirection.NORTH.Equals(warshipDirection) ? 1 : -1;
            for (int i = 0; i < _warshipTileSize; i++) {
                int x = IsHorizontal ? (int)bowCoordinates.x : (int)bowCoordinates.x + i * directionModifier; 
                int y = IsHorizontal ? (int)bowCoordinates.y + i * directionModifier : (int)bowCoordinates.y; 
                Coordinates.Add(new Tuple<int, int>(x, y));
            }
        }
    }
}