using Potop.Client.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 플레이어의 입력(회전 및 발사)을 처리하는 터렛 컨트롤러 클래스입니다.
    /// </summary>
    public class TurretShooter : MonoBehaviour {
        [Header("Input Settings")]
        [SerializeField] private InputActionReference _attackAction;
        [SerializeField] private InputActionReference _lookAction;

        [Header("Combat Settings")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _fireRate = 0.5f;
        [SerializeField] private float _sensitivity = 5.0f;

        private float _nextFireTime = 0f;

        private void OnEnable() {
            if (_attackAction != null) {
                _attackAction.action.Enable();
            }
            if (_lookAction != null) {
                _lookAction.action.Enable();
            }
        }

        private void OnDisable() {
            if (_attackAction != null) {
                _attackAction.action.Disable();
            }
            if (_lookAction != null) {
                _lookAction.action.Disable();
            }
        }

        private void Start() {
            if (GameManager.Instance != null) {
                GameManager.Instance.PlayerTransform = transform;
            }
        }

        private void Update() {
            if (GameManager.Instance != null && GameManager.Instance.IsGameOver) {
                return;
            }

            // 회전 (Input System 사용)
            if (_lookAction != null) {
                float mouseX = _lookAction.action.ReadValue<Vector2>().x * _sensitivity;
                transform.Rotate(0, mouseX, 0);
            }

            // 발사
            if (_attackAction != null && _attackAction.action.IsPressed() && Time.time >= _nextFireTime) {
                Shoot();
                _nextFireTime = Time.time + _fireRate;
            }
        }

        private void Shoot() {
            if (_projectilePrefab != null && _firePoint != null) {
                Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            } else {
#if UNITY_EDITOR
                if (_projectilePrefab == null) {
                    Debug.LogWarning("Projectile Prefab is missing!");
                }
                if (_firePoint == null) {
                    Debug.LogWarning("FirePoint is missing!");
                }
#endif
            }
        }
    }
}

