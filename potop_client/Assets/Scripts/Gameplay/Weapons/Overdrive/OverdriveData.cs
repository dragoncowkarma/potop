using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Overdrive
{
    /// <summary>
    /// 궁극 진화 무기의 스탯과 진화 조건을 저장하는 ScriptableObject입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewOverdriveData", menuName = "POTOP/Weapons/Overdrive Data")]
    public class OverdriveData : WeaponData
    {
        [Tooltip("궁극 진화에 필요한 시너지 조건")]
        public SynergyType RequiredSynergy;
    }
}
