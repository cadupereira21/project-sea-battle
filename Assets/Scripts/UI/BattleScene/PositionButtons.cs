using System.Collections.Generic;
using UnityEngine;

namespace UI.BattleScene {
    
    public enum StartOrientation {
        FROM_ABOVE,
        FROM_BELOW
    }
    
    public class PositionButtons : MonoBehaviour {
        
        [SerializeField]
        private float xOffset;
        
        [SerializeField]
        private StartOrientation startOrientation = StartOrientation.FROM_BELOW;
        
        [SerializeField] 
        [Tooltip("The buttons that will be positioned. They need to have the same size")]
        private List<GameObject> buttons;

        private void Start() {
            Position();
        }

        private void Position() {
            float canvasHeight = this.GetComponent<RectTransform>().rect.height;
            float buttonHeight = buttons[0].GetComponent<RectTransform>().rect.height;
            float totalButtonHeight = buttonHeight * buttons.Count;
            float spaceBetweenButtons = (canvasHeight - totalButtonHeight) / (buttons.Count + 1);
            
            for (int i = 0; i < buttons.Count; i++) {
                RectTransform rectTransform = buttons[i].GetComponent<RectTransform>();
                
                float newY = (canvasHeight/2) - (spaceBetweenButtons * (i+1)) - (buttonHeight * i) - (buttonHeight/2);
                
                rectTransform.anchoredPosition = new Vector2(-xOffset, startOrientation.Equals(StartOrientation.FROM_ABOVE) ? newY : -newY);
            }
        }
    }
}
