using System;
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
                int coordinateNumber = i + 1;
                tileButton.onClick.AddListener(() => {
                    AttackInterfaceController.Instance.SetSelectedCoordinate(new Tuple<int, int>(_lineIndex, coordinateNumber));
                });
            }
        }

        public void SetLineLetter(int lineLetter) {
            _lineIndex = lineLetter;
        }

    }
}