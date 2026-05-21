using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Combat {
    public class EnergyManager : MonoBehaviour {
        public static EnergyManager Instance { get; private set; }

        public int CurrentEnergy { get; private set; }
        public const int MAX_ENERGY = 1000;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }
        }

        private void OnEnable() {
            EventBroker.Subscribe<EnemyDiedEvent>(OnEnemyDied);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<EnemyDiedEvent>(OnEnemyDied);
        }

        private void OnEnemyDied(EnemyDiedEvent e) {
            AddEnergy(e.ScoreValue > 0 ? e.ScoreValue : 10);
        }

        public void AddEnergy(int amount) {
            CurrentEnergy = Mathf.Min(CurrentEnergy + amount, MAX_ENERGY);
        }

        public bool ConsumeEnergy(int amount) {
            if (CurrentEnergy >= amount) {
                CurrentEnergy -= amount;
                return true;
            }
            return false;
        }
    }
}
