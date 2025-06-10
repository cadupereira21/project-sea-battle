using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class LoadingScreen : MonoBehaviour {
        
        [SerializeField]
        private Slider loadingSlider;
        
        private void Awake() {
            HideLoadingScreen();
        }

        private void OnEnable() {
            ShowLoadingScreen();
        }

        private void ShowLoadingScreen() {
            this.gameObject.SetActive(true);
        }
        
        private void HideLoadingScreen() {
            this.gameObject.SetActive(false);
        }

        public Slider GetSlider() {
            return loadingSlider;
        }
    }
}