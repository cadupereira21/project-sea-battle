using System;
using System.Collections.Generic;
using Battle.Warship;
using UnityEngine;

namespace Scriptable_Objects {
    
    [CreateAssetMenu]
    public class WarshipDataSo : ScriptableObject {
        
        public int id;

        public string name;

        public WarshipSize sizeEnum;

        private readonly Tuple<int, int>[] _coordinates;

        public int Size {
            get {
                return sizeEnum switch {
                    WarshipSize.SMALL => 2,
                    WarshipSize.MEDIUM => 3,
                    WarshipSize.LARGE => 5,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }

        public GameObject prefab;
    }
}