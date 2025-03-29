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
            
            SelectedCoordinate = new Tuple<int, int>(0, 1);
            selectedCoordinateText.text = GetStringCoordinates();
        }
        
        public void SetSelectedCoordinate(Tuple<int, int> coordinate) {
            Debug.Log($"[AttackInterfaceController] Selected coordinate: {coordinate}");
            SelectedCoordinate = coordinate;
            selectedCoordinateText.text = GetStringCoordinates();
        }

        public void ConfirmAttack() {
            
        }

        private string GetStringCoordinates() {
            string lineLetter = LineLetters[SelectedCoordinate.Item1];
            string coordinateNumber = SelectedCoordinate.Item2.ToString();
            
            return string.Concat(lineLetter, coordinateNumber);
        }
    }
}
