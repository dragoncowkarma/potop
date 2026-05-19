using UnityEngine;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 투사체의 크기를 배율에 따라 확대하는 변이 로직입니다.
    /// 스케일을 조절하여 충돌 범위도 비례하여 확대됩니다.
    /// </summary>
    public class ScaleModifier : MonoBehaviour, IModifier {
        [SerializeField, Min(1.1f)] private float _scaleMultiplier = 1.5f;

        private Projectile _projectile;
        private Vector3 _originalScale;

        private void Awake() {
            _originalScale = transform.localScale;
        }

        /// <summary>
        /// 투사체에 거대화 변이를 적용합니다.
        /// </summary>
        public void Apply(Projectile projectile) {
            _projectile = projectile;
            transform.localScale = _originalScale * _scaleMultiplier;
        }

        /// <summary>
        /// 투사체에서 거대화 변이를 제거합니다.
        /// </summary>
        public void Remove(Projectile projectile) {
            _projectile = null;
            transform.localScale = _originalScale;
        }
    }
}
