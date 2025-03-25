using UnityEngine;

namespace Camera {
    public class CameraMovementController : MonoBehaviour {
        
        private UnityEngine.Camera _camera;

        private Vector3 _lastClickPosition;

        private bool _canMove = true;
    
        private void Awake() {  
            _camera = this.GetComponent<UnityEngine.Camera>();
        }

        private void Update() {
            GetInputForMobile();
        }

        private void GetInputForMobile() {
            switch (Input.touchCount) {
                case <= 0:
                    return;
                default:
                    HandleScreenDrag();
                    break;
            }
        }

        private void HandleScreenDrag() {
            Touch touch = Input.touches[0];
        
            switch (touch.phase) {
                case TouchPhase.Began:
                    //_canMove == isClickOverUi();
                    _lastClickPosition = touch.position;
                    break;
                case TouchPhase.Moved when _canMove:
                    MoveCamera(touch.position);
                
                    _lastClickPosition = touch.position;
                    break;
            }
        }

        private void MoveCamera(Vector3 actualPosition) {
            Vector3 actualMouseWorldPosition = _camera.ScreenToWorldPoint(actualPosition);
            Vector3 lastMouseWorldPosition = _camera.ScreenToWorldPoint(_lastClickPosition);
        
            Vector3 delta = lastMouseWorldPosition - actualMouseWorldPosition;
        
            this.transform.position += delta;
        }
    }
}
