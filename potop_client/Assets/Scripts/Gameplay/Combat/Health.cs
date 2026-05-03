using UnityEngine;
using Potop.Client.Core.Events;
using System;

namespace Potop.Client.Gameplay.Combat {
    /// <summary>
    /// 게임 내 체력 시스템을 관리하는 공용 컴포넌트입니다.
    /// 플레이어, 적, 구조물 등 공격을 받을 수 있는 모든 객체에 부착하여 체력 관리 및 이벤트를 발행합니다.
    /// </summary>
    public class Health : MonoBehaviour, IDamageable {
        [SerializeField] private int _maxHealth = 100;

        private int _currentHealth;

        /// <summary>
        /// 현재 체력입니다.
        /// </summary>
        public int CurrentHealth => _currentHealth;

        /// <summary>
        /// 최대 체력입니다.
        /// </summary>
        public int MaxHealth => _maxHealth;

        /// <summary>
        /// 체력이 변경될 때 호출되는 이벤트입니다.
        /// </summary>
        public event Action<int, int> OnHealthChanged;

        /// <summary>
        /// 데미지를 입었을 때 호출되는 이벤트입니다.
        /// </summary>
        public event Action<DamageInfo> OnDamaged;

        /// <summary>
        /// 사망 시 호출되는 이벤트입니다.
        /// </summary>
        public event Action OnDeath;

        private void OnEnable() {
            _currentHealth = _maxHealth;
        }

        /// <summary>
        /// 최대 체력을 설정하고 현재 체력을 초기화합니다.
        /// </summary>
        /// <param name="maxHealth">새로운 최대 체력</param>
        public void InitializeHealth(int maxHealth) {
            _maxHealth = maxHealth;
            _currentHealth = _maxHealth;
        }

        /// <summary>
        /// 외부로부터 피해가 가해졌을 때 호출되는 메서드입니다.
        /// 체력 차감, 상태 이상 적용 등의 로직을 처리합니다.
        /// </summary>
        /// <param name="info">피해량 등의 정보</param>
        public void TakeDamage(DamageInfo info) {
            if (_currentHealth <= 0) return;

            _currentHealth -= info.Amount;

            OnDamaged?.Invoke(info);
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);

            // Publish globally
            EventBroker.Publish(new HealthChangedEvent {
                CurrentHealth = _currentHealth,
                MaxHealth = _maxHealth
            });

            if (_currentHealth <= 0) {
                OnDeath?.Invoke();
            }
        }
    }
}
