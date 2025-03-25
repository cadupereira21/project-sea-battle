using UnityEngine;

public class CameraMovementController : MonoBehaviour {
    
    [SerializeField]
    private Vector2 _zoomMinMax = new Vector2(2, 10);

    [SerializeField] private float zoomSpeed;
    
    private Camera _camera;

    private Vector3 _lastClickPosition;

    private bool _canMove = true;
    
    private void Awake() {  
        _camera = this.GetComponent<Camera>();
    }

    private void Update() {
        GetInputForMobile();
    }

    private void GetInputForMouse() {
        if (Input.GetMouseButtonDown(0)) {
            _lastClickPosition = Input.mousePosition;
        }

        // TODO: Add isMouseOverUI() when implemented
        
        if (!Input.GetMouseButton(0) && _canMove) return;
        
        MoveCamera(Input.mousePosition);
        _lastClickPosition = Input.mousePosition;
    }

    private void GetInputForMobile() {
        if (Input.touchCount <= 0) return;

        if (Input.touchCount == 2) {
            HandleScreenPinch();
        } else {
            HandleScreenDrag();
        }
    }

    private void HandleScreenPinch() {
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);
        
        Vector2 touch0PreviousPosition = touch0.position - touch0.deltaPosition;
        Vector2 touch1PreviousPosition = touch1.position - touch1.deltaPosition;

        float previousDistance = (touch0PreviousPosition - touch1PreviousPosition).magnitude;
        float currentDistance = (touch0.position - touch1.position).magnitude;
        
        float deltaDistance = currentDistance - previousDistance;

        ZoomCamera(deltaDistance * zoomSpeed * Time.deltaTime);
    }

    private void HandleScreenDrag() {
        Touch touch = Input.touches[0];
        
        if (touch.phase == TouchPhase.Began) {
            //_canMove == isClickOverUi();
            _lastClickPosition = touch.position;

        }

        if (touch.phase == TouchPhase.Moved && _canMove) {
            MoveCamera(touch.position);
            _lastClickPosition = touch.position;
        }
    }

    private void MoveCamera(Vector3 actualPosition) {
        Vector3 actualMouseWorldPosition = _camera.ScreenToWorldPoint(actualPosition);
        Vector3 lastMouseWorldPosition = _camera.ScreenToWorldPoint(_lastClickPosition);
        
        Vector3 delta = lastMouseWorldPosition - actualMouseWorldPosition;
        
        this.transform.position += delta;
    }

    private void ZoomCamera(float distance) {
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize - distance, _zoomMinMax.x, _zoomMinMax.y);
    }
}
