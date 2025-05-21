using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Placement {
    public class PlacementInputManager : MonoBehaviour {

        [SerializeField] 
        private UnityEngine.Camera sceneCamera;
        
        [SerializeField]
        private LayerMask placementLayerMask;
        
        [SerializeField]
        private Button exitButton;
        
        private Vector3 _lastPosition;

        private float _timeSinceClick;
        
        public UnityEvent OnClick = new ();

        public UnityEvent OnExit = new ();

        private void Awake() {
            exitButton.onClick.AddListener(() => {
                OnExit?.Invoke();
            });
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                OnClick?.Invoke();
            }
        }
        
        public Vector3 GetWorldPointClick() {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = sceneCamera.nearClipPlane;
            
            Ray ray = sceneCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 10, placementLayerMask)) {
                _lastPosition = hit.point;
            }

            return _lastPosition;
        }
    }
}
