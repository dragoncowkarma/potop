using Potop.Client.Core;
using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 적의 이동 및 플레이어 공격 로직을 처리하는 클래스입니다.
    /// </summary>
    public class EnemyBot : MonoBehaviour {
        [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private int _health = 10;
        [SerializeField] private int _damage = 10;
        [SerializeField] private int _scoreValue = 100;
        [SerializeField] private float _attackRange = 2f;

        private Transform _target;

        /// <summary>
        /// 적이 처치되었을 때 획득할 수 있는 점수입니다.
        /// </summary>
        public int ScoreValue => _scoreValue;

        private void Start() {
            if (GameManager.Instance != null) {
                _target = GameManager.Instance.PlayerTransform;
            }
        }

        private void Update() {
            if (_target != null) {
                transform.LookAt(_target);
                transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);

                // 플레이어에 도달하면 데미지
                float distanceToPlayer = Vector3.Distance(transform.position, _target.position);
                if (distanceToPlayer <= _attackRange) {
                    AttackPlayer();
                }
            }
        }

        /// <summary>
        /// 적이 데미지를 받을 때 호출되는 메서드입니다.
        /// </summary>
        /// <param name="damage">입은 피해량</param>
        public void TakeDamage(int damage) {
            _health -= damage;
            if (_health <= 0) {
                Destroy(gameObject);
            }
        }

        private void AttackPlayer() {
            if (GameManager.Instance != null) {
                GameManager.Instance.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}
