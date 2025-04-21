using System;
using System.Collections.Generic;
using Battle.Warship;
using UnityEngine;

namespace Battle.Boards {
    public class PlayerBoard : BattleBoard {

        public static PlayerBoard Instance { get; private set; }
        
        [SerializeField]
        private WarshipFleet warshipFleet;
        
        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(this.gameObject);
            }
        }

        public void Init() {
            this.InitWater();
            InitWarships();
            //this.PrintBoard();
        }

        private void InitWarships() {
            List<Tuple<int, int>> allCoordinates = warshipFleet.GetAllWarshipCoordinates();
            base.InitWarships(allCoordinates);
        }
    }
}