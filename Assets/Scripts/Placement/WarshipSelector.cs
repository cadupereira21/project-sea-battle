using Scriptable_Objects;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Placement {
    public class WarshipSelector : MonoBehaviour {
        
        [SerializeField]
        private TextMeshProUGUI warshipNameText;
        
        [SerializeField]
        private Button button;

        private WarshipDataSo _warshipDataSo;
        
        public void Init(WarshipDataSo warshipDataSo, UnityAction onClick) {
            _warshipDataSo = warshipDataSo;
            warshipNameText.text = _warshipDataSo.name;
            button.onClick.AddListener(onClick);
        }

    }
}