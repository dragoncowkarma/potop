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
    }
}
