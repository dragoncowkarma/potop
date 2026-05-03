using UnityEngine;

namespace Potop.Client.Gameplay.Weapons.Parts {
    /// <summary>
    /// 무기의 탄창을 담당하는 파츠입니다.
    /// 최대 장탄수에 영향을 줍니다.
    /// </summary>
    public class WeaponMagazine : MonoBehaviour {
        [SerializeField, Tooltip("파츠의 능력치 데이터")]
        private WeaponPartData _partData;

        /// <summary>
        /// 탄창의 최대 용량을 반환합니다.
        /// </summary>
        public int GetMaxAmmo() {
            if (_partData == null) return 30; // 기본 장탄수 fallback
            return _partData.MaxAmmo;
        }
    }
}
