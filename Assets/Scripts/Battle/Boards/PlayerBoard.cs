namespace Battle.Boards {
    public class PlayerBoard : BattleBoard {

        private static PlayerBoard _instance;
        
        public static PlayerBoard Instance => _instance ??= new PlayerBoard();
        
        private PlayerBoard() { }
    }
}