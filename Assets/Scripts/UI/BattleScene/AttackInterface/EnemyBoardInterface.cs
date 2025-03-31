using System;
using Battle.Boards;
using UnityEngine;

namespace UI.BattleScene.AttackInterface {
    public class EnemyBoardInterface : MonoBehaviour {
        
        [SerializeField]
        private GameObject[] boardLines = new GameObject[10];

        private void Awake() {
            for (int index = 0; index < boardLines.Length; index++) {
                GameObject line = boardLines[index];
                line.GetComponent<BoardLine>().SetLineLetter(index);
            }
        }

        public void SetInterfaceTile(int x, int y, TileType tileType) {
            boardLines[x].GetComponent<BoardLine>().SetButtonColor(y, tileType);
        }
    }
}