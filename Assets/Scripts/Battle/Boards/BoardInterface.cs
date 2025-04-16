using UnityEngine;

namespace Battle.Boards {
    public class BoardInterface : MonoBehaviour {
        
        [SerializeField]
        private GameObject[] boardLinesObj = new GameObject[10];
        
        private readonly BoardLine[] _boardLines = new BoardLine[10];

        private void Awake() {
            for (int index = 0; index < boardLinesObj.Length; index++) {
                BoardLine line = boardLinesObj[index].GetComponent<BoardLine>();
                line.SetLineLetter(index);
                
                _boardLines[index] = line;
            }
        }

        public void SetInterfaceTile(int x, int y, TileType tileType) {
            _boardLines[x].SetColor(y, tileType);
        }
    }
}