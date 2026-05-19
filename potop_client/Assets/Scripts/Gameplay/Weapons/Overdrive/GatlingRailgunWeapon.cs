using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Overdrive
{
    /// <summary>
    /// 개틀링 레일건 무기 클래스입니다.
    /// 초고속 무한 관통 패턴을 사용합니다.
    /// </summary>
    public class GatlingRailgunWeapon : WeaponBase
    {
        protected override void Start()
        {
            base.Start();
            SetFireStrategy(new GatlingRailgunFireStrategy());
        }
    }
}
