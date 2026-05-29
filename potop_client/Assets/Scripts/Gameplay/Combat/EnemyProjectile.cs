using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Combat {
    /// <summary>
    /// 적(보스 등)이 발사한 투사체의 이동 및 플레이어 피격 처리를 담당하는 클래스입니다.
    /// </summary>
    public class EnemyProjectile : MonoBehaviour {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _lifeTime = 3f;
        [SerializeField] private int _damage = 5;

        public float Speed => _speed;
        public float LifeTime => _lifeTime;
        public int Damage => _damage;

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

        private void Update() {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.CompareTag("Player")) {
                EventBroker.Publish(new PlayerTakeDamageEvent { Damage = _damage });
                DespawnSelf();
            }
        }

        private void DespawnSelf() {
            if (Potop.Client.Core.Pooling.PoolManager.Instance != null) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
