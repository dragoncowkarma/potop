using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Overdrive
{
    /// <summary>
    /// 오비탈 스트라이크 무기 클래스입니다.
    /// 광범위 궤도 폭격 패턴을 사용합니다.
    /// </summary>
    public class OrbitalStrikeWeapon : WeaponBase
    {
        protected override void Start()
        {
            base.Start();
            SetFireStrategy(new OrbitalStrikeFireStrategy());
        }
    }
}
