using UnityEngine;

namespace Potop.Client.Data {
    /// <summary>
    /// Titan Core 보스의 고유 능력치 및 설정을 관리하는 ScriptableObject 클래스입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "New TitanCoreData", menuName = "Potop/Data/TitanCoreData")]
    public class TitanCoreData : EnemyData {
        [Header("Phase 2: Laser Settings")]
        [SerializeField] private float _laserChargeDuration = 1f;
        [SerializeField] private float _laserFireDuration = 2f;
        [SerializeField] private float _laserInterval = 3f;
        [SerializeField] private int _laserDamage = 20;
        [SerializeField] private float _laserRange = 50f;
        [SerializeField] private float _laserWidth = 1f;
        [SerializeField] private float _laserChargeWidth = 0.1f;

        [Header("Phase 3: Overclock Settings")]
        [SerializeField] private float _overclockSpeedMultiplier = 2f;
        [SerializeField] private float _bulletInterval = 3f;
        [SerializeField] private float _bulletSpeed = 8f;
        [SerializeField] private int _bulletDamage = 5;
        [SerializeField] private int _bulletCount = 8;
        [SerializeField] private GameObject _bulletPrefab;

        public float LaserChargeDuration => _laserChargeDuration;
        public float LaserFireDuration => _laserFireDuration;
        public float LaserInterval => _laserInterval;
        public int LaserDamage => _laserDamage;
        public float LaserRange => _laserRange;
        public float LaserWidth => _laserWidth;
        public float LaserChargeWidth => _laserChargeWidth;

        public float OverclockSpeedMultiplier => _overclockSpeedMultiplier;
        public float BulletInterval => _bulletInterval;
        public float BulletSpeed => _bulletSpeed;
        public int BulletDamage => _bulletDamage;
        public int BulletCount => _bulletCount;
        public GameObject BulletPrefab => _bulletPrefab;
    }
}
