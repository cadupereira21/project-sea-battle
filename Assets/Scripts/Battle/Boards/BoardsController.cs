using UnityEngine;

namespace Battle.Boards {
    public class BoardsController {
        
        private static BoardsController _instance;
        
        public static BoardsController Instance => _instance ??= new BoardsController();
        
        private readonly EnemyBoard _enemyBoard = EnemyBoard.Instance;
        
        private readonly PlayerBoard _playerBoard = PlayerBoard.Instance;
        
        private BoardsController() { }

        public void AttackEnemy(int x, int y) {
            _enemyBoard.Attack(x, y);
        }
        
        public void AttackPlayer(int x, int y) {
            _playerBoard.Attack(x, y);
        }
        
        public void InitEnemyBoard() {
            _enemyBoard.Init();
        }
    }
}