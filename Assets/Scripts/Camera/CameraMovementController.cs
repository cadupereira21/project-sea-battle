using UnityEngine;

namespace Camera {
    public class CameraMovementController : MonoBehaviour {

        [SerializeField] private UnityEngine.Camera sceneCamera;

        private Plane _plane;
        
        private Vector3 _lastWorldPosition;

        private void Start() {
            _plane = new Plane(inNormal: Vector3.up, inPoint: Vector3.zero);
        }

        private void Update() {
            Touch[] touches = Input.touches;

            if (touches.Length != 1) return;
            
            
            HandleCameraDrag(touches[0]);
        }

        private void HandleCameraDrag(Touch touch) {
            Ray ray = sceneCamera.ScreenPointToRay(touch.position);

            if (!_plane.Raycast(ray: ray, enter: out float enter)) return;
            
            Vector3 worldPosition = ray.GetPoint(enter);

            if (touch.phase == TouchPhase.Began) {
                _lastWorldPosition = worldPosition; // record first
            }
            else {
                MoveCamera(worldPosition);
            }
        }

        private void MoveCamera(Vector3 worldPosition) {
            Vector3 delta = worldPosition - _lastWorldPosition;
                
            Vector3 position = sceneCamera.transform.position;
            position -= delta;

            sceneCamera.transform.position = position;
        }
    }
}
