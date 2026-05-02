using Potop.Client.Core;
using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 발사체의 이동, 수명, 그리고 적과의 충돌 처리를 담당하는 클래스입니다.
    /// </summary>
    public class Projectile : MonoBehaviour {
        [SerializeField] private float _speed = 20f;
        [SerializeField] private float _lifeTime = 3f;

        /// <summary>
        /// 발사체의 이동 속도입니다.
        /// </summary>
        public float Speed => _speed;

        /// <summary>
        /// 발사체의 수명(초)입니다.
        /// </summary>
        public float LifeTime => _lifeTime;

        private const string ENEMY_TAG = "Enemy";

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
                // 점수 추가
                EnemyBot enemy = collision.gameObject.GetComponent<EnemyBot>();
                if (enemy != null && GameManager.Instance != null) {
                    GameManager.Instance.AddScore(enemy.ScoreValue);
                }

                Destroy(collision.gameObject);
                DespawnSelf();
            }
        }
    }
}
