using UnityEngine;

namespace Potop.Client.Gameplay.AI.Variants {
    /// <summary>
    /// 방어선의 빈틈을 빠르게 파고들어 진형을 무너뜨리기 위해 설계된 적 변종입니다.
    /// 높은 기동성을 통해 플레이어를 압박하지만 생존력은 낮습니다.
    /// </summary>
    public class BlitzEnemy : EnemyBase {
        [SerializeField] private float _speedMultiplier = 1.5f;

        /// <summary>
        /// 특유의 빠른 이동 속도를 적용하기 위해 기본 이동 로직을 재정의합니다.
        /// 빠른 접근이라는 돌격형 적의 기획적 의도를 달성하기 위해 이동 배율을 곱합니다.
        /// </summary>
        protected override void Move() {
            if (Target != null) {
                Vector3 targetPosition = new Vector3(Target.position.x, transform.position.y, Target.position.z);
                transform.LookAt(targetPosition);
                transform.Translate(Vector3.forward * (MoveSpeed * _speedMultiplier) * Time.deltaTime);
            }
        }
    }
}
