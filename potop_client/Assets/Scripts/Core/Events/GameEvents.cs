using System;

namespace Potop.Client.Core.Events {
    /// <summary>
    /// 점수가 변경되었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct ScoreChangedEvent {
        /// <summary>
        /// 현재 게임 점수입니다.
        /// </summary>
        public int CurrentScore;
    }

    /// <summary>
    /// 플레이어의 체력이 변경되었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct HealthChangedEvent {
        /// <summary>
        /// 현재 체력입니다.
        /// </summary>
        public int CurrentHealth;

        /// <summary>
        /// 최대 체력입니다.
        /// </summary>
        public int MaxHealth;
    }

    /// <summary>
    /// 적이 처치되었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct EnemyDiedEvent {
        /// <summary>
        /// 적이 제공하는 점수(피버 게이지 증가량)입니다.
        /// </summary>
        public int ScoreValue;
    }

    /// <summary>
    /// 피버 모드의 활성화 상태가 변경되었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct FeverStateChangedEvent {
        /// <summary>
        /// 피버 모드 활성화 여부입니다.
        /// </summary>
        public bool IsFeverActive;
    }

    /// <summary>
    /// 피버 게이지의 진행 상태가 변경되었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct FeverProgressChangedEvent {
        /// <summary>
        /// 피버 게이지의 진행도 (0.0 ~ 1.0)
        /// </summary>
        public float Progress;
    }
    /// <summary>
    /// 플레이어가 피해를 입었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct PlayerTakeDamageEvent {
        /// <summary>
        /// 입은 피해량입니다.
        /// </summary>
        public int Damage;
    }

    /// <summary>
    /// 전투 중 타격이 발생했을 때 발생하는 이벤트입니다. 카메라 흔들림 등의 피드백에 사용됩니다.
    /// </summary>
    public struct CombatImpactEvent {
        /// <summary>
        /// 타격이 발생한 위치입니다.
        /// </summary>
        public UnityEngine.Vector3 Position;

        /// <summary>
        /// 타격의 강도입니다. (0.0 ~ 1.0 권장)
        /// </summary>
        public float Intensity;

        /// <summary>
        /// 강력한 충격(AoE 등)인지 여부입니다.
        /// </summary>
        public bool IsHeavy;
    }
}

