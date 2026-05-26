using UnityEngine;

namespace Potop.Client.Gameplay.VFX {
    /// <summary>
    /// Rotates the shield ring at 120 RPM (720 degrees/second).
    /// Upon entering Phase 2, decouples from the parent and triggers particle effects.
    /// </summary>
    public class ShieldRingRotator : MonoBehaviour {
        [SerializeField] private float _rotationSpeed = 720f; // 120 RPM = 120 * 360 / 60 = 720 deg/s
        [SerializeField] private ParticleSystem _phase2Particles;

        private bool _isDecoupled = false;

        public bool IsDecoupled => _isDecoupled;
        public float RotationSpeed => _rotationSpeed;

        private void Update() {
            if (!_isDecoupled) {
                transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// Decouples the shield ring from its parent and plays the associated particle effect.
        /// </summary>
        public void DecoupleAndTriggerVFX() {
            if (_isDecoupled) return;
            _isDecoupled = true;
            transform.SetParent(null);
            if (_phase2Particles != null) {
                _phase2Particles.Play();
            }
        }
    }
}
