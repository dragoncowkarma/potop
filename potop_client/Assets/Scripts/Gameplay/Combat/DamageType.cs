namespace Potop.Client.Gameplay.Combat {
    /// <summary>
    /// 전투 시스템에서 피해의 종류를 분류하여, 대상의 방어 속성이나 상태 이상 유발 여부를 결정하기 위해 사용합니다.
    /// </summary>
    public enum DamageType {
        /// <summary>
        /// 추가적인 상태 이상이나 속성 효과가 없는 기본적인 피해를 처리하기 위한 타입입니다.
        /// </summary>
        Normal,

        /// <summary>
        /// 대상에게 연소 상태 이상을 부여하거나 화염 약점을 가진 적에게 추가 피해를 입히기 위한 타입입니다.
        /// </summary>
        Fire,

        /// <summary>
        /// 대상을 일시적으로 마비시키거나 기계류 적에게 더 큰 피해를 주기 위한 타입입니다.
        /// </summary>
        Electric,

        /// <summary>
        /// 넓은 범위의 다수 적에게 피해를 주거나 장갑을 파괴하기 위한 타입입니다.
        /// </summary>
        Explosive
    }

    /// <summary>
    /// 피해에 추가적인 속성이나 특성을 부여하기 위한 비트 플래그입니다.
    /// </summary>
    [System.Flags]
    public enum DamageTags {
        None = 0,
        Critical = 1 << 0,
        Penetrating = 1 << 1,
        Indirect = 1 << 2,
        FeverBonus = 1 << 3
    }
}
