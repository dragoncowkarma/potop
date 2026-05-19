using System;
using System.Collections.Generic;
using UnityEngine;

namespace Potop.Client.Gameplay
{
    /// <summary>
    /// 모디파이어의 종류를 정의합니다.
    /// </summary>
    public enum ModifierType
    {
        None = 0,
        Pierce = 1,
        Explosion = 2,
        MultiShot = 3,
        Bounce = 4,
        Scale = 5,
        Knockback = 6
    }

    /// <summary>
    /// 시너지의 종류를 정의합니다.
    /// </summary>
    public enum SynergyType
    {
        None = 0,
        PierceExplosion = 1,
        BounceHoming = 2,
        ScaleShockwave = 3
    }

    /// <summary>
    /// 두 개의 모디파이어 조합으로 발생하는 시너지 규칙을 정의합니다.
    /// </summary>
    [Serializable]
    public struct SynergyRule
    {
        [Tooltip("첫 번째 요구 모디파이어")]
        public ModifierType Modifier1;

        [Tooltip("두 번째 요구 모디파이어")]
        public ModifierType Modifier2;

        [Tooltip("활성화되는 시너지")]
        public SynergyType Synergy;
    }

    /// <summary>
    /// 시너지 규칙 데이터를 담고 있는 ScriptableObject입니다.
    /// 코드 하드코딩을 방지하고 외부(Inspector)에서 시너지 규칙을 정의할 수 있도록 합니다.
    /// </summary>
    [CreateAssetMenu(fileName = "SynergyRuleData", menuName = "POTOP/Weapons/Synergy Rule Data")]
    public class SynergyRuleData : ScriptableObject
    {
        [Tooltip("시너지 규칙 목록")]
        public List<SynergyRule> Rules = new List<SynergyRule>();
    }
}
