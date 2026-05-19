using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 투사체가 적을 관통하도록 하는 변이 로직입니다.
    /// 최대 관통 횟수 초과 시 투사체가 제거됩니다.
    /// </summary>
    public class PierceModifier : MonoBehaviour, IModifier {
        private const string ENEMY_TAG = "Enemy";
        [SerializeField, Min(1)] private int _maxPierceCount = 3;

        private int _currentPierceCount;
        private Projectile _projectile;

        /// <summary>
        /// 투사체에 관통 변이를 적용합니다.
        /// </summary>
        public void Apply(Projectile projectile) {
            _projectile = projectile;
            _currentPierceCount = _maxPierceCount;
        }

        /// <summary>
        /// 투사체에서 관통 변이를 제거합니다.
        /// </summary>
        public void Remove(Projectile projectile) {
            _projectile = null;
        }

        private void OnCollisionEnter(Collision collision) {
            if (_projectile == null) return;

            if (collision.gameObject.CompareTag(ENEMY_TAG)) {
                _currentPierceCount--;

                if (_currentPierceCount <= 0) {
                    Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
                }
            }
        }
    }
}
