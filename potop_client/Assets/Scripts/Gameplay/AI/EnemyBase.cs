using Potop.Client.Core;
using Potop.Client.Data;
using UnityEngine;

using Potop.Client.Gameplay.Combat;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 적의 현재 행동 상태를 정의합니다.
    /// </summary>
    public enum EnemyState {
        Idle,
        Move,
        Attack,
        Die
    }

    /// <summary>
    /// 적의 공통 이동 로직과 IDamageable 통합을 처리하는 추상 기본 클래스입니다.
    /// 구체적인 적의 행동은 이 클래스를 상속받아 구현해야 합니다.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class EnemyBase : MonoBehaviour {
        [SerializeField] protected EnemyData _enemyData;
        [SerializeField] protected int _damage = 10;
        [SerializeField] protected float _attackRange = 2f;

        protected Health _healthComponent;
        protected float _sqrAttackRange;

        protected EnemyState _currentState = EnemyState.Idle;
        protected float _nextRotationUpdateTime;
        protected const float ROTATION_UPDATE_INTERVAL = 0.1f;
        protected Quaternion _targetRotation;

        /// <summary>
        /// 적의 이동 속도입니다.
        /// </summary>
        public virtual float MoveSpeed => _enemyData != null ? _enemyData.MoveSpeed : 0f;

        /// <summary>
        /// 적의 체력입니다.
        /// </summary>
        public int Health => _healthComponent != null ? _healthComponent.CurrentHealth : 0;

        /// <summary>
        /// 적이 플레이어에게 입히는 피해량입니다.
        /// </summary>
        public int Damage => _damage;

        /// <summary>
        /// 적의 공격 사거리입니다.
        /// </summary>
        public float AttackRange => _attackRange;

        /// <summary>
        /// 적이 처치되었을 때 획득할 수 있는 점수입니다.
        /// </summary>
        public int ScoreValue => _enemyData != null ? _enemyData.ScoreValue : 0;

        protected Transform _target;
        public Transform Target => _target;

        protected Rigidbody _rb;

        protected virtual void Awake() {
            _healthComponent = GetComponent<Health>();
            _rb = GetComponent<Rigidbody>();
            _sqrAttackRange = _attackRange * _attackRange;
        }

        protected virtual void OnEnable() {
            if (GameManager.Instance != null) {
                _target = GameManager.Instance.PlayerTransform;
            }
            if (_healthComponent != null) {
                if (_enemyData != null) {
                    _healthComponent.InitializeHealth(_enemyData.MaxHealth);
                }
                _healthComponent.OnDeath += HandleDeath;
            }
            _currentState = EnemyState.Move;
            _nextRotationUpdateTime = Time.time + Random.Range(0f, ROTATION_UPDATE_INTERVAL);
        }

        protected virtual void OnDisable() {
            if (_healthComponent != null) {
                _healthComponent.OnDeath -= HandleDeath;
            }
        }

        protected virtual void Start() {
            // Initialization moved to OnEnable for pooling support
        }

        protected virtual void Update() {
            if (_target == null) return;

            switch (_currentState) {
                case EnemyState.Move:
                    HandleMoveState();
                    break;
                case EnemyState.Attack:
                    // 공격 연출 및 쿨타임 로직 (향후 확장 가능)
                    break;
            }
        }

        protected virtual void HandleMoveState() {
            if (_target == null) return;

            // 회전 업데이트 (Time Slicing)
            if (Time.time >= _nextRotationUpdateTime) {
                UpdateRotation();
                _nextRotationUpdateTime = Time.time + ROTATION_UPDATE_INTERVAL;
            }

            // 이동 방향 보간
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * 5f);
            
            // 이동 처리 (Rigidbody 지원)
            if (_rb != null) {
                Vector3 moveVelocity = transform.forward * MoveSpeed;
                _rb.linearVelocity = new Vector3(moveVelocity.x, _rb.linearVelocity.y, moveVelocity.z);
            } else {
                transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
            }

            // 거리 체크 (sqrMagnitude 최적화)
            float sqrDistance = (transform.position - _target.position).sqrMagnitude;
            if (sqrDistance <= _sqrAttackRange) {
                _currentState = EnemyState.Attack;
                AttackPlayer();
            }
        }

        protected virtual void UpdateRotation() {
            if (_target != null) {
                Vector3 direction = (_target.position - transform.position).normalized;
                if (direction != Vector3.zero) {
                    _targetRotation = Quaternion.LookRotation(direction);
                }
            }
        }

        protected virtual void Move() {
            // Deprecated: HandleMoveState()가 대신 처리합니다.
        }

        /// <summary>
        /// 적이 데미지를 받을 때 호출되는 메서드입니다.
        /// 하위 호환성을 위해 유지되며, Health 컴포넌트로 처리를 위임합니다.
        /// </summary>
        /// <param name="damage">입은 피해량</param>
        public void TakeDamage(int damage) {
            TakeDamage(new DamageInfo { Amount = damage });
        }

        public virtual void TakeDamage(DamageInfo damageInfo) {
            if (_healthComponent != null) {
                _healthComponent.TakeDamage(damageInfo);
            }
        }

        public virtual void ApplyKnockback(Vector3 force) {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddForce(force, ForceMode.Impulse);
            }
        }

        private void HandleDeath() {
            Potop.Client.Core.Events.EventBroker.Publish(new Potop.Client.Core.Events.EnemyDiedEvent { ScoreValue = ScoreValue });

            if (Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
            }
        }

        protected virtual void AttackPlayer() {
            Potop.Client.Core.Events.EventBroker.Publish(new Potop.Client.Core.Events.PlayerTakeDamageEvent { Damage = _damage });
            
            _currentState = EnemyState.Die;
            if (Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
            }
        }
    }
}
