using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Warship {
    public class Warship : MonoBehaviour {
        
        [SerializeField]
        private WarshipData warshipData;
        
        [Header("Warship Position")]
        [SerializeField]
        [Tooltip("The coordinate of the front side of the warship")]
        private Vector2 coordinatesBow;

        [SerializeField]
        [Tooltip("The direction where the boat is facing")]
        private WarshipDirection warshipDirection;

        public List<Tuple<int, int>> Coordinates { get; private set; } = new ();

        private const int FIRST_COLUMN_POSITION = -45;
        
        private const int FIRST_ROW_POSITION = 45;

        private bool IsHorizontal => WarshipDirection.EAST.Equals(warshipDirection) ||
                                      WarshipDirection.WEST.Equals(warshipDirection);

        private int _warshipTileSize;

        private void Start() {
            SetWarshipSize();
            SetWarshipPositionByWarshipSize();
            SetWarshipCoordinates();
        }

        private void SetWarshipSize() {
            Vector3 scale = this.transform.localScale;

            switch (warshipData.size) {
                case WarshipSize.SMALL:
                    this.transform.localScale = new Vector3(WarshipSizeConstants.SMALL_WARSHIP_SIZE, scale.y, scale.z);
                    _warshipTileSize = 2;
                    break;
                case WarshipSize.MEDIUM:
                    this.transform.localScale = new Vector3(WarshipSizeConstants.MEDIUM_WARSHIP_SIZE, scale.y, scale.z);
                    _warshipTileSize = 3;
                    break;
                case WarshipSize.LARGE:
                    this.transform.localScale = new Vector3(WarshipSizeConstants.LARGE_WARSHIP_SIZE, scale.y, scale.z);
                    _warshipTileSize = 5;
                    break;
                default:
                    _warshipTileSize = 0;
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetWarshipPositionByWarshipSize() {
            WarshipSize size = warshipData.size;

            switch (size) {
                case WarshipSize.SMALL:
                    SetSmallWarshipPosition();
                    break;
                case WarshipSize.MEDIUM:
                    SetMediumWarshipPosition();
                    break;
                case WarshipSize.LARGE:
                    SetLargeWarshipPosition();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetSmallWarshipPosition() {
            SetWarshipPosition(40);
        }
        
        private void SetMediumWarshipPosition() {
            SetWarshipPosition(35);
        }

        private void SetLargeWarshipPosition() {
            SetWarshipPosition(25);
        }
        
        private void SetWarshipPosition(int basePosition) {
            int x = IsHorizontal ? -basePosition : FIRST_COLUMN_POSITION; // -40
            int z = IsHorizontal ? FIRST_ROW_POSITION : basePosition; // 45
            
            if (IsHorizontal) {
                x = WarshipDirection.WEST.Equals(warshipDirection ) ? x : x - 10 * (_warshipTileSize-1); 
            } else {
                this.transform.Rotate(Vector3.up, 90);
                z = WarshipDirection.NORTH.Equals(warshipDirection) ? z : z + 10 * (_warshipTileSize-1);
            }

            this.transform.position = new Vector3(
                x + ((int) coordinatesBow.y * 10),
                2,
                z - ((int) coordinatesBow.x * 10)
            );
        }

        private void SetWarshipCoordinates() {
            int directionModifier = WarshipDirection.WEST.Equals(warshipDirection) || WarshipDirection.NORTH.Equals(warshipDirection) ? 1 : -1;
            for (int i = 0; i < _warshipTileSize; i++) {
                int x = IsHorizontal ? (int)coordinatesBow.x : (int)coordinatesBow.x + i * directionModifier; 
                int y = IsHorizontal ? (int)coordinatesBow.y + i * directionModifier : (int)coordinatesBow.y; 
                Coordinates.Add(new Tuple<int, int>(x, y));
            }
        }
    }
}