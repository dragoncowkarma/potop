using System;

namespace Potop.Client.Gameplay.Flow {
    /// <summary>
    /// 게임 진행 상태를 나타내는 열거형입니다. Core와 Gameplay 모두에서 공유됩니다.
    /// </summary>
    public enum GameFlowState {
        Lobby,
        SelectTurret,
        InGame,
        BossBattle,
        Overclock,
        Result
    }
}

namespace Potop.Client.Core {
    using Potop.Client.Gameplay.Flow;

    /// <summary>
    /// Core 어셈블리와 Gameplay.Flow 어셈블리 간의 상호 참조를 끊기 위한 브릿지 클래스입니다.
    /// </summary>
    public static class CoreFlowBridge {
        public static Func<GameFlowState> GetCurrentState { get; set; }
        public static Action<GameFlowState> TransitionTo { get; set; }
    }
}
