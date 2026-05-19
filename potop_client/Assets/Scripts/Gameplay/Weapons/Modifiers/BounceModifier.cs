using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 투사체가 벽이나 적에 충돌 시 도탄되도록 하는 변이 로직입니다.
    /// Vector3.Reflect를 사용하여 반사각을 계산합니다.
    /// </summary>
    public class BounceModifier : MonoBehaviour, IModifier {
        [SerializeField, Min(1)] private int _maxBounceCount = 3;

        private int _currentBounceCount;
        private Projectile _projectile;

        /// <summary>
        /// 투사체에 도탄 변이를 적용합니다.
        /// </summary>
        public void Apply(Projectile projectile) {
            _projectile = projectile;
            _currentBounceCount = _maxBounceCount;
        }

        /// <summary>
        /// 투사체에서 도탄 변이를 제거합니다.
        /// </summary>
        public void Remove(Projectile projectile) {
            _projectile = null;
        }

        private void OnCollisionEnter(Collision collision) {
            if (_projectile == null) return;

            _currentBounceCount--;

            if (_currentBounceCount < 0) {
                Potop.Client.Core.Pooling.PoolManager.Instance.Despawn(gameObject);
                return;
            }

            ContactPoint contact = collision.GetContact(0);
            Vector3 reflectedDirection = Vector3.Reflect(transform.forward, contact.normal);

            transform.forward = reflectedDirection.normalized;
        }
    }
}
