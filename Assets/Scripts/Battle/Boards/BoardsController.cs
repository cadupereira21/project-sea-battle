using System;
using UnityEngine;

namespace Battle.Boards {
    public class BoardsController : MonoBehaviour {
        
        public static BoardsController Instance { get; private set; }
        
        private EnemyBoard _enemyBoard;
        
        private PlayerBoard _playerBoard;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }

        private void Start() {
            _enemyBoard = EnemyBoard.Instance;
            _playerBoard = PlayerBoard.Instance;
        }

        public void AttackEnemy(int x, int y) {
            _enemyBoard.Attack(x, y);
        }
        
        public void AttackPlayer(int x, int y) {
            _playerBoard.Attack(x, y);
        }
        
        public void InitBoards() {
            // If we don't initialize the enemy board here, it will be null when we try to init it
            _enemyBoard = EnemyBoard.Instance;
            _playerBoard = PlayerBoard.Instance;
            
            _enemyBoard.Init();
            _playerBoard.Init();
        }
    }
}