using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 콤보 시스템의 변경 사항을 전달하는 이벤트입니다.
    /// </summary>
    public struct ComboChangedEvent {
        /// <summary>
        /// 현재 콤보 횟수입니다.
        /// </summary>
        public int ComboCount;

        /// <summary>
        /// 현재 콤보에 따른 점수 배율입니다.
        /// </summary>
        public float Multiplier;

        /// <summary>
        /// 적 처치로 인해 획득한 기본 점수입니다.
        /// </summary>
        public int BaseScore;
    }

    /// <summary>
    /// 적 처치 이벤트를 구독하여 콤보를 계산하고 배율을 적용하는 시스템입니다.
    /// </summary>
    public class ComboCalculator : MonoBehaviour {
        private const float COMBO_TIMEOUT = 2f;
        private const float MULTIPLIER_PER_10_COMBO = 0.1f;
        private const float MAX_MULTIPLIER = 5.0f;

        private int _currentCombo;
        private float _lastKillTime;

        private void OnEnable() {
            EventBroker.Subscribe<EnemyDiedEvent>(OnEnemyDied);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<EnemyDiedEvent>(OnEnemyDied);
        }

        private void Update() {
            if (_currentCombo > 0 && Time.time - _lastKillTime > COMBO_TIMEOUT) {
                ResetCombo();
            }
        }

        private void OnEnemyDied(EnemyDiedEvent e) {
            _lastKillTime = Time.time;
            _currentCombo++;

            float multiplier = CalculateMultiplier();

            EventBroker.Publish(new ComboChangedEvent {
                ComboCount = _currentCombo,
                Multiplier = multiplier,
                BaseScore = e.ScoreValue
            });
        }

        private float CalculateMultiplier() {
            float calculatedMultiplier = 1.0f + (_currentCombo / 10) * MULTIPLIER_PER_10_COMBO;
            return Mathf.Min(calculatedMultiplier, MAX_MULTIPLIER);
        }

        private void ResetCombo() {
            _currentCombo = 0;
            EventBroker.Publish(new ComboChangedEvent {
                ComboCount = _currentCombo,
                Multiplier = 1.0f,
                BaseScore = 0
            });
        }
    }
}
