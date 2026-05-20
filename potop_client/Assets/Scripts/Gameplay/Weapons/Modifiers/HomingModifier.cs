using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 투사체가 가장 가까운 적을 향해 유도되도록 하는 변이 로직입니다.
    /// 틱 기반 탐색으로 성능을 최적화하고 일정 속도로 적을 향해 회전합니다.
    /// </summary>
    public class HomingModifier : MonoBehaviour, IModifier {
        [SerializeField, Min(1f)] private float _rotationSpeed = 90f;
        [SerializeField, Min(1f)] private float _searchRadius = 20f;
        [SerializeField, Min(0.1f)] private float _searchInterval = 0.5f;

        private Projectile _projectile;
        private Transform _target;
        private int _enemyLayerMask;
        private Rigidbody _rigidbody;

        private static readonly Collider[] _hitColliders = new Collider[MAX_TARGETS];
        private const string ENEMY_LAYER_NAME = "Enemy";
        private const int MAX_TARGETS = 10;

        private void Awake() {
            _enemyLayerMask = LayerMask.GetMask(ENEMY_LAYER_NAME);
            _rigidbody = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// 투사체에 유도 변이를 적용하고 주기적인 탐색을 시작합니다.
        /// </summary>
        public void Apply(Projectile projectile) {
            _projectile = projectile;
            InvokeRepeating(nameof(FindClosestEnemy), 0f, _searchInterval);
        }

        /// <summary>
        /// 투사체에서 유도 변이를 제거하고 탐색을 중지합니다.
        /// </summary>
        public void Remove(Projectile projectile) {
            _projectile = null;
            _target = null;
            CancelInvoke(nameof(FindClosestEnemy));
        }

        private void Update() {
            if (_projectile == null || _target == null || !_target.gameObject.activeInHierarchy) return;

            Vector3 targetDirection = (_target.position - transform.position).normalized;

            if (targetDirection != Vector3.zero) {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f);
                transform.forward = newDirection;

                if (_rigidbody != null) {
                    _rigidbody.linearVelocity = newDirection * _rigidbody.linearVelocity.magnitude;
                }
            }
        }

        private void FindClosestEnemy() {
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _searchRadius, _hitColliders, _enemyLayerMask);

            float closestDistance = float.MaxValue;
            Transform closestTarget = null;

            for (int i = 0; i < hitCount; i++) {
                float distance = Vector3.SqrMagnitude(transform.position - _hitColliders[i].transform.position);
                if (distance < closestDistance) {
                    closestDistance = distance;
                    closestTarget = _hitColliders[i].transform;
                }
            }

            _target = closestTarget;
        }
    }
}
