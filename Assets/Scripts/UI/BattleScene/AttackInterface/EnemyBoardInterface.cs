using System;
using UnityEngine;

namespace UI.BattleScene.AttackInterface {
    public class EnemyBoardInterface : MonoBehaviour {
        
        private static readonly string[] LineLetters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};
        
        [SerializeField]
        private GameObject[] boardLines = new GameObject[10];

        private void Awake() {
            for (int index = 0; index < boardLines.Length; index++) {
                GameObject line = boardLines[index];
                line.GetComponent<BoardLine>().SetLineLetter(LineLetters[index]);
            }
        }
    }
}