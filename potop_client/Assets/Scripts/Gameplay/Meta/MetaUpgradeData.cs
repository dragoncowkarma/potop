using UnityEngine;

namespace Potop.Client.Gameplay.Meta {
    /// <summary>
    /// 영구 강화 항목 하나를 정의하는 ScriptableObject입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "MetaUpgradeData", menuName = "POTOP/Meta/MetaUpgradeData")]
    public class MetaUpgradeData : ScriptableObject {
        [Header("Identification")]
        [SerializeField] private string _upgradeId;
        [SerializeField] private string _displayName;
        [SerializeField] private string _description;

        [Header("Balance")]
        [Tooltip("레벨 0이 기본(미강화) 상태. 배열 인덱스 i = 레벨 i+1로 업그레이드하는 비용.")]
        [SerializeField] private int[] _costPerLevel;
        [Tooltip("레벨당 적용되는 효과 수치 배열. 인덱스 i = 레벨 i+1의 효과값.")]
        [SerializeField] private float[] _effectPerLevel;

        /// <summary>강화 항목의 고유 식별자입니다.</summary>
        public string UpgradeId => _upgradeId;

        /// <summary>UI에 표시되는 이름입니다.</summary>
        public string DisplayName => _displayName;

        /// <summary>효과 설명 문자열입니다.</summary>
        public string Description => _description;

        /// <summary>최대 레벨 (costPerLevel 배열 길이)입니다.</summary>
        public int MaxLevel => _costPerLevel != null ? _costPerLevel.Length : 0;

        /// <summary>
        /// 특정 레벨로 업그레이드하는 데 필요한 비용을 반환합니다.
        /// targetLevel은 1-based입니다 (예: 1레벨 달성 비용 = [0]).
        /// </summary>
        public int GetCost(int targetLevel) {
            int idx = targetLevel - 1;
            if (_costPerLevel == null || idx < 0 || idx >= _costPerLevel.Length) return 0;
            return _costPerLevel[idx];
        }

        /// <summary>
        /// 특정 레벨에서의 효과 수치를 반환합니다. (0 레벨 = 기본값 0)
        /// </summary>
        public float GetEffect(int level) {
            if (level <= 0 || _effectPerLevel == null || level > _effectPerLevel.Length) return 0f;
            return _effectPerLevel[level - 1];
        }
    }
}
