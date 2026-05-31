using System;
using UnityEngine;

namespace Potop.Client.Core {
    /// <summary>
    /// Core 어셈블리와 UI 어셈블리 간의 상호 참조를 끊기 위한 브릿지 클래스입니다.
    /// </summary>
    public static class CoreUIBridge {
        public static Action<GameObject, int, int, int, float, Action> SetupResultUI { get; set; }
    }
}
