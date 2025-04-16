using System;
using UI.BattleScene.AttackInterface;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.Boards {
    public abstract class BoardLine : MonoBehaviour {
            
        [SerializeField]
        protected GameObject[] coordinates = new GameObject[10];

        protected int LineIndex;

        public void SetLineLetter(int lineLetter) {
            LineIndex = lineLetter;
        }
        
        public abstract void SetColor(int column, TileType tileType);
    }
}