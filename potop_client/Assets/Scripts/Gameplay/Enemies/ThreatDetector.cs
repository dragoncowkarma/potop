using UnityEngine;
using Potop.Client.Core;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Enemies {
    public enum ThreatLevel {
        None,
        Yellow,
        Orange,
        Red
    }

    /// <summary>
    /// 플레이어 주변의 가장 가까운 적을 감지하여 거리별 위협 수준(Yellow: 15m, Orange: 8m, Red: 3m)과 
    /// 방향 각도를 계산해 EventBroker로 매 프레임 쓰레기(GC) 생성 없이 이벤트를 발행하는 컴포넌트입니다.
    /// </summary>
    public struct ThreatUpdateEvent {
        public ThreatLevel Level;
        public float Distance;
        public float Angle;
        public Vector3 Direction;
    }

    public class ThreatDetector : MonoBehaviour {
        [Header("Distance Thresholds")]
        [SerializeField] private float _yellowThreshold = 15f;
        [SerializeField] private float _orangeThreshold = 8f;
        [SerializeField] private float _redThreshold = 3f;

        private void Update() {
            if (GameManager.Instance == null || GameManager.Instance.PlayerTransform == null) {
                return;
            }

            Transform player = GameManager.Instance.PlayerTransform;
            Vector3 playerPos = player.position;

            EnemyBase closestEnemy = null;
            float minSqrDistance = float.MaxValue;

            // 정적 리스트를 순회하여 최단 거리 탐색 (가비지 프리)
            int count = EnemyBase.ActiveEnemies.Count;
            for (int i = 0; i < count; i++) {
                EnemyBase enemy = EnemyBase.ActiveEnemies[i];
                if (enemy == null) continue;

                float sqrDist = (enemy.transform.position - playerPos).sqrMagnitude;
                if (sqrDist < minSqrDistance) {
                    minSqrDistance = sqrDist;
                    closestEnemy = enemy;
                }
            }

            ThreatLevel currentThreatLevel = ThreatLevel.None;
            float distance = 0f;
            float angle = 0f;
            Vector3 direction = Vector3.zero;

            if (closestEnemy != null) {
                distance = Mathf.Sqrt(minSqrDistance);
                direction = (closestEnemy.transform.position - playerPos).normalized;
                
                // 플레이어의 전방 방향을 기준으로 상대 각도 계산
                angle = Vector3.SignedAngle(player.forward, direction, Vector3.up);

                if (distance <= _redThreshold) {
                    currentThreatLevel = ThreatLevel.Red;
                } else if (distance <= _orangeThreshold) {
                    currentThreatLevel = ThreatLevel.Orange;
                } else if (distance <= _yellowThreshold) {
                    currentThreatLevel = ThreatLevel.Yellow;
                }
            }

            // 매 프레임 값타입 구조체 이벤트를 발행 (가비지 생성 없음)
            EventBroker.Publish(new ThreatUpdateEvent {
                Level = currentThreatLevel,
                Distance = distance,
                Angle = angle,
                Direction = direction
            });
        }
    }
}
