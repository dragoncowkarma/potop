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
        private Collider[] _swarmColliders = new Collider[20];
        private Rigidbody _rb;

        protected virtual void Awake() {
            _rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// 무리 지어 이동하는 특성을 구현하기 위해 이동 로직을 재정의합니다.
        /// 개별적인 이동보다는 주변 개체들과 일정 거리를 유지하며 함께 움직이는 군집 행동을 모사합니다.
        /// </summary>
        protected override void Move() {
            if (Target != null) {
                // GC를 방지하기 위해 NonAlloc 및 LayerMask 사용
                int hitCount = Physics.OverlapSphereNonAlloc(transform.position, _groupingRadius, _swarmColliders, _swarmLayer);
                int swarmCount = 0;
                for (int i = 0; i < hitCount; i++) {
                    // GetComponent 대신 태그를 사용하여 식별 비용을 최소화합니다.
                    if (_swarmColliders[i].CompareTag(gameObject.tag)) {
                        swarmCount++;
                    }
                }

                float currentSpeed = MoveSpeed;
                if (swarmCount > 1) {
                    currentSpeed *= _swarmSpeedMultiplier;
                }

                Vector3 targetPosition = new Vector3(Target.position.x, transform.position.y, Target.position.z);
                transform.LookAt(targetPosition);

                if (_rb != null) {
                    Vector3 moveVelocity = transform.forward * currentSpeed;
                    _rb.linearVelocity = new Vector3(moveVelocity.x, _rb.linearVelocity.y, moveVelocity.z);
                } else {
                    transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
                }
            }
        }
    }
}
