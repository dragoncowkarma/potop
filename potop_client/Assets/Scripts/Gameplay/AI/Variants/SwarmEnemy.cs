using UnityEngine;

namespace Potop.Client.Gameplay.AI.Variants {
    /// <summary>
    /// 단일 개체의 위협은 낮지만 물량 공세로 플레이어의 탄약을 소진시키고 시야를 방해하는 무리형 적 변종입니다.
    /// 체력은 낮지만 대량으로 스폰되어 압박감을 조성합니다.
    /// </summary>
    public class SwarmEnemy : EnemyBase {
        [SerializeField] private float _groupingRadius = 3f;
        [SerializeField] private float _swarmSpeedMultiplier = 1.2f;
        [SerializeField] private LayerMask _swarmLayer;
        
        private Collider[] _swarmColliders = new Collider[10];
        private float _currentMultiplier = 1f;
        private float _nextSwarmCheckTime;
        private const float SWARM_CHECK_INTERVAL = 0.5f;

        public override float MoveSpeed => base.MoveSpeed * _currentMultiplier;

        protected override void Update() {
            base.Update();
            
            if (Time.time >= _nextSwarmCheckTime) {
                UpdateSwarmMultiplier();
                _nextSwarmCheckTime = Time.time + SWARM_CHECK_INTERVAL;
            }
        }

        private void UpdateSwarmMultiplier() {
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _groupingRadius, _swarmColliders, _swarmLayer);
            int swarmCount = 0;
            for (int i = 0; i < hitCount; i++) {
                if (_swarmColliders[i].gameObject != gameObject && _swarmColliders[i].CompareTag(gameObject.tag)) {
                    swarmCount++;
                }
            }
            _currentMultiplier = (swarmCount > 0) ? _swarmSpeedMultiplier : 1f;
        }
    }
}
