using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle.Warship {
    public class WarshipFleet : MonoBehaviour {
        
        [SerializeField]
        private List<Warship> warships;
        
        public List<Tuple<int, int>> GetAllWarshipCoordinates() {
            List<Tuple<int, int>> allCoordinates = new ();
            
            foreach (Warship warship in warships.Where(warship => warship != null)) {
                allCoordinates.AddRange(warship.Coordinates);
            }

            return allCoordinates;
        } 
        
        public void AddWarship(Warship warship) {
            if (warship == null) {
                throw new ArgumentNullException(nameof(warship), "Warship cannot be null");
            }

            warships.Add(warship);
        }
    }
}