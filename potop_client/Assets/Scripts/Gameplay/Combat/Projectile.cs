using Potop.Client.Core;
using Potop.Client.Gameplay.Combat;
using Potop.Client.Core.Events;
using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 발사체의 이동, 수명, 그리고 적과의 충돌 처리를 담당하는 클래스입니다.
    /// </summary>
    public class Projectile : MonoBehaviour {
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private int _damage = 10;

        private float _aoeRadius;
        private int _currentPierceCount;
        private float _knockbackForce;
        private int _enemyLayerMask;

        private readonly Collider[] _hitColliders = new Collider[MAX_AOE_TARGETS];

        private const float HEAVY_IMPACT_THRESHOLD = 50f;
        private const float IMPACT_INTENSITY_MULTIPLIER = 0.1f;

        /// <summary>
        /// 발사체의 이동 속도입니다.
        /// </summary>
        public float Speed => _speed;

        /// <summary>
        /// 발사체의 수명(초)입니다.
        /// </summary>
        public float LifeTime => _lifeTime;

        /// <summary>
        /// 발사체의 피해량입니다.
        /// </summary>
        public int Damage => _damage;

        private const string ENEMY_TAG = "Enemy";
        private const string ENEMY_LAYER_NAME = "Enemy";
        private const int MAX_AOE_TARGETS = 20;

        /// <summary>
        /// 기본 발사체를 초기화합니다.
        /// </summary>
        /// <param name="speed">투사체 속도</param>
        /// <param name="damage">가할 피해량</param>
        public void Initialize(float speed, int damage) {
            Initialize(speed, damage, 0f, 0, 0f);
        }

        /// <summary>
        /// 확장된 발사체를 초기화합니다.
        /// </summary>
        /// <param name="speed">투사체 속도</param>
        /// <param name="damage">가할 피해량</param>
        /// <param name="aoeRadius">범위 공격 반경 (0이면 단일 대상)</param>
        /// <param name="pierceCount">관통 가능 횟수 (0이면 충돌 시 즉시 파괴)</param>
        /// <param name="knockbackForce">넉백 힘 (0이면 넉백 없음)</param>
        public void Initialize(float speed, int damage, float aoeRadius, int pierceCount, float knockbackForce) {
            _speed = speed;
            _damage = damage;
            _aoeRadius = aoeRadius;
            _currentPierceCount = pierceCount;
            _knockbackForce = knockbackForce;
        }

        private void Awake() {
            _enemyLayerMask = LayerMask.GetMask(ENEMY_LAYER_NAME);
        }

        private void OnEnable() {
            Invoke(nameof(DespawnSelf), _lifeTime);
        }

        private void OnDisable() {
            CancelInvoke(nameof(DespawnSelf));
        }

        private void DespawnSelf() {
            Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
        }

        private void Update() {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision) {
            if (!collision.gameObject.CompareTag(ENEMY_TAG)) return;

            ContactPoint contact = collision.GetContact(0);

            if (_aoeRadius > 0) {
                int hitCount = Physics.OverlapSphereNonAlloc(
                    contact.point,
                    _aoeRadius,
                    _hitColliders,
                    _enemyLayerMask
                );

                for (int i = 0; i < hitCount; i++) {
                    GameObject hitObj = _hitColliders[i].gameObject;
                    Vector3 radialDirection = (hitObj.transform.position - contact.point).normalized;
                    // AoE의 경우 넉백 방향을 방사형으로 전달
                    ProcessHit(hitObj, hitObj.transform.position, radialDirection, radialDirection);
                }
            } else {
                // 단일 대상의 경우 넉백 방향은 발사체의 전방(transform.forward) 사용
                ProcessHit(collision.gameObject, contact.point, contact.normal, transform.forward);
            }

            if (_currentPierceCount > 0) {
                _currentPierceCount--;
            } else {
                DespawnSelf();
            }
        }

        private void ProcessHit(GameObject target, Vector3 hitPoint, Vector3 hitNormal, Vector3 knockbackDirection) {
            if (target.TryGetComponent<IDamageable>(out var damageable)) {
                damageable.TakeDamage(new DamageInfo {
                    Amount = _damage,
                    HitPoint = hitPoint,
                    HitNormal = hitNormal,
                    Instigator = gameObject
                });

                // Publish impact event for camera shake/feedback
                EventBroker.Publish(new CombatImpactEvent {
                    Position = hitPoint,
                    Intensity = _damage * IMPACT_INTENSITY_MULTIPLIER,
                    IsHeavy = _aoeRadius > 0 || _damage >= HEAVY_IMPACT_THRESHOLD
                });

                if (_knockbackForce > 0 && target.TryGetComponent<Rigidbody>(out var rb)) {
                    rb.AddForce(knockbackDirection * _knockbackForce, ForceMode.Impulse);
                }
            }
        }
    }
}

