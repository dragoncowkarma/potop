using UnityEngine;

namespace Potop.Client.Data {
    /// <summary>
    /// 오버클럭 모드의 데이터 설정을 정의하는 ScriptableObject입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "New OverclockData", menuName = "Potop/Data/OverclockData")]
    public class OverclockData : ScriptableObject {
        [SerializeField] private float _scalingInterval = 30f;
        [SerializeField] private float _hpScalingFactor = 1.5f;
        [SerializeField] private float _speedScalingFactor = 1.5f;
        [SerializeField] private float _damageScalingFactor = 1.5f;
        [SerializeField] private float _baseHpMultiplier = 1.0f;
        [SerializeField] private float _baseSpeedMultiplier = 1.0f;
        [SerializeField] private float _baseDamageMultiplier = 1.0f;
        [SerializeField] private float _overclockSpawnInterval = 1.0f;

        public float ScalingInterval => _scalingInterval;
        public float HpScalingFactor => _hpScalingFactor;
        public float SpeedScalingFactor => _speedScalingFactor;
        public float DamageScalingFactor => _damageScalingFactor;
        public float BaseHpMultiplier => _baseHpMultiplier;
        public float BaseSpeedMultiplier => _baseSpeedMultiplier;
        public float BaseDamageMultiplier => _baseDamageMultiplier;
        public float OverclockSpawnInterval => _overclockSpawnInterval;
    }
}
