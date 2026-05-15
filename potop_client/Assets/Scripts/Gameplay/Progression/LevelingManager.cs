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

        // Time.timeScale 원본 복원을 위한 저장 변수
        private float _originalTimeScale = 1f;
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
        }

        private void OnDisable()
        {
            EventBroker.Unsubscribe<EXPCollectedEvent>(OnEXPCollected);

            // 매니저가 비활성화될 때 타임스케일 복원 보장
            if (_pendingLevelUpsCount > 0)
            {
                _pendingLevelUpsCount = 0;
                Time.timeScale = _originalTimeScale;
            }
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
            if (_pendingLevelUpsCount == 0)
            {
                _originalTimeScale = Time.timeScale;
                Time.timeScale = 0f;
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
                    Time.timeScale = _originalTimeScale;
                }
            }
        }
    }
}
