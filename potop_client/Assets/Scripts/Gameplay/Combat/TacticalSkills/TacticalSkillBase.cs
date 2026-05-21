using UnityEngine;

namespace Potop.Client.Gameplay.Combat {
    public abstract class TacticalSkillBase : MonoBehaviour {
        [SerializeField] protected TacticalSkillData _skillData;

        protected float _lastUseTime = -9999f;

        public int EnergyCost => _skillData != null ? _skillData.EnergyCost : 0;
        public float Cooldown => _skillData != null ? _skillData.Cooldown : 0f;

        public float LastUseTime => _lastUseTime;

        public float GetRemainingCooldown() {
            return Mathf.Max(0f, Cooldown - (Time.time - _lastUseTime));
        }

        public bool CanExecute() {
            if (Time.time < _lastUseTime + Cooldown) return false;
            if (EnergyManager.Instance == null || EnergyManager.Instance.CurrentEnergy < EnergyCost) return false;
            return true;
        }

        public virtual bool TryExecute() {
            if (!CanExecute()) return false;

            if (EnergyManager.Instance.ConsumeEnergy(EnergyCost)) {
                _lastUseTime = Time.time;
                Execute();
                Potop.Client.Core.Events.EventBroker.Publish(new Potop.Client.Core.Events.EnergyChangedEvent {
                    CurrentEnergy = EnergyManager.Instance.CurrentEnergy,
                    MaxEnergy = EnergyManager.MAX_ENERGY
                });
                return true;
            }
            return false;
        }

        public abstract void Execute();
    }

    public static class EnemyBaseExtensions {
        public static void Stun(this EnemyBase enemy, float duration) {
            if (enemy == null || !enemy.gameObject.activeInHierarchy || enemy.StateMachine == null) return;

            enemy.StartCoroutine(StunRoutine(enemy, duration));
        }

        private static System.Collections.IEnumerator StunRoutine(EnemyBase enemy, float duration) {
            var prevState = enemy.StateMachine.CurrentState;
            enemy.StateMachine.ChangeState(EnemyBase.IdleState, enemy);

            yield return new WaitForSeconds(duration);

            if (enemy != null && enemy.gameObject.activeInHierarchy && enemy.StateMachine.CurrentState == EnemyBase.IdleState) {
                // Restore its previous state
                enemy.StateMachine.ChangeState(prevState, enemy);
            }
        }
    }
}
