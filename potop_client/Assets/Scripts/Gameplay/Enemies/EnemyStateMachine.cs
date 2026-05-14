using UnityEngine;

namespace Potop.Client.Gameplay {
    public interface IEnemyState {
        void Enter(EnemyBase enemy);
        void Update(EnemyBase enemy);
        void Exit(EnemyBase enemy);
    }

    public class EnemyStateMachine {
        public IEnemyState CurrentState { get; private set; }

        public void ChangeState(IEnemyState newState, EnemyBase enemy) {
            if (CurrentState != null) {
                CurrentState.Exit(enemy);
            }

            CurrentState = newState;

            if (CurrentState != null) {
                CurrentState.Enter(enemy);
            }
        }

        public void Update(EnemyBase enemy) {
            if (CurrentState != null) {
                CurrentState.Update(enemy);
            }
        }
    }

    public class EnemyIdleState : IEnemyState {
        public void Enter(EnemyBase enemy) {
        }

        public void Update(EnemyBase enemy) {
        }

        public void Exit(EnemyBase enemy) {
        }
    }

    public class EnemyChaseState : IEnemyState {
        public void Enter(EnemyBase enemy) {
        }

        public void Update(EnemyBase enemy) {
            enemy.UpdateMovement();

            if (enemy.Target != null) {
                float sqrDistance = (enemy.transform.position - enemy.Target.position).sqrMagnitude;
                if (sqrDistance <= enemy.SqrAttackRange) {
                    enemy.StateMachine.ChangeState(enemy.AttackState, enemy);
                }
            }
        }

        public void Exit(EnemyBase enemy) {
        }
    }

    public class EnemyAttackState : IEnemyState {
        public void Enter(EnemyBase enemy) {
            enemy.TriggerAttack();
        }

        public void Update(EnemyBase enemy) {
        }

        public void Exit(EnemyBase enemy) {
        }
    }

    public class EnemyDeathState : IEnemyState {
        public void Enter(EnemyBase enemy) {
        }

        public void Update(EnemyBase enemy) {
        }

        public void Exit(EnemyBase enemy) {
        }
    }
}
