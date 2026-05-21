using UnityEngine;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Meta;

namespace Potop.Client.Core {
    public class PlayerHealthController : MonoBehaviour {
        public static PlayerHealthController Instance { get; private set; }

        [SerializeField] private int _baseMaxHealth = 100;
        private int _maxHealth;
        public int MaxHealth => _maxHealth;

        private int _health;
        public int Health { get { return _health; } private set { _health = value; } }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start() {
            EventBroker.Subscribe<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
        }

        private void OnDestroy() {
            EventBroker.Unsubscribe<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
            if (Instance == this) {
                Instance = null;
            }
        }

        public void TakeDamage(int value) {
            Health = Mathf.Max(0, Health - value);
            EventBroker.Publish(new HealthChangedEvent { CurrentHealth = Health, MaxHealth = _maxHealth });

            if (Health <= 0 && GameManager.Instance != null && !GameManager.Instance.IsGameOver) {
                GameManager.Instance.TriggerGameOver();
            }
        }

        public void Heal(int amount) {
            if (amount <= 0) return;

            Health = Mathf.Min(_maxHealth, Health + amount);
            EventBroker.Publish(new HealthChangedEvent { CurrentHealth = Health, MaxHealth = _maxHealth });
        }

        private void OnPlayerTakeDamage(PlayerTakeDamageEvent e) {
            TakeDamage(e.Damage);
        }

        public void InitializeHealth() {
            int bonusHp = 0;
            if (MetaUpgradeManager.Instance != null) {
                bonusHp = MetaUpgradeManager.Instance.GetStatBundle().BonusHp;
            }
            _maxHealth = _baseMaxHealth + bonusHp;
            Health = _maxHealth;
            EventBroker.Publish(new HealthChangedEvent { CurrentHealth = Health, MaxHealth = _maxHealth });
        }
    }
}
