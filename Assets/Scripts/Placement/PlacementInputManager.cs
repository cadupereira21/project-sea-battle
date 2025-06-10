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
        
        private Vector3 _lastPosition;

        private float _timeSinceClick;
        
        public UnityEvent onScreenClick = new ();

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                onScreenClick?.Invoke();
            }
        }
        
        public Vector3 GetWorldPointClick() {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = sceneCamera.nearClipPlane;
            
            Ray ray = sceneCamera.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 20, placementLayerMask)) {
                _lastPosition = hit.point;
            }

            return _lastPosition;
        }
    }
}
