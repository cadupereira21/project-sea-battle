using System;
using Core;
using UnityEngine;

namespace UI.InitialScene {
    public class InitialSceneUiController : MonoBehaviour {
       
        [SerializeField] private GameObject initialScreen;

        [SerializeField] private GameObject editFleetOverlay;

        private void OnEnable() {
            initialScreen.SetActive(true);
            editFleetOverlay.SetActive(false);
        }

        public void OnEditFleetButtonClick() {
            initialScreen.SetActive(false);
            editFleetOverlay.SetActive(true);
        }

        public void OnEditFleetBackButtonClick() {
            initialScreen.SetActive(true);
            editFleetOverlay.SetActive(false);
        }

        public void OnPlayButtonClick() {
            SceneLoader.Instance.LoadSceneAsync("BattleScene");
        }
    }
}
