using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Fever {
    /// <summary>
    /// 피버 게이지를 관리하고 피버 모드 상태를 제어하는 매니저 클래스입니다.
    /// </summary>
    public class FeverManager : MonoBehaviour {
        [SerializeField, Min(1)] private int _maxGauge = 100;
        [SerializeField, Min(0.1f)] private float _feverDuration = 5f;

        private int _currentGauge;
        private float _feverTimer;
        private bool _isFeverActive;

        private void OnEnable() {
            EventBroker.Subscribe<EnemyDiedEvent>(OnEnemyDied);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<EnemyDiedEvent>(OnEnemyDied);
            if (_isFeverActive) {
                DeactivateFever();
            }
        }

        private void Update() {
            if (_isFeverActive) {
                _feverTimer -= Time.deltaTime;

                if (_feverTimer <= 0) {
                    DeactivateFever();
                }
            }
        }

        private void OnEnemyDied(EnemyDiedEvent e) {
            if (_isFeverActive) return;

            _currentGauge += e.ScoreValue;

            if (_currentGauge >= _maxGauge) {
                ActivateFever();
            }
        }

        private void ActivateFever() {
            _isFeverActive = true;
            _currentGauge = 0;
            _feverTimer = _feverDuration;

            EventBroker.Publish(new FeverStateChangedEvent { IsFeverActive = true });
        }

        private void DeactivateFever() {
            _isFeverActive = false;

            EventBroker.Publish(new FeverStateChangedEvent { IsFeverActive = false });
        }
    }
}
