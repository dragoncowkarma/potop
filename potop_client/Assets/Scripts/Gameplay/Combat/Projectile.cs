using Potop.Client.Core;
using Potop.Client.Gameplay.Combat;
using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 발사체의 이동, 수명, 그리고 적과의 충돌 처리를 담당하는 클래스입니다.
    /// </summary>
    public class Projectile : MonoBehaviour {
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private int _damage = 10;

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

        /// <summary>
        /// 발사체를 초기화합니다.
        /// </summary>
        /// <param name="speed">투사체 속도</param>
        /// <param name="damage">가할 피해량</param>
        public void Initialize(float speed, int damage) {
            _speed = speed;
            _damage = damage;
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
            if (collision.gameObject.CompareTag(ENEMY_TAG)) {
                IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
                if (damageable != null) {
                    ContactPoint contact = collision.GetContact(0);
                    damageable.TakeDamage(new DamageInfo {
                        Amount = _damage,
                        HitPoint = contact.point,
                        HitNormal = contact.normal,
                        Instigator = gameObject
                    });
                    DespawnSelf();
                }
            }
        }
    }
}
