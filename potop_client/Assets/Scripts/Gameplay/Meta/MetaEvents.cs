namespace Potop.Client.Gameplay.Meta {
    /// <summary>
    /// 보석(Gem) 잔고가 변경되었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct GemChangedEvent {
        /// <summary>변경 후의 새 잔고입니다.</summary>
        public int NewBalance;
    }

    /// <summary>
    /// 영구 강화 항목이 구매(레벨업)되었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct MetaUpgradePurchasedEvent {
        /// <summary>강화 항목의 식별자입니다.</summary>
        public string UpgradeId;
        /// <summary>구매 후 새로운 레벨입니다.</summary>
        public int NewLevel;
    }
}
