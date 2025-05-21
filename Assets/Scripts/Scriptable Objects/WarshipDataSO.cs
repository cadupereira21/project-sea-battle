using Battle.Warship;
using UnityEngine;

namespace Scriptable_Objects {
    
    [CreateAssetMenu]
    public class WarshipDataSO : ScriptableObject {
        
        public int id;

        public string name;
        
        public WarshipSize size;
        
        public GameObject prefab;
    }
}