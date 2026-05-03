using Potop.Client.Core;
using Potop.Client.Data;
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

        /// <summary>
        /// 발사 조작에 사용할 입력 액션 레퍼런스입니다.
        /// </summary>
        public InputActionReference AttackAction => _attackAction;

        /// <summary>
        /// 시점 조작에 사용할 입력 액션 레퍼런스입니다.
        /// </summary>
        public InputActionReference LookAction => _lookAction;

        [Header("Combat Settings")]
        [SerializeField] private WeaponData _weaponData;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _sensitivity = 5.0f;

        /// <summary>
        /// 발사할 발사체 프리팹입니다.
        /// </summary>
        public GameObject ProjectilePrefab => _weaponData != null ? _weaponData.ProjectilePrefab : null;

        /// <summary>
        /// 발사체가 생성될 위치(Transform)입니다.
        /// </summary>
        public Transform FirePoint => _firePoint;

        /// <summary>
        /// 발사 주기(초 단위)입니다.
        /// </summary>
        public float FireRate => _weaponData != null ? _weaponData.FireRate : 0f;

        /// <summary>
        /// 시점 회전 민감도입니다.
        /// </summary>
        public float Sensitivity => _sensitivity;

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
                _nextFireTime = Time.time + FireRate;
            }
        }

        private void Shoot() {
            if (_projectilePrefab != null && _firePoint != null) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Spawn(_projectilePrefab, _firePoint.position, _firePoint.rotation);
            } else {
#if UNITY_EDITOR
                if (prefab == null) {
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

