using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

/// <summary>
/// 1인칭 시점의 카메라 회전을 처리하는 클래스입니다.
/// </summary>
public class FirstPersonLook : MonoBehaviour {
    [SerializeField] private InputActionReference _lookAction;

    [FormerlySerializedAs("_mouseSensitivity")]
    [SerializeField] private float _sensitivity = 2.0f;
    [SerializeField] private float _smoothing = 2.0f;

    private float _verticalRotation;

    private const float MIN_X_ROTATION = -90f;
    private const float MAX_X_ROTATION = 90f;

    private void OnEnable() {
        if (_lookAction != null) {
            _lookAction.action.Enable();
        }
    }

    private void OnDisable() {
        if (_lookAction != null) {
            _lookAction.action.Disable();
        }
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {
        if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameState.Playing) {
            return;
        }

        if (_lookAction != null) {
            Vector2 lookInput = _lookAction.action.ReadValue<Vector2>();

            _verticalRotation -= lookInput.y * _sensitivity;
            _verticalRotation = Mathf.Clamp(_verticalRotation, MIN_X_ROTATION, MAX_X_ROTATION);

            transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        }
    }
}
