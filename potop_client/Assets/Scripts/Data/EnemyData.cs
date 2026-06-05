using UnityEngine;

namespace Potop.Client.Data {
    /// <summary>
    /// 적의 데이터를 정의하는 ScriptableObject입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "New EnemyData", menuName = "Potop/Data/EnemyData")]
    public class EnemyData : ScriptableObject {
        [SerializeField] private string _enemyName;
        [SerializeField] private int _maxHealth;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _scoreValue;
        [SerializeField] private int _energyReward = 10;

        /// <summary>
        /// 적의 이름입니다.
        /// </summary>
        public string EnemyName => _enemyName;

        /// <summary>
        /// 적의 최대 체력입니다.
        /// </summary>
        public int MaxHealth => _maxHealth;

        /// <summary>
        /// 적의 이동 속도입니다.
        /// </summary>
        public float MoveSpeed => _moveSpeed;

        /// <summary>
        /// 적 처치 시 획득하는 점수입니다.
        /// </summary>
        public int ScoreValue => _scoreValue;

        /// <summary>
        /// 적 처치 시 획득하는 에너지입니다.
        /// </summary>
        public int EnergyReward => _energyReward;

        [SerializeField] private int _baseDamage;
        [SerializeField] private float _spawnWeight = 1f;

        /// <summary>
        /// 적의 기본 공격 피해량입니다.
        /// </summary>
        public int BaseDamage => _baseDamage;

        /// <summary>
        /// 적의 웨이브 스폰 가중치입니다.
        /// </summary>
        public float SpawnWeight => _spawnWeight;

        public void InitializeFromBalance(string name, int maxHealth, float moveSpeed, int scoreValue, int energyReward, int baseDamage, float spawnWeight) {
            _enemyName = name;
            _maxHealth = maxHealth;
            _moveSpeed = moveSpeed;
            _scoreValue = scoreValue;
            _energyReward = energyReward;
            _baseDamage = baseDamage;
            _spawnWeight = spawnWeight;
        }
    }
}
