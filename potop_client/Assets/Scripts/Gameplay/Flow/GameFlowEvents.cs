using System;

namespace Potop.Client.Gameplay.Flow {
    /// <summary>
    /// 게임 진행 상태를 나타내는 열거형입니다.
    /// </summary>
    public enum GameFlowState {
        Lobby,
        SelectTurret,
        InGame,
        BossBattle,
        Overclock,
        Result
    }

    /// <summary>
    /// 한 판의 게임 결과를 나타내는 데이터 전송 객체(DTO)입니다.
    /// </summary>
    public class SettlementData {
        public int Kills { get; set; }
        public int MaxWaves { get; set; }
        public int GemsEarned { get; set; }
        public float SurvivalTime { get; set; }
    }
}

namespace Potop.Client.Core.Events {
    using Potop.Client.Gameplay.Flow;

    /// <summary>
    /// 게임 진행 상태가 변경될 때 발행되는 이벤트입니다.
    /// </summary>
    public struct GameFlowStateChangedEvent {
        public GameFlowState PreviousState;
        public GameFlowState NewState;
    }
}
