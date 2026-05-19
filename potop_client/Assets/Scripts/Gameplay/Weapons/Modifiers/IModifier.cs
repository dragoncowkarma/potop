namespace Potop.Client.Gameplay {
    /// <summary>
    /// 투사체에 적용되는 변이(Modifier)의 인터페이스입니다.
    /// </summary>
    public interface IModifier {
        /// <summary>
        /// 투사체에 변이를 적용합니다.
        /// </summary>
        void Apply(Projectile projectile);

        /// <summary>
        /// 투사체에서 변이를 제거합니다.
        /// </summary>
        void Remove(Projectile projectile);
    }
}
