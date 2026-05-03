using System;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Weapons {
    /// <summary>
    /// 무기 시스템의 공통 동작을 정의하는 인터페이스입니다.
    /// 발사, 재장전, 상태 업데이트를 포함합니다.
    /// </summary>
    public interface IWeapon {
        /// <summary>
        /// 무기를 발사합니다.
        /// </summary>
        void Fire();

        /// <summary>
        /// 무기를 재장전합니다.
        /// </summary>
        void Reload();

        /// <summary>
        /// 무기의 상태(예: 쿨다운)를 갱신합니다.
        /// </summary>
        void UpdateState(float deltaTime);
    }

    /// <summary>
    /// 무기가 발사되었을 때 발행되는 이벤트입니다.
    /// EventBroker를 통해 구독자에게 전달됩니다.
    /// </summary>
    public struct WeaponFiredEvent {
        public IWeapon Weapon;
    }

    /// <summary>
    /// 무기가 재장전되었을 때 발행되는 이벤트입니다.
    /// EventBroker를 통해 구독자에게 전달됩니다.
    /// </summary>
    public struct WeaponReloadedEvent {
        public IWeapon Weapon;
    }
}
