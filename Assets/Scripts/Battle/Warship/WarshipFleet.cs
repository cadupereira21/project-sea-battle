using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle.Warship {
    public class WarshipFleet : MonoBehaviour {
        
        [SerializeField]
        private Warship[] warships;
        
        public List<Tuple<int, int>> GetAllWarshipCoordinates() {
            List<Tuple<int, int>> allCoordinates = new ();
            
            foreach (Warship warship in warships) {
                if (warship != null) {
                    allCoordinates.AddRange(warship.Coordinates);
                }
            }

            return allCoordinates;
        } 
    }
}