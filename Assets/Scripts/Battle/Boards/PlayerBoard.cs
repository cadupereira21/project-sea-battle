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
            
            DontDestroyOnLoad(this);
            
            this.InstantiateBoardCells();
        }

        public void AddWarship(Warship.Warship warship) {
            if (warship == null) {
                throw new ArgumentNullException(nameof(warship), "Warship cannot be null");
            }

            warshipFleet.AddWarship(warship);
            this.SetBoardTilesForWarships(warship.Coordinates);
        }
    }
}