namespace Potop.Client.Gameplay.Weapons.Strategies {
    /// <summary>
    /// 무기의 발사 방식을 정의하는 전략 인터페이스입니다.
    /// 전략 패턴을 사용하여 런타임에 발사 로직을 교체할 수 있도록 합니다.
    /// </summary>
    public interface IFireStrategy {
        /// <summary>
        /// 주어진 무기의 정보를 바탕으로 실제 발사 로직을 수행합니다.
        /// </summary>
        /// <param name="weapon">발사 로직을 실행할 무기 인스턴스</param>
        void ExecuteFire(WeaponBase weapon);
    }
}
