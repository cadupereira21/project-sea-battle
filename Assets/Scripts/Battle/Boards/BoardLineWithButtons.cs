using System;
using UI.BattleScene.AttackInterface;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Boards {
    public class BoardLineWithButtons : BoardLine {
        
        private readonly Image[] _buttonImages = new Image[10];

        private void Awake() {
            for (int i = 0; i < this.coordinates.Length; i++) {
                _buttonImages[i] = this.coordinates[i].GetComponent<Image>();
            }
        }

        private void Start() {
            for (int i = 0; i < this.coordinates.Length; i++) {
                Button tileButton = this.coordinates[i].GetComponent<Button>();
                SetColor(i, TileType.UNKNOWN);
                int column = i;
                tileButton.onClick.AddListener(() => {
                    AttackInterfaceController.Instance.SetSelectedCoordinate(new Tuple<int, int>(this.LineIndex, column));
                });
            }
        }
        
        public override void SetColor(int column, TileType tileType) {
            _buttonImages[column].color = tileType switch {
                TileType.WATER => Color.blue,
                TileType.WARSHIP_DESTROYED => Color.red,
                TileType.UNKNOWN => Color.gray,
                TileType.WARSHIP_ALIVE => Color.green,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}