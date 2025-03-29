using System;
using UnityEngine;

namespace Battle {
    public class PlayerController : MonoBehaviour {
        
        public static PlayerController Instance { get; private set; }

        private BattleController _battleController;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }

        private void Start() {
            _battleController = BattleController.Instance;
            _battleController.PlayerTurn.AddListener(StartPlayerTurn);
        }

        private void StartPlayerTurn() {
            Debug.Log("[PlayerController] Player turn started!");
        }
        
        public void Attack(int x, int y) {
            _battleController.PlayerAttack(x, y);
        }
    }
}