using Potop.Client.Core;
using Potop.Client.Data;
using UnityEngine;

using Potop.Client.Gameplay.Combat;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 적의 이동 및 플레이어 공격 로직을 처리하는 클래스입니다.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class EnemyBot : MonoBehaviour {
        [SerializeField] private EnemyData _enemyData;
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _attackRange = 2f;

        private Health _healthComponent;

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

        private Transform _target;

        private void Awake() {
            _healthComponent = GetComponent<Health>();
        }

        private void OnEnable() {
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

        private void OnDisable() {
            if (_healthComponent != null) {
                _healthComponent.OnDeath -= HandleDeath;
            }
        }

        private void Start() {
            // Initialization moved to OnEnable for pooling support
        }

        private void Update() {
            if (_target != null) {
                transform.LookAt(_target);
                transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);

                // 플레이어에 도달하면 데미지
                float distanceToPlayer = Vector3.Distance(transform.position, _target.position);
                if (distanceToPlayer <= _attackRange) {
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

        private void HandleDeath() {
            if (Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
            }
        }

        private void AttackPlayer() {
            if (GameManager.Instance != null) {
                GameManager.Instance.TakeDamage(_damage);
            }
            Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
        }
    }
}
