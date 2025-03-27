using System;
using UnityEngine;

namespace UI.BattleScene {
    public class BattleInterface : MonoBehaviour {
        
        [SerializeField] 
        private GameObject actionOverlay;

        [SerializeField] 
        private GameObject attackInterface;

        private void Awake() {
            actionOverlay.SetActive(false);
            attackInterface.SetActive(false);
        }

        private void OnEnable() {
            actionOverlay.SetActive(true);
        }
        
        private void OnDisable() {
            actionOverlay.SetActive(false);
            attackInterface.SetActive(false);
        }
        
        public void ShowAttackInterface() {
            actionOverlay.SetActive(false);
            
            attackInterface.SetActive(true);
        }
        
        public void ShowActionOverlay() {
            attackInterface.SetActive(false);
            
            actionOverlay.SetActive(true); 
        }
    }
}
