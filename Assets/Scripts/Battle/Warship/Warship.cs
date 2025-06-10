using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Exception;
using Placement;
using Scriptable_Objects;
using Unity.VisualScripting;
using UnityEngine;

namespace Battle.Warship {
    public class Warship : MonoBehaviour {

        [SerializeField] 
        private PlacementGridManager _placementGridManager;
        
        [SerializeField]
        private WarshipDataSo warshipDataSo;
        
        [Header("Warship Position")]
        [SerializeField]
        [Tooltip("The direction where the boat is facing")]
        private WarshipDirection warshipDirection = WarshipDirection.WEST;

        private Tuple<int, int> _bowCoordinates;
        
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
            SetWarshipCoordinatesBasedOnBowCoordinates(newDirection);

            try {
                ValidateCoordinates(Coordinates);
                UpdateDirection(newDirection);
            } catch (UserException) {
                RotateChild(warshipDirection);
                SetChildPositionByDirection(warshipDirection);
                SetWarshipCoordinatesBasedOnBowCoordinates();
                throw;
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
            _bowCoordinates = GetBowCoordinates(coordinates);
        }
        
        private static Tuple<int, int> GetBowCoordinates(Vector3Int coordinates) {
            return new Tuple<int, int>(9 - coordinates.x, coordinates.z);
        }

        public void SetWarshipCoordinatesBasedOnBowCoordinates(WarshipDirection? newWarshipDirection = null) {
            Coordinates.Clear();
            Coordinates.AddRange(GetWarshipCoordinatesBasedOnBowCoordinates(_bowCoordinates, newWarshipDirection ?? warshipDirection));
        }

        private  List<Tuple<int, int>> GetWarshipCoordinatesBasedOnBowCoordinates(Tuple<int, int> bowCoordinates, WarshipDirection direction) {
            List<Tuple<int, int>> warshipCoordinates = new ();

            int directionModifier = WarshipDirection.WEST.Equals(direction) || WarshipDirection.SOUTH.Equals(direction) ? 1 : -1;
            
            warshipCoordinates.Add(new Tuple<int, int>(bowCoordinates.Item1, bowCoordinates.Item2));
            
            for (int i = 1; i < warshipDataSo.Size; i++) {
                int x = IsHorizontal(direction) ? bowCoordinates.Item1 : bowCoordinates.Item1 + i * directionModifier; 
                int y = IsHorizontal(direction) ? bowCoordinates.Item2 + i * directionModifier : bowCoordinates.Item2; 
                warshipCoordinates.Add(new Tuple<int, int>(x, y));
            }

            return warshipCoordinates;
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

        private static void ValidateCoordinates(List<Tuple<int, int>> coordinates) {
            if (coordinates.Any(PlacementGridManager.IsCellOccupied)) {
                throw new PlacementObjectOverlapException();
            }

            if (!coordinates.All(coordinate => coordinate.Item1 is >= 0 and < 10 && coordinate.Item2 is >= 0 and < 10)) {
                throw new PlacementObjectOutOfBoundsException();
            }
        }

        public void CheckNewWarshipPosition(Vector3Int gridPosition) {
            Tuple<int, int> newBowCoordinates = GetBowCoordinates(gridPosition);
            
            List<Tuple<int, int>> newWarshipCoordinates = GetWarshipCoordinatesBasedOnBowCoordinates(newBowCoordinates, warshipDirection);

            ValidateCoordinates(newWarshipCoordinates);
        }
    }
}