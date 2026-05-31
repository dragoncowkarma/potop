using System;

namespace Potop.Client.Core {
    /// <summary>
    /// Core 어셈블리와 Gameplay.Meta 어셈블리 간의 상호 참조를 끊기 위한 브릿지 클래스입니다.
    /// </summary>
    public static class CoreMetaBridge {
        public static Func<bool> IsMetaUpgradeManagerActive { get; set; }
        public static Func<bool> IsGemWalletActive { get; set; }
        public static Func<int> GetBonusHp { get; set; }
    }
}
