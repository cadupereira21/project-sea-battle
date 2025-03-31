using System;
using Battle.Boards;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleScene.AttackInterface {
    public class BoardLine : MonoBehaviour {
            
        [SerializeField]
        private GameObject[] coordinates = new GameObject[10];

        private int _lineIndex;

        private void Start() {
            for (int i = 0; i < coordinates.Length; i++) {
                Button tileButton = coordinates[i].GetComponent<Button>();
                SetButtonColor(i);
                int column = i;
                tileButton.onClick.AddListener(() => {
                    AttackInterfaceController.Instance.SetSelectedCoordinate(new Tuple<int, int>(_lineIndex, column));
                });
            }
        }

        public void SetLineLetter(int lineLetter) {
            _lineIndex = lineLetter;
        }
        
        public void SetButtonColor(int column, TileType tileType = TileType.UNKNOWN) {
            coordinates[column].GetComponent<Image>().color = tileType switch {
                TileType.WATER => Color.blue,
                TileType.WARSHIP_DESTROYED => Color.red,
                TileType.UNKNOWN => Color.gray,
                TileType.WARSHIP_ALIVE => Color.green,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

    }
}