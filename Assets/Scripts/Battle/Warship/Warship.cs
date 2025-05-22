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
        
        private void Awake() {
            _childObjectTransform = this.transform.GetChild(0);
        }

        private void Start() {
            SetChildPositionByDirection();
        }

        public void RotateShip() {
            UpdateDirection();
            RotateChild();
            Coordinates.Clear();
            SetChildPositionByDirection();
        }

        private void UpdateDirection() {
            warshipDirection = warshipDirection switch {
                WarshipDirection.WEST => WarshipDirection.SOUTH,
                WarshipDirection.SOUTH => WarshipDirection.EAST,
                WarshipDirection.EAST => WarshipDirection.NORTH,
                WarshipDirection.NORTH => WarshipDirection.WEST,
                _ => warshipDirection
            };
        }

        private void RotateChild() {
            if (Mathf.Approximately(this.transform.rotation.y, 270)) {
                _childObjectTransform.transform.Rotate(Vector3.up, -270);
            } else {
                _childObjectTransform.transform.Rotate(Vector3.up, 90);
            }
        }

        private void SetChildPositionByDirection() {
            float x = (float) warshipDataSo.Size/2;
            float z = (float) warshipDataSo.Size/2;
            
            if (IsHorizontal) {
                if (warshipDirection.Equals(WarshipDirection.EAST)) {
                    x -= Mathf.Ceil(warshipDataSo.Size-1);
                }
                z = POSITION_CONST;
            } else {
                if (warshipDirection.Equals(WarshipDirection.SOUTH)) {
                    z -= Mathf.Ceil(warshipDataSo.Size-1);
                }
                x = POSITION_CONST;
            }
            
            _childObjectTransform.transform.localPosition = new Vector3(x, _childObjectTransform.transform.position.y, z);
        }

        public void SetBowCoordinates(Vector3Int coordinates) {
            bowCoordinates = new Vector2Int(Mathf.Abs(coordinates.x - 4), coordinates.z + 5);
        }

        public void SetWarshipCoordinatesBasedOnBowCoordinates() {
            Coordinates.Clear();
            int directionModifier = WarshipDirection.WEST.Equals(warshipDirection) || WarshipDirection.SOUTH.Equals(warshipDirection) ? 1 : -1;
            
            Coordinates.Add(new Tuple<int, int>(bowCoordinates.x, bowCoordinates.y));
            Debug.Log($"[Warship] Coordinate: ({bowCoordinates.x}, {bowCoordinates.y})");
            
            for (int i = 1; i < warshipDataSo.Size; i++) {
                int x = IsHorizontal ? bowCoordinates.x : bowCoordinates.x + i * directionModifier; 
                int y = IsHorizontal ? bowCoordinates.y + i * directionModifier : bowCoordinates.y; 
                Coordinates.Add(new Tuple<int, int>(x, y));
                Debug.Log($"[Warship] Coordinate: ({x}, {y})");
            }
        }
    }
}