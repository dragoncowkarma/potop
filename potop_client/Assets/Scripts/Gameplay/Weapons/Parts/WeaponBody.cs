using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Parts {
    /// <summary>
    /// 무기의 본체를 담당하는 파츠입니다.
    /// 주로 피해량과 연사력을 수정하는 데 사용됩니다.
    /// </summary>
    public class WeaponBody : MonoBehaviour {
        [SerializeField, Tooltip("파츠의 능력치 데이터")]
        private WeaponPartData _partData;

        /// <summary>
        /// 기본 피해량을 파츠 데이터에 기반하여 수정합니다.
        /// </summary>
        public float ModifyDamage(float baseDamage) {
            if (_partData == null) return baseDamage;
            return baseDamage * _partData.DamageMultiplier;
        }

        /// <summary>
        /// 기본 연사력을 파츠 데이터에 기반하여 수정합니다.
        /// </summary>
        public float ModifyFireRate(float baseFireRate) {
            if (_partData == null) return baseFireRate;
            return baseFireRate * _partData.FireRateMultiplier;
        }
    }
}
