using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/// <summary>
/// 1인칭 시점의 카메라 회전 및 플레이어 몸체 회전을 처리하는 클래스입니다.
/// </summary>
public class FirstPersonLook : MonoBehaviour {
    [SerializeField, FormerlySerializedAs("mouseSensitivity")] private float _mouseSensitivity = 200f;
    private float _xRotation = 0f;

    private const float MIN_X_ROTATION = -90f;
    private const float MAX_X_ROTATION = 90f;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) {
            return;
        }

        float mouseX = Mouse.current.delta.x.ReadValue() * _mouseSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * _mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, MIN_X_ROTATION, MAX_X_ROTATION);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        if (transform.parent != null) {
            transform.parent.Rotate(Vector3.up * mouseX);
        }
    }
}
