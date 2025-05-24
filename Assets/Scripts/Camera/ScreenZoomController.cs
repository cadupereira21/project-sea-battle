using UnityEngine;

namespace Camera {
    public class ScreenZoomController : MonoBehaviour {
        
        [SerializeField]
        private float maxZoom;

        [SerializeField] 
        private float minZoom;

        [SerializeField] 
        private float zoomSpeed;

        private UnityEngine.Camera _camera;

        private void Awake() {
            _camera = this.GetComponent<UnityEngine.Camera>();
        }

        private void Update() {
            CheckInput();
        }

        private void CheckInput() {
            if (Input.touchCount != 2) return;
            
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
        
            Vector2 touch0PreviousPosition = touch0.position - touch0.deltaPosition;
            Vector2 touch1PreviousPosition = touch1.position - touch1.deltaPosition;

            float previousDistance = (touch0PreviousPosition - touch1PreviousPosition).magnitude;
            float currentDistance = (touch0.position - touch1.position).magnitude;
        
            float deltaDistance = currentDistance - previousDistance;

            ZoomCamera(deltaDistance * zoomSpeed * Time.deltaTime);
        }
        
        private void ZoomCamera(float zoomAmount) {
            _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView - zoomAmount, minZoom, maxZoom);
        }
    }
}