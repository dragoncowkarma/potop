using UnityEngine;
using Potop.Client.Gameplay.Combat;

namespace Potop.Client.Gameplay.SFX {
    /// <summary>
    /// 피격/사망 이벤트에 반응하여 효과음을 재생하는 컴포넌트입니다.
    /// Health 컴포넌트와 동일한 GameObject에 부착하여 사용합니다.
    /// </summary>
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(AudioSource))]
    public class SFXTrigger : MonoBehaviour {
        [Header("Sound Clips")]
        [SerializeField] private AudioClip _hitClip;
        [SerializeField] private AudioClip _deathClip;

        [Header("Volume")]
        [SerializeField, Range(0f, 1f)] private float _hitVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float _deathVolume = 1f;

        private Health _health;
        private AudioSource _audioSource;

        private void Awake() {
            _health = GetComponent<Health>();
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable() {
            _health.OnDamaged += HandleDamaged;
            _health.OnDeath += HandleDeath;
        }

        private void OnDisable() {
            _health.OnDamaged -= HandleDamaged;
            _health.OnDeath -= HandleDeath;
        }

        private void HandleDamaged(DamageInfo info) {
            PlayClip(_hitClip, _hitVolume);
        }

        private void HandleDeath() {
            PlayClip(_deathClip, _deathVolume);
        }

        private void PlayClip(AudioClip clip, float volume) {
            if (clip == null || _audioSource == null) return;
            // PlayOneShot allows overlapping calls without interrupting current playback
            _audioSource.PlayOneShot(clip, volume);
        }
    }
}
