using Battle;
using UnityEngine;

namespace UI.BattleScene {
    public class BattleInterface : MonoBehaviour {
        
        [SerializeField] 
        private GameObject actionOverlay;

        [SerializeField] 
        private GameObject attackInterface;

        [SerializeField] 
        private GameObject enemyTurnOverlay;
        
        private BattleController _battleController;

        private void Awake() {
            actionOverlay.SetActive(false);
            attackInterface.SetActive(false);
            enemyTurnOverlay.SetActive(false);
        }

        private void Start() {
            _battleController = BattleController.Instance;
            
            _battleController.OnPlayerTurn.AddListener(ShowActionOverlay);
            _battleController.OnEnemyTurn.AddListener(ShowEnemyTurnOverlay);
        }

        private void OnEnable() {
            actionOverlay.SetActive(true);
        }
        
        private void OnDisable() {
            actionOverlay.SetActive(false);
            attackInterface.SetActive(false);
            enemyTurnOverlay.SetActive(false);
        }
        
        public void ShowAttackInterface() {
            actionOverlay.SetActive(false);
            attackInterface.SetActive(true);
        }
        
        public void ShowActionOverlay() {
            enemyTurnOverlay.SetActive(false);
            attackInterface.SetActive(false);
            actionOverlay.SetActive(true); 
        }
        
        public void ShowEnemyTurnOverlay() {
            actionOverlay.SetActive(false);
            attackInterface.SetActive(false);
            enemyTurnOverlay.SetActive(true);
        }
    }
}
