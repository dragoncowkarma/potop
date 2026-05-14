using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 피버 레벨이 변경되었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct FeverLevelChangedEvent {
        /// <summary>
        /// 현재 피버 레벨입니다. (1~5)
        /// </summary>
        public int Level;
    }

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
            EventBroker.Subscribe<ComboChangedEvent>(OnComboChanged);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<ComboChangedEvent>(OnComboChanged);
            if (_isFeverActive) {
                DeactivateFever();
            }
        }

        private void Update() {
            if (_isFeverActive) {
                _feverTimer -= Time.deltaTime;
                
                EventBroker.Publish(new FeverProgressChangedEvent { Progress = Mathf.Clamp01(_feverTimer / _feverDuration) });

                if (_feverTimer <= 0) {
                    DeactivateFever();
                }
            }
        }

        private void OnComboChanged(ComboChangedEvent e) {
            CheckFeverLevel(e.ComboCount);

            if (_isFeverActive || e.ComboCount == 0) return;

            int scoreToAdd = Mathf.RoundToInt(e.BaseScore * e.Multiplier);
            _currentGauge += scoreToAdd;
            
            EventBroker.Publish(new FeverProgressChangedEvent { Progress = Mathf.Clamp01((float)_currentGauge / _maxGauge) });

            if (_currentGauge >= _maxGauge) {
                ActivateFever();
            }
        }

        private void CheckFeverLevel(int comboCount) {
            if (comboCount == 50) {
                EventBroker.Publish(new FeverLevelChangedEvent { Level = 1 });
            } else if (comboCount == 100) {
                EventBroker.Publish(new FeverLevelChangedEvent { Level = 2 });
            } else if (comboCount == 200) {
                EventBroker.Publish(new FeverLevelChangedEvent { Level = 3 });
            }
        }

        private void ActivateFever() {
            _isFeverActive = true;
            _currentGauge = 0;
            _feverTimer = _feverDuration;

            EventBroker.Publish(new FeverStateChangedEvent { IsFeverActive = true });
            EventBroker.Publish(new FeverProgressChangedEvent { Progress = 1f });
        }

        private void DeactivateFever() {
            _isFeverActive = false;

            EventBroker.Publish(new FeverStateChangedEvent { IsFeverActive = false });
            EventBroker.Publish(new FeverProgressChangedEvent { Progress = 0f });
        }
    }
}
