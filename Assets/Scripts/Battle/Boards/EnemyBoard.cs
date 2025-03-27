namespace Battle.Boards {
    public class EnemyBoard : BattleBoard {

        private static EnemyBoard _instance;

        public static EnemyBoard Instance => _instance ??= new EnemyBoard();
        
        private EnemyBoard() { }
    }
}