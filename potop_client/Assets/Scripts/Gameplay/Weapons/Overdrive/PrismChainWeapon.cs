using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Overdrive
{
    /// <summary>
    /// 프리즘 체인 무기 클래스입니다.
    /// 추적 및 전이 레이저 패턴을 사용합니다.
    /// </summary>
    public class PrismChainWeapon : WeaponBase
    {
        protected override void Start()
        {
            base.Start();
            SetFireStrategy(new PrismChainFireStrategy());
        }
    }
}
