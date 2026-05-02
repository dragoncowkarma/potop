using Potop.Client.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 1인칭 시점의 카메라 회전 및 플레이어 몸체 회전을 처리하는 클래스입니다.
    /// </summary>
    public class FirstPersonLook : MonoBehaviour {
        [SerializeField] private InputActionReference _lookAction;
        [SerializeField] private float _sensitivity = 2.0f;
        [SerializeField] private float _smoothing = 2.0f;

        /// <summary>
        /// 시점 조작에 사용할 입력 액션 레퍼런스입니다.
        /// </summary>
        public InputActionReference LookAction => _lookAction;

        /// <summary>
        /// 시점 회전 민감도입니다.
        /// </summary>
        public float Sensitivity => _sensitivity;

        /// <summary>
        /// 시점 회전 부드러움 정도입니다.
        /// </summary>
        public float Smoothing => _smoothing;

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

        private void Update() {
            if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameState.Playing) {
                return;
            }

            if (_lookAction != null) {
                float mouseY = _lookAction.action.ReadValue<Vector2>().y * _sensitivity * Time.deltaTime;

                _verticalRotation -= mouseY;
                _verticalRotation = Mathf.Clamp(_verticalRotation, MIN_X_ROTATION, MAX_X_ROTATION);

                transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
            }
        }
    }
}

