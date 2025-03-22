using System;
using UnityEngine;

public class CameraMovementController : MonoBehaviour {
    
    private Camera _camera;

    private Vector3 _lastMousePosition;
    
    private void Awake() {
        _camera = this.GetComponent<Camera>();
    }

    private void Update() {
        MoveCamera();
    }

    private void MoveCamera() {
        if (Input.GetMouseButtonDown(0)) {
            _lastMousePosition = Input.mousePosition;
        }

        if (!Input.GetMouseButton(0)) return;
        
        Vector3 actualMouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lastMouseWorldPosition = _camera.ScreenToWorldPoint(_lastMousePosition);
        
        _lastMousePosition = Input.mousePosition;
        
        Vector3 delta = lastMouseWorldPosition - actualMouseWorldPosition;
        
        this.transform.position += delta;
    }
}
