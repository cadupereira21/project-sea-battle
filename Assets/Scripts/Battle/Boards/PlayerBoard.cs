namespace Battle.Boards {
    public class PlayerBoard : BattleBoard {

        public static PlayerBoard Instance { get; private set; }
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }
    }
}