using System;
using Battle.Boards;
using UnityEngine;
using UnityEngine.Events;

namespace Battle {
    public class BattleController : MonoBehaviour {
        
        public static BattleController Instance { get; private set; }

        [NonSerialized]
        public readonly UnityEvent OnEnemyTurn = new ();

        [NonSerialized]
        public readonly UnityEvent OnPlayerTurn = new ();
        
        private BoardsController _boardsController;
        
        private BattleTurn _currentTurn = BattleTurn.PLAYER;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
            
        }

        private void Start() {
            _boardsController = BoardsController.Instance;
            _boardsController.InitBoards();
        }

        public void PlayerAttack(int x, int y) {
            Debug.Log($"[BattleController] Player attack: ({x+1}, {y+1})");
            _boardsController.AttackEnemy(x, y);
            ChangeTurn();
        }

        public void EnemyAttack(int x, int y) {
            Debug.Log($"[BattleController] Enemy attack: ({x+1}, {y+1})");
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
                    OnPlayerTurn.Invoke();
                    break;
                case BattleTurn.ENEMY:
                    OnEnemyTurn.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
