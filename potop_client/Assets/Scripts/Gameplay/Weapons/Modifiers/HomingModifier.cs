using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 투사체가 가장 가까운 적을 향해 유도되도록 하는 변이 로직입니다.
    /// 매 프레임 일정 속도로 적을 향해 회전합니다.
    /// </summary>
    public class HomingModifier : MonoBehaviour, IModifier {
        [SerializeField, Min(1f)] private float _rotationSpeed = 90f;
        [SerializeField, Min(1f)] private float _searchRadius = 20f;

        private Projectile _projectile;
        private Transform _target;
        private int _enemyLayerMask;
        private readonly Collider[] _hitColliders = new Collider[MAX_TARGETS];
        private const string ENEMY_LAYER_NAME = "Enemy";
        private const int MAX_TARGETS = 10;

        private void Awake() {
            _enemyLayerMask = LayerMask.GetMask(ENEMY_LAYER_NAME);
        }

        /// <summary>
        /// 투사체에 유도 변이를 적용합니다.
        /// </summary>
        public void Apply(Projectile projectile) {
            _projectile = projectile;
            FindTarget();
        }

        /// <summary>
        /// 투사체에서 유도 변이를 제거합니다.
        /// </summary>
        public void Remove(Projectile projectile) {
            _projectile = null;
            _target = null;
        }

        private void Update() {
            if (_projectile == null) return;

            if (_target == null || !_target.gameObject.activeInHierarchy) {
                FindTarget();
            }

            if (_target != null) {
                Vector3 targetDirection = (_target.position - transform.position).normalized;

                if (targetDirection != Vector3.zero) {
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f);
                    transform.forward = newDirection;
                }
            }
        }

        private void FindTarget() {
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
