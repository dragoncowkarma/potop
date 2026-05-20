using System.Collections.Generic;
using UnityEngine;
using Potop.Client.Core.Pooling;
using Potop.Client.Gameplay;

namespace Potop.Client.Gameplay.Weapons {
    /// <summary>
    /// 무기 전환 및 진화 시 기존 무기와 연관된 상태를 정리하고
    /// 새로운 무기를 안전하게 스폰하는 유틸리티 클래스입니다.
    /// </summary>
    public static class WeaponTransitionHandler {
        /// <summary>
        /// 기존 무기를 파괴하고, 현재 활성화된 모든 투사체를 회수한 뒤,
        /// 새로운 무기 프리팹을 지정된 마운트 포인트에 생성합니다.
        /// </summary>
        /// <param name="currentWeapon">현재 장착된 무기 인스턴스 (파괴됨)</param>
        /// <param name="newWeaponPrefab">새로 생성할 무기 프리팹</param>
        /// <param name="mountPoint">무기가 생성될 부모 Transform 위치</param>
        /// <returns>새로 생성된 무기 인스턴스</returns>
        public static WeaponBase TransitionWeapon(WeaponBase currentWeapon, WeaponBase newWeaponPrefab, Transform mountPoint) {
            // 1. 기존 무기 파괴 (이벤트 구독 해제 및 정리 로직은 무기의 OnDisable/OnDestroy에서 처리됨을 가정)
            if (currentWeapon != null) {
                Object.Destroy(currentWeapon.gameObject);
            }

            // 2. 현재 활성화된 모든 투사체 회수 (안전한 화면 정리)
            // 성능 이슈 방지를 위해 씬 검색 함수 대신 추적된 해시셋의 복사본을 순회합니다.
            List<Projectile> projectilesToDespawn = new List<Projectile>(Projectile.ActiveProjectiles);
            for (int i = 0; i < projectilesToDespawn.Count; i++) {
                if (projectilesToDespawn[i] != null) {
                    PoolManager.Instance.Despawn(projectilesToDespawn[i].gameObject);
                }
            }

            // 3. 새 무기 생성 및 반환
            if (newWeaponPrefab != null && mountPoint != null) {
                return Object.Instantiate(newWeaponPrefab, mountPoint.position, mountPoint.rotation, mountPoint);
            }

            return null;
        }
    }
}
