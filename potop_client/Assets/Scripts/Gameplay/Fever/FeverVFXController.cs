using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Fever {
    /// <summary>
    /// 피버 모드 시각 효과(Post-processing)를 관리하는 컨트롤러입니다.
    /// </summary>
    [RequireComponent(typeof(Volume))]
    public class FeverVFXController : MonoBehaviour {
        [SerializeField] private Volume _volume;
        [SerializeField] private float _transitionSpeed = 5f;
        
        private Vignette _vignette;
        private ColorAdjustments _colorAdjustments;
        private bool _isFeverActive;
        private float _targetWeight;

        private void Awake() {
            if (_volume == null) {
                _volume = GetComponent<Volume>();
            }

            if (_volume != null && _volume.profile != null) {
                _volume.profile.TryGet(out _vignette);
                _volume.profile.TryGet(out _colorAdjustments);
            }
        }

        private void OnEnable() {
            EventBroker.Subscribe<FeverStateChangedEvent>(OnFeverStateChanged);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<FeverStateChangedEvent>(OnFeverStateChanged);
        }

        private void Update() {
            if (_volume == null) return;

            float currentWeight = _volume.weight;
            if (!Mathf.Approximately(currentWeight, _targetWeight)) {
                _volume.weight = Mathf.MoveTowards(currentWeight, _targetWeight, _transitionSpeed * Time.deltaTime);
            }
        }

        private void OnFeverStateChanged(FeverStateChangedEvent e) {
            _isFeverActive = e.IsFeverActive;
            _targetWeight = _isFeverActive ? 1f : 0f;
        }
    }
}
