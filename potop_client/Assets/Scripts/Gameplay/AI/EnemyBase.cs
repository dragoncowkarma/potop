using Potop.Client.Core;
using Potop.Client.Data;
using UnityEngine;

using Potop.Client.Gameplay.Combat;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 적의 공통 이동 로직과 IDamageable 통합을 처리하는 추상 기본 클래스입니다.
    /// 구체적인 적의 행동은 이 클래스를 상속받아 구현해야 합니다.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public abstract class EnemyBase : MonoBehaviour {
        [SerializeField] protected EnemyData _enemyData;
        [SerializeField] protected int _damage = 10;
        [SerializeField] protected float _attackRange = 2f;

        protected Health _healthComponent;
        protected float _sqrAttackRange;

        /// <summary>
        /// 적의 이동 속도입니다.
        /// </summary>
        public float MoveSpeed => _enemyData != null ? _enemyData.MoveSpeed : 0f;

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

        protected virtual void Awake() {
            _healthComponent = GetComponent<Health>();
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
            if (_target != null) {
                transform.LookAt(_target);
                transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);

                // 플레이어에 도달하면 데미지 (sqrMagnitude를 이용한 최적화)
                if ((transform.position - _target.position).sqrMagnitude <= _sqrAttackRange) {
                    AttackPlayer();
                }
            }
        }

        /// <summary>
        /// 적이 데미지를 받을 때 호출되는 메서드입니다.
        /// 하위 호환성을 위해 유지되며, Health 컴포넌트로 처리를 위임합니다.
        /// </summary>
        /// <param name="damage">입은 피해량</param>
        public void TakeDamage(int damage) {
            if (_healthComponent != null) {
                _healthComponent.TakeDamage(new DamageInfo { Amount = damage });
            }
        }

        protected virtual void HandleDeath() {
            if (Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
            }
        }

        protected virtual void AttackPlayer() {
            if (GameManager.Instance != null) {
                GameManager.Instance.TakeDamage(_damage);
            }
            Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
        }
    }
}
