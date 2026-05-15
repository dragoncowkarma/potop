using Potop.Client.Core;
using Potop.Client.Core.Events;
using Potop.Client.Data;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Potop.Client.Gameplay.Weapons {
    /// <summary>
    /// 플레이어의 입력(회전 및 발사)을 처리하는 터렛 컨트롤러 클래스입니다.
    /// </summary>
    public class TurretShooter : WeaponBase {
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
        [SerializeField] private float _sensitivity = 5.0f;

        /// <summary>
        /// 시점 회전 민감도입니다.
        /// </summary>
        public float Sensitivity => _sensitivity;

        private bool _isFeverActive;
        private const float FEVER_FIRE_RATE_MULTIPLIER = 2.0f;

        private void OnEnable() {
            EventBroker.Subscribe<FeverStateChangedEvent>(OnFeverStateChanged);

            if (_attackAction != null) {
                _attackAction.action.Enable();
            }
            if (_lookAction != null) {
                _lookAction.action.Enable();
            }
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<FeverStateChangedEvent>(OnFeverStateChanged);

            if (_attackAction != null) {
                _attackAction.action.Disable();
            }
            if (_lookAction != null) {
                _lookAction.action.Disable();
            }
        }

        protected override void Start() {
            base.Start();

            _fireStrategy = new Potop.Client.Gameplay.Weapons.Strategies.StraightFireStrategy();

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
            if (_attackAction != null && _attackAction.action.IsPressed()) {
                Fire();
            }
        }

        private void OnFeverStateChanged(FeverStateChangedEvent e) {
            _isFeverActive = e.IsFeverActive;
        }

        /// <summary>
        /// 터렛의 특성에 맞게 발사 가능 여부를 재정의합니다. 피버 모드의 영향을 받습니다.
        /// </summary>
        protected override bool CanFire() {
            if (_weaponData == null) return false;

            // 터렛은 잔탄을 소모하지 않거나 확인하지 않습니다.
            float currentFireRate = _weaponBody != null ? _weaponBody.ModifyFireRate(_weaponData.BaseFireRate) : _weaponData.BaseFireRate;

            if (_isFeverActive) {
                currentFireRate *= FEVER_FIRE_RATE_MULTIPLIER;
            }

            // FireRate가 0 이하일 경우 발사하지 않습니다.
            if (currentFireRate <= 0) return false;

            float fireInterval = 1f / currentFireRate;
            return Time.time >= _lastFireTime + fireInterval;
        }

        /// <summary>
        /// 터렛의 발사 로직입니다. 잔탄 소모를 무시합니다.
        /// </summary>
        public override void Fire() {
            if (!CanFire()) return;

            _lastFireTime = Time.time;

            _fireStrategy?.ExecuteFire(this);

            EventBroker.Publish(new WeaponFiredEvent { Weapon = this });
        }
    }
}

