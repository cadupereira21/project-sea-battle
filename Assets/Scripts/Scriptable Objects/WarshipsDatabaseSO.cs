using System.Collections.Generic;
using Battle.Warship;
using UnityEngine;

namespace Scriptable_Objects {
    [CreateAssetMenu]
    public class WarshipsDatabaseSO : ScriptableObject {

        public List<WarshipDataSO> warshipsData;

    }
}