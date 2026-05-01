using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어의 카메라 회전을 처리하는 클래스입니다.
/// </summary>
public class Player : MonoBehaviour {
    private const float MOUSE_SENSITIVITY = 200f;
    private const float MIN_X_ROTATION = -90f;
    private const float MAX_X_ROTATION = 90f;

    private float _mouseX;
    private float _mouseY;
    private float _xRotation = 0f;
    private float _yRotation = 0f;

    private Camera _mainCamera;

    private void Start() {
        _mainCamera = Camera.main;

        if (GameManager.Instance != null) {
            GameManager.Instance.PlayerTransform = transform;
        }
    }

    private void Update() {
        _mouseX = Mouse.current.delta.x.ReadValue() * Time.deltaTime * MOUSE_SENSITIVITY;
        _mouseY = Mouse.current.delta.y.ReadValue() * Time.deltaTime * MOUSE_SENSITIVITY;

        _xRotation -= _mouseY;
        _yRotation += _mouseX;

        _xRotation = Mathf.Clamp(_xRotation, MIN_X_ROTATION, MAX_X_ROTATION);

        if (_mainCamera != null) {
            _mainCamera.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
        }
    }
}
