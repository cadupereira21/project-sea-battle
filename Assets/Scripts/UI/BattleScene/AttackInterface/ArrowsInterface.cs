using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleScene.AttackInterface {
    public class ArrowsInterface : MonoBehaviour {
        
        [SerializeField]
        private GameObject upArrow;
        
        [SerializeField]
        private GameObject downArrow;
        
        [SerializeField]
        private GameObject leftArrow;
        
        [SerializeField]
        private GameObject rightArrow;
        
        [SerializeField]
        private GameObject confirmButton;
        
        private void Awake() {
            upArrow.SetActive(true);
            downArrow.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            confirmButton.SetActive(true);
            
            upArrow.GetComponent<Button>().onClick.AddListener(UpArrowClick);
            downArrow.GetComponent<Button>().onClick.AddListener(DownArrowClick);
            leftArrow.GetComponent<Button>().onClick.AddListener(LeftArrowClick);
            rightArrow.GetComponent<Button>().onClick.AddListener(RightArrowClick);
        }

        private static void UpArrowClick() {
            Tuple<int, int> selectedCoordinate = AttackInterfaceController.Instance.SelectedCoordinate;
            
            if (selectedCoordinate.Item1 == 0) {
                return;
            }
            
            int newLineValue = selectedCoordinate.Item1 - 1;
            
            AttackInterfaceController.Instance.SetSelectedCoordinate(new Tuple<int, int>(newLineValue, selectedCoordinate.Item2));
        }
        
        private static void DownArrowClick() {
            Tuple<int, int> selectedCoordinate = AttackInterfaceController.Instance.SelectedCoordinate;
            
            if (selectedCoordinate.Item1 == 9) {
                return;
            }
            
            int newLineValue = selectedCoordinate.Item1 + 1;
            
            AttackInterfaceController.Instance.SetSelectedCoordinate(new Tuple<int, int>(newLineValue, selectedCoordinate.Item2));
        }
        
        private static void LeftArrowClick() {
            Tuple<int, int> selectedCoordinate = AttackInterfaceController.Instance.SelectedCoordinate;
            
            if (selectedCoordinate.Item2 == 1) {
                return;
            }
            
            int newCoordinateValue = selectedCoordinate.Item2 - 1;
            
            AttackInterfaceController.Instance.SetSelectedCoordinate(new Tuple<int, int>(selectedCoordinate.Item1, newCoordinateValue));
        }
        
        private static void RightArrowClick() {
            Tuple<int, int> selectedCoordinate = AttackInterfaceController.Instance.SelectedCoordinate;
            
            if (selectedCoordinate.Item2 == 10) {
                return;
            }
            
            int newCoordinateValue = selectedCoordinate.Item2 + 1;
            
            AttackInterfaceController.Instance.SetSelectedCoordinate(new Tuple<int, int>(selectedCoordinate.Item1, newCoordinateValue));
        }
    }
}