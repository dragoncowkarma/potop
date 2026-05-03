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
}
