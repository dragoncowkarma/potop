using UnityEngine;

namespace Potop.Client.Gameplay.Combat {
    public abstract class TacticalSkillBase : MonoBehaviour {
        [SerializeField] protected TacticalSkillData _skillData;

        protected float _lastUseTime = -9999f;

        public int EnergyCost => _skillData != null ? _skillData.EnergyCost : 0;
        public float Cooldown => _skillData != null ? _skillData.Cooldown : 0f;

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
                return true;
            }
            return false;
        }

        public abstract void Execute();
    }

    public static class EnemyBaseExtensions {
        public static async void Stun(this EnemyBase enemy, float duration) {
            if (enemy == null || enemy.StateMachine == null) return;

            var prevState = enemy.StateMachine.CurrentState;
            enemy.StateMachine.ChangeState(EnemyBase.IdleState, enemy);

            await System.Threading.Tasks.Task.Delay(Mathf.RoundToInt(duration * 1000));

            if (enemy != null && enemy.gameObject.activeInHierarchy && enemy.StateMachine.CurrentState == EnemyBase.IdleState) {
                // If it's still idle (meaning it wasn't dead or changed by something else), restore its previous state
                // Actually, usually they go back to ChaseState.
                enemy.StateMachine.ChangeState(EnemyBase.ChaseState, enemy);
            }
        }
    }
}
