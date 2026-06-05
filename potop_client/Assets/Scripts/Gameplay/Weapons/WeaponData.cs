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

        [Tooltip("산탄 분산 각도")]
        public float SpreadAngle = 10f;

        [Tooltip("산탄 투사체 개수")]
        public int SpreadProjectileCount = 3;

        [Tooltip("포물선 발사 시 위쪽으로 기울일 각도")]
        public float LaunchAngle = 45f;

        [Tooltip("범위 공격 반경 (AoE)")]
        public float AoERadius = 3f;

        [Tooltip("관통 횟수 (0이면 관통 불가)")]
        public int BasePierce = 0;

        [Tooltip("넉백 수치")]
        public float KnockbackForce = 0f;

        public void InitializeFromBalance(
            float baseDamage,
            float baseFireRate,
            float baseProjectileSpeed,
            float spreadAngle,
            int spreadProjectileCount,
            float launchAngle,
            float aoERadius,
            int basePierce,
            float knockbackForce) {
            BaseDamage = baseDamage;
            BaseFireRate = baseFireRate;
            BaseProjectileSpeed = baseProjectileSpeed;
            SpreadAngle = spreadAngle;
            SpreadProjectileCount = spreadProjectileCount;
            LaunchAngle = launchAngle;
            AoERadius = aoERadius;
            BasePierce = basePierce;
            KnockbackForce = knockbackForce;
        }
    }
}
