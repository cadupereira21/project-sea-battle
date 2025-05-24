using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exception;
using Scriptable_Objects;
using Unity.VisualScripting;
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
        
        private void Awake() {
            _childObjectTransform = this.transform.GetChild(0);
        }

        private void Start() {
            SetChildPositionByDirection(warshipDirection);
        }

        public void RotateShip() {
            WarshipDirection newDirection = GetNewWarshipDirection();
            RotateChild(newDirection);
            SetChildPositionByDirection(newDirection);
            
            if (!IsOverlapping())
                UpdateDirection(newDirection);
            else {
                RotateChild(warshipDirection);
                SetChildPositionByDirection(warshipDirection);
                throw new ObjectOverlappingException();
            }
        }

        private void UpdateDirection(WarshipDirection newWarshipDirection) {
            warshipDirection = newWarshipDirection;
        }

        private WarshipDirection GetNewWarshipDirection() {
            return warshipDirection switch {
                WarshipDirection.WEST => WarshipDirection.SOUTH,
                WarshipDirection.SOUTH => WarshipDirection.EAST,
                WarshipDirection.EAST => WarshipDirection.NORTH,
                WarshipDirection.NORTH => WarshipDirection.WEST,
                _ => warshipDirection
            };
        }

        private void RotateChild(WarshipDirection newWarshipDirection) {
            _childObjectTransform.transform.rotation = Quaternion.Euler(0, IsHorizontal(newWarshipDirection) ? 0 : 90, 0);
        }

        private void SetChildPositionByDirection(WarshipDirection newWarshipDirection) {
            _childObjectTransform.transform.localPosition = GetChildPositionByDirection(newWarshipDirection);
        }

        private Vector3 GetChildPositionByDirection(WarshipDirection newWarshipDirection) {
            float x = (float) warshipDataSo.Size/2;
            float z = (float) warshipDataSo.Size/2;
            
            if (IsHorizontal(newWarshipDirection)) {
                if (newWarshipDirection.Equals(WarshipDirection.EAST)) {
                    x -= Mathf.Ceil(warshipDataSo.Size-1);
                }
                z = POSITION_CONST;
            } else {
                if (newWarshipDirection.Equals(WarshipDirection.SOUTH)) {
                    z -= Mathf.Ceil(warshipDataSo.Size-1);
                }
                x = POSITION_CONST;
            }
            
            return new Vector3(x, _childObjectTransform.transform.position.y, z);
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
                int x = IsHorizontal() ? bowCoordinates.x : bowCoordinates.x + i * directionModifier; 
                int y = IsHorizontal() ? bowCoordinates.y + i * directionModifier : bowCoordinates.y; 
                Coordinates.Add(new Tuple<int, int>(x, y));
                Debug.Log($"[Warship] Coordinate: ({x}, {y})");
            }
        }
        
        private bool IsHorizontal(WarshipDirection? direction = null) {
            if (direction == null) {
                return WarshipDirection.EAST.Equals(warshipDirection) ||
                       WarshipDirection.WEST.Equals(warshipDirection);
            } else {
                return WarshipDirection.EAST.Equals(direction) ||
                       WarshipDirection.WEST.Equals(direction);  
            }
        }

        private bool IsOverlapping() {
            // check collision with other trigger collider
            return false;
        }
    }
}