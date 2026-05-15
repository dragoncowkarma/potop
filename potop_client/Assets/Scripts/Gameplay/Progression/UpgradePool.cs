using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Potop.Client.Gameplay.Progression
{
    /// <summary>
    /// 업그레이드 선택지 하나의 정보를 담는 구조체 (혹은 클래스)
    /// </summary>
    [System.Serializable]
    public struct UpgradeOption
    {
        public string UpgradeId;
        public string DisplayName;
        public string Description;
        // Phase 6에서 사용될 확장 인터페이스를 위한 변수
        public int RarityWeight;
    }

    /// <summary>
    /// 전체 업그레이드 목록을 관리하고 랜덤으로 추출하는 클래스
    /// </summary>
    public class UpgradePool : MonoBehaviour
    {
        [Tooltip("전체 업그레이드 옵션 풀")]
        [SerializeField] private List<UpgradeOption> _availableUpgrades = new List<UpgradeOption>();

        /// <summary>
        /// 주어진 개수만큼 중복되지 않는 랜덤 업그레이드 옵션을 추출합니다.
        /// Phase 6에서 확률에 기반한 로직이 추가될 예정입니다.
        /// </summary>
        private System.Random _random = new System.Random();

        /// <param name="count">추출할 옵션 개수 (일반적으로 3~4개)</param>
        /// <returns>선택된 업그레이드 옵션 리스트</returns>
        public List<UpgradeOption> GetRandomUpgrades(int count)
        {
            if (_availableUpgrades == null || _availableUpgrades.Count == 0)
            {
                Debug.LogWarning("[UpgradePool] 업그레이드 풀이 비어있습니다.");
                return new List<UpgradeOption>();
            }

            int extractCount = Mathf.Min(count, _availableUpgrades.Count);

            // 임시로 단순히 섞어서 앞에서부터 추출 (Phase 6 가중치 시스템 도입 전)
            var shuffled = _availableUpgrades.OrderBy(x => _random.Next()).ToList();

            return shuffled.Take(extractCount).ToList();
        }

        // 임시 테스트용 초기화 (실제로는 에디터나 외부 데이터에서 세팅됨)
        private void Awake()
        {
            if (_availableUpgrades.Count == 0)
            {
                _availableUpgrades.Add(new UpgradeOption { UpgradeId = "wp_dmg_up", DisplayName = "Damage Up", Description = "Increases weapon damage", RarityWeight = 10 });
                _availableUpgrades.Add(new UpgradeOption { UpgradeId = "wp_spd_up", DisplayName = "Fire Rate Up", Description = "Increases weapon fire rate", RarityWeight = 10 });
                _availableUpgrades.Add(new UpgradeOption { UpgradeId = "pl_hp_up", DisplayName = "Max HP Up", Description = "Increases player max HP", RarityWeight = 5 });
                _availableUpgrades.Add(new UpgradeOption { UpgradeId = "pl_spd_up", DisplayName = "Move Speed Up", Description = "Increases player movement speed", RarityWeight = 10 });
                _availableUpgrades.Add(new UpgradeOption { UpgradeId = "wp_crit_up", DisplayName = "Crit Chance Up", Description = "Increases weapon critical chance", RarityWeight = 2 });
            }
        }
    }
}
