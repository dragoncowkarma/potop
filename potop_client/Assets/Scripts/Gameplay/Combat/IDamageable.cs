namespace Potop.Client.Gameplay.Combat {
    /// <summary>
    /// 게임 내에서 피해를 입을 수 있는 모든 개체(적, 플레이어, 구조물 등)가 반드시 구현해야 하는 인터페이스입니다.
    /// 구체적인 클래스 타입에 의존하지 않고 다형성을 통해 일관된 피해 처리 로직을 적용하기 위해 사용합니다.
    /// </summary>
    public interface IDamageable {
        /// <summary>
        /// 외부로부터 피해가 가해졌을 때 호출되는 메서드입니다.
        /// 전달받은 피해 정보를 기반으로 체력 차감, 상태 이상 적용, 피격 효과 재생 등의 로직을 처리해야 합니다.
        /// </summary>
        /// <param name="info">피해량, 피격 위치, 공격 주체 등 상세한 피해 정보가 포함된 구조체입니다.</param>
        void TakeDamage(DamageInfo info);
    }
}
