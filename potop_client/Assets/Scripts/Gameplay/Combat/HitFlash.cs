using System.Collections;
using UnityEngine;

namespace Potop.Client.Gameplay.Combat {
    /// <summary>
    /// 피격 시 MaterialPropertyBlock을 사용하여 렌더러의 _EmissionColor를 순간적으로 변경하는 컴포넌트입니다.
    /// 새로운 머티리얼 인스턴스를 생성하지 않아 메모리 안전합니다.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class HitFlash : MonoBehaviour {
        [Header("Flash Settings")]
        [SerializeField] private Color _flashColor = Color.white;
        [SerializeField] private float _flashDuration = 0.1f;

        private static readonly int EMISSION_COLOR_ID = Shader.PropertyToID("_EmissionColor");

        private Health _health;
        private Renderer _renderer;
        private MaterialPropertyBlock _propertyBlock;
        private Coroutine _flashCoroutine;

        private void Awake() {
            _health = GetComponent<Health>();
            _renderer = GetComponentInChildren<Renderer>();
            _propertyBlock = new MaterialPropertyBlock();
        }

        private void OnEnable() {
            _health.OnDamaged += HandleDamaged;
        }

        private void OnDisable() {
            _health.OnDamaged -= HandleDamaged;
            ResetEmission();
        }

        private void HandleDamaged(DamageInfo info) {
            if (_renderer == null) return;

            // Restart flash coroutine on each hit for responsive feedback
            if (_flashCoroutine != null) {
                StopCoroutine(_flashCoroutine);
            }
            _flashCoroutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine() {
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(EMISSION_COLOR_ID, _flashColor);
            _renderer.SetPropertyBlock(_propertyBlock);

            yield return new WaitForSeconds(_flashDuration);

            ResetEmission();
            _flashCoroutine = null;
        }

        private void ResetEmission() {
            if (_renderer == null) return;

            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(EMISSION_COLOR_ID, Color.black);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}
