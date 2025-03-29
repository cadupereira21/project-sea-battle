using System;
using Battle;
using TMPro;
using UnityEngine;

namespace UI.BattleScene.AttackInterface {
    public class AttackInterfaceController : MonoBehaviour {
        
        public static AttackInterfaceController Instance { get; private set; }

        public static string[] LineLetters { get; private set; } = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

        [SerializeField]
        private TextMeshProUGUI selectedCoordinateText;
        
        public Tuple<int, int> SelectedCoordinate { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);  
            }
            
            SelectedCoordinate = new Tuple<int, int>(0, 0);
            selectedCoordinateText.text = GetStringCoordinates();
        }
        
        public void SetSelectedCoordinate(Tuple<int, int> coordinate) {
            Debug.Log($"[AttackInterfaceController] Selected coordinate: {coordinate}");
            SelectedCoordinate = coordinate;
            selectedCoordinateText.text = GetStringCoordinates();
        }

        public void ConfirmAttack() {
            PlayerController.Instance.Attack(SelectedCoordinate.Item1, SelectedCoordinate.Item2);
        }

        private string GetStringCoordinates() {
            string lineLetter = LineLetters[SelectedCoordinate.Item1];
            int coordinateNumber = SelectedCoordinate.Item2 + 1;
            
            return string.Concat(lineLetter, coordinateNumber);
        }
    }
}
