using TMPro;
using UnityEngine;

namespace UI.BattleScene.AttackInterface {
    public class AttackInterfaceController : MonoBehaviour {
        
        public static AttackInterfaceController Instance { get; private set; }
        
        [SerializeField]
        private TextMeshProUGUI selectedCoordinateText;
        
        public string SelectedCoordinate { get; private set; }

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this);  
            }
            
            SelectedCoordinate = "A1";
            selectedCoordinateText.text = SelectedCoordinate;
        }
        
        public void SetSelectedCoordinate(string coordinate) {
            Debug.Log($"[AttackInterfaceController] Selected coordinate: {coordinate}");
            SelectedCoordinate = coordinate;
            selectedCoordinateText.text = coordinate;
        }
    }
}
