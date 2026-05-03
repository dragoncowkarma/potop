using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Parts {
    /// <summary>
    /// 무기의 총열을 담당하는 파츠입니다.
    /// 주로 투사체 속도, 스프레드 감소 및 관통력 부여에 영향을 줍니다.
    /// </summary>
    public class WeaponBarrel : MonoBehaviour {
        [SerializeField, Tooltip("파츠의 능력치 데이터")]
        private WeaponPartData _partData;

        /// <summary>
        /// 기본 투사체 속도를 파츠 데이터에 기반하여 수정합니다.
        /// </summary>
        public float ModifyProjectileSpeed(float baseSpeed) {
            if (_partData == null) return baseSpeed;
            return baseSpeed * _partData.ProjectileSpeedMultiplier;
        }

        /// <summary>
        /// 스프레드 감소 효과를 반환합니다.
        /// </summary>
        public float GetSpreadReduction() {
            if (_partData == null) return 0f;
            return _partData.SpreadReduction;
        }

        /// <summary>
        /// 관통력을 반환합니다.
        /// </summary>
        public int GetPenetrationPower() {
            if (_partData == null) return 0;
            return _partData.PenetrationPower;
        }
    }
}
