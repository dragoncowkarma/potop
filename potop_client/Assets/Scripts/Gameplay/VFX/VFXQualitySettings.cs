using UnityEngine;

namespace Potop.Client.Gameplay.VFX {
    /// <summary>
    /// VFX 품질 수준을 정의하는 열거형입니다.
    /// </summary>
    public enum VFXQuality {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// 게임 내 VFX 품질을 전역적으로 관리하는 클래스입니다.
    /// </summary>
    public static class VFXQualitySettings {
        private static VFXQuality _currentQuality = VFXQuality.High;

        /// <summary>
        /// 현재 적용된 VFX 품질 수준을 가져오거나 설정합니다.
        /// </summary>
        public static VFXQuality CurrentQuality {
            get => _currentQuality;
            set {
                if (_currentQuality != value) {
                    _currentQuality = value;
                    // 필요한 경우 품질 변경 이벤트를 호출할 수 있습니다.
                }
            }
        }
    }
}
