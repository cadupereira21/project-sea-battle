using System;
using Battle.Boards;
using UnityEngine;
using UnityEngine.Events;

namespace Battle {
    public class BattleController : MonoBehaviour {
        
        public static BattleController Instance { get; private set; }

        [NonSerialized]
        public readonly UnityEvent EnemyTurn = new UnityEvent();

        [NonSerialized]
        public readonly UnityEvent PlayerTurn = new UnityEvent();
        
        private readonly BoardsController _boardsController = BoardsController.Instance;
        
        private BattleTurn _currentTurn = BattleTurn.PLAYER;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
            
            _boardsController.InitEnemyBoard();
        }

        public void PlayerAttack(int x, int y) {
            Debug.Log($"[BattleController] Player attack: ({x}, {y})");
            _boardsController.AttackEnemy(x, y);
            ChangeTurn();
        }

        public void EnemyAttack(int x, int y) {
            Debug.Log($"[BattleController] Enemy attack: ({x}, {y})");
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
                    PlayerTurn.Invoke();
                    break;
                case BattleTurn.ENEMY:
                    EnemyTurn.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
