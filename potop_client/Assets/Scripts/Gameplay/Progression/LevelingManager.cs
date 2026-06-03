using System.Collections.Generic;
using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Progression
{
    /// <summary>
    /// 경험치(EXP)를 수집했을 때 발생하는 이벤트입니다.
    /// (GameEvents.cs의 수정 권한이 제한된 상황이므로 로컬에서 정의)
    /// </summary>
    public struct EXPCollectedEvent
    {
        public int Amount;
    }

    /// <summary>
    /// 레벨업이 발생했을 때 호출되는 이벤트입니다.
    /// (GameEvents.cs의 수정 권한이 제한된 상황이므로 로컬에서 정의)
    /// </summary>
    public struct LevelUpEvent
    {
        public int NewLevel;
        public List<UpgradeOption> UpgradeOptions;
    }

    /// <summary>
    /// 업그레이드가 선택되었을 때 발생하는 이벤트입니다.
    /// (GameEvents.cs의 수정 권한이 제한된 상황이므로 로컬에서 정의)
    /// </summary>
    public struct UpgradeSelectedEvent
    {
        public string SelectedId;
    }

    /// <summary>
    /// Lv.1~5 및 비선택 레벨 패시브 자동 적용 시 발생하는 이벤트입니다.
    /// </summary>
    public struct PassiveUpgradeAppliedEvent
    {
        public string UpgradeName;
    }

    /// <summary>
    /// 플레이어의 경험치 누적 및 레벨업 판정을 관리하는 매니저입니다.
    /// </summary>
    [RequireComponent(typeof(UpgradePool))]
    public class LevelingManager : MonoBehaviour
    {
        [Tooltip("레벨업 요구량 데이터")]
        [SerializeField] private LevelingData _levelingData;

        [Tooltip("업그레이드 선택지 추출 개수")]
        [Min(1)]
        [SerializeField] private int _optionsCount = 3;

        private UpgradePool _upgradePool;
        private int _currentLevel = 1;
        private int _currentXp = 0;

        private int _pendingLevelUpsCount = 0;

        public int CurrentLevel => _currentLevel;
        public int CurrentXp => _currentXp;

        private void Awake()
        {
            _upgradePool = GetComponent<UpgradePool>();
            if (_levelingData == null)
            {
#if UNITY_EDITOR
                Debug.LogWarning("[LevelingManager] LevelingData가 할당되지 않았습니다. 기본 동작을 보장할 수 없습니다.");
#endif
            }
        }

        private void OnEnable()
        {
            EventBroker.Subscribe<EXPCollectedEvent>(OnEXPCollected);
            EventBroker.Subscribe<UpgradeSelectedEvent>(OnUpgradeSelected);
        }

        private void OnDisable()
        {
            EventBroker.Unsubscribe<EXPCollectedEvent>(OnEXPCollected);
            EventBroker.Unsubscribe<UpgradeSelectedEvent>(OnUpgradeSelected);

            // 매니저가 비활성화될 때 타임스케일 복원 보장
            if (_pendingLevelUpsCount > 0)
            {
                _pendingLevelUpsCount = 0;
                Potop.Client.Core.TimeController.ClearSlowMotion();
            }
        }

        private void OnUpgradeSelected(UpgradeSelectedEvent evt)
        {
            ResolveLevelUp();
        }

        /// <summary>
        /// 경험치 획득 이벤트를 처리하고 레벨업 여부를 판정합니다.
        /// </summary>
        /// <param name="evt">경험치 획득 이벤트 데이터</param>
        private void OnEXPCollected(EXPCollectedEvent evt)
        {
            if (evt.Amount <= 0) return;

            _currentXp += evt.Amount;
            CheckLevelUp();
        }

        /// <summary>
        /// 현재 경험치가 요구량을 충족하는지 확인하고, 충족 시 레벨업을 진행합니다.
        /// </summary>
        private void CheckLevelUp()
        {
            if (_levelingData == null) return;

            int requiredXp = _levelingData.GetRequiredXpForNextLevel(_currentLevel);

            while (_currentXp >= requiredXp)
            {
                _currentXp -= requiredXp;
                _currentLevel++;

                TriggerLevelUp();

                requiredXp = _levelingData.GetRequiredXpForNextLevel(_currentLevel);
            }
        }

        /// <summary>
        /// 레벨업 처리 로직 (이벤트 발행, 시간 정지 등)을 수행합니다.
        /// </summary>
        private void TriggerLevelUp()
        {
            bool isSelection = _currentLevel >= 6 && (_currentLevel - 6) % 3 == 0;

            if (isSelection)
            {
                if (_pendingLevelUpsCount == 0)
                {
                    Potop.Client.Core.TimeController.TriggerSlowMotion(5f, 0.1f);
                }

                _pendingLevelUpsCount++;

                List<UpgradeOption> options = _upgradePool.GetRandomUpgrades(_optionsCount);

                LevelUpEvent levelUpEvent = new LevelUpEvent
                {
                    NewLevel = _currentLevel,
                    UpgradeOptions = options
                };

                EventBroker.Publish(levelUpEvent);

#if UNITY_EDITOR
                Debug.Log($"[LevelingManager] Level Up! Current Level: {_currentLevel}, Pending Options: {options.Count}");
#endif
            }
            else
            {
                // Auto-apply passive upgrade for non-selection levels
                List<UpgradeOption> options = _upgradePool.GetRandomUpgrades(1);
                if (options != null && options.Count > 0)
                {
                    UpgradeOption option = options[0];
                    var synergyManager = FindFirstObjectByType<MutationSynergyManager>();
                    if (synergyManager != null)
                    {
                        ModifierType modifier = GetModifierFromOption(option);
                        if (modifier != ModifierType.None)
                        {
                            synergyManager.AddModifier(modifier);
                        }
                    }

                    EventBroker.Publish(new PassiveUpgradeAppliedEvent { UpgradeName = option.DisplayName });
                }
            }
        }

        private ModifierType GetModifierFromOption(UpgradeOption option)
        {
            if (option.AssociatedModifier != ModifierType.None)
            {
                return option.AssociatedModifier;
            }

            string id = option.UpgradeId.ToLower();
            if (id.Contains("pierce")) return ModifierType.Pierce;
            if (id.Contains("explosion") || id.Contains("explode")) return ModifierType.Explosion;
            if (id.Contains("multi") || id.Contains("shot")) return ModifierType.MultiShot;
            if (id.Contains("bounce")) return ModifierType.Bounce;
            if (id.Contains("scale") || id.Contains("size")) return ModifierType.Scale;
            if (id.Contains("knockback") || id.Contains("push")) return ModifierType.Knockback;

            return ModifierType.None;
        }

        /// <summary>
        /// 업그레이드 선택이 완료되었을 때 호출하여 게임 상태(시간)를 복원합니다.
        /// 여러 번의 레벨업이 발생했을 경우 모든 처리가 완료되어야 타임스케일이 복원됩니다.
        /// </summary>
        public void ResolveLevelUp()
        {
            if (_pendingLevelUpsCount > 0)
            {
                _pendingLevelUpsCount--;

                if (_pendingLevelUpsCount == 0)
                {
                    Potop.Client.Core.TimeController.ClearSlowMotion();
                }
            }
        }
    }
}
