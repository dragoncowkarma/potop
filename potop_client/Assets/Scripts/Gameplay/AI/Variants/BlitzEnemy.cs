using UnityEngine;

namespace Potop.Client.Gameplay.AI.Variants {
    /// <summary>
    /// 방어선의 빈틈을 빠르게 파고들어 진형을 무너뜨리기 위해 설계된 적 변종입니다.
    /// 높은 기동성을 통해 플레이어를 압박하지만 생존력은 낮습니다.
    /// </summary>
    public class BlitzEnemy : EnemyBase {
        [SerializeField] private float _speedMultiplier = 1.5f;

        public override float MoveSpeed => base.MoveSpeed * _speedMultiplier;
    }
}
