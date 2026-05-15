using UnityEngine;
using Potop.Client.Gameplay.Weapons.Strategies;

namespace Potop.Client.Gameplay {
    /// <summary>
    /// 직선형 레일건 포탑입니다.
    /// 단일 대상을 향해 높은 탄속으로 직선 투사체를 발사합니다.
    /// </summary>
    public class GuardianWeapon : Weapons.WeaponBase {
        // 발사 방식은 직선 발사(StraightFireStrategy)를 사용합니다.
        // 스탯 수치는 ScriptableObject(WeaponData)를 통해 주입됩니다.

        protected override void Start() {
            base.Start();
            // 기본 발사 전략을 설정합니다. (직선 발사)
            SetFireStrategy(new StraightFireStrategy());
        }
    }
}
