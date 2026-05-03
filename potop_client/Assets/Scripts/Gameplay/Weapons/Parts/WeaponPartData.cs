using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Parts {
    /// <summary>
    /// 무기 파츠의 능력치 및 특수 효과를 정의하는 ScriptableObject입니다.
    /// 파츠의 종류(Body, Barrel, Magazine)에 따라 이 데이터를 참조하여 무기의 기본 능력치를 수정합니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewWeaponPartData", menuName = "POTOP/Weapons/Weapon Part Data")]
    public class WeaponPartData : ScriptableObject {
        [Header("Stat Modifiers")]
        [Tooltip("피해량 증가(예: 1.2는 20% 증가)")]
        public float DamageMultiplier = 1.0f;

        [Tooltip("연사력 증가(예: 1.1은 10% 증가)")]
        public float FireRateMultiplier = 1.0f;

        [Tooltip("투사체 속도 증가")]
        public float ProjectileSpeedMultiplier = 1.0f;

        [Tooltip("탄창 최대 용량")]
        public int MaxAmmo = 30;

        [Header("Special Effects")]
        [Tooltip("집탄률 개선도 (스프레드 감소치)")]
        public float SpreadReduction = 0.0f;

        [Tooltip("투사체 관통력")]
        public int PenetrationPower = 0;
    }
}
