using UnityEngine;

namespace Potop.Client.Gameplay.Weapons {
    /// <summary>
    /// 무기의 기본 스탯을 저장하는 ScriptableObject입니다.
    /// 데이터 기반으로 무기를 설정하기 위해 사용됩니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewWeaponData", menuName = "POTOP/Weapons/Weapon Data")]
    public class WeaponData : ScriptableObject {
        [Tooltip("기본 피해량")]
        public float BaseDamage = 10f;

        [Tooltip("초당 발사 횟수")]
        public float BaseFireRate = 1f;

        [Tooltip("투사체 이동 속도")]
        public float BaseProjectileSpeed = 20f;

        [SerializeField, Tooltip("발사할 투사체 프리팹")]
        private GameObject _projectilePrefab;

        /// <summary>
        /// 발사할 투사체 프리팹입니다.
        /// </summary>
        public GameObject ProjectilePrefab => _projectilePrefab;
    }
}
