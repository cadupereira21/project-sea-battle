using System;
using Battle.Boards;
using UnityEngine;
using UnityEngine.Events;

namespace Battle {
    public class BattleController : MonoBehaviour {
        
        public static BattleController Instance { get; private set; }

        public UnityEvent enemyTurn = new UnityEvent();

        public UnityEvent playerTurn = new UnityEvent();
        
        private readonly BoardsController _boardsController = BoardsController.Instance;
        
        private BattleTurn _currentTurn = BattleTurn.PLAYER;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }

        public void PlayerAttack(int x, int y) {
            _boardsController.AttackEnemy(x, y);
            ChangeTurn();
        }

        public void EnemyAttack(int x, int y) {
            _boardsController.AttackPlayer(x, y);
            ChangeTurn();
        }

        private void ChangeTurn() {
            _currentTurn = _currentTurn == BattleTurn.ENEMY ? BattleTurn.PLAYER : BattleTurn.ENEMY;
            InvokeEventByCurrentTurn();
        }

        private void InvokeEventByCurrentTurn() {
            switch (_currentTurn) {
                case BattleTurn.PLAYER:
                    playerTurn.Invoke();
                    break;
                case BattleTurn.ENEMY:
                    enemyTurn.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
