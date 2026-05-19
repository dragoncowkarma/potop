using System.Collections.Generic;
using UnityEngine;

namespace Potop.Client.Gameplay
{
    /// <summary>
    /// 활성 모디파이어 조합을 감지하여 시너지 보너스를 적용 및 해제하는 매니저 클래스입니다.
    /// O(n^2) 이하의 복잡도로 모디파이어 변경 시에만 시너지를 재평가합니다.
    /// </summary>
    public class MutationSynergyManager : MonoBehaviour
    {
        [SerializeField, Tooltip("시너지 규칙 데이터 (ScriptableObject)")]
        private SynergyRuleData _synergyRuleData;

        // 현재 활성화된 모디파이어를 추적합니다.
        private readonly HashSet<ModifierType> _activeModifiers = new HashSet<ModifierType>();

        // 현재 활성화된 시너지를 추적합니다.
        private HashSet<SynergyType> _activeSynergies = new HashSet<SynergyType>();

        public event System.Action<SynergyType> SynergyActivated;
        public event System.Action<SynergyType> SynergyDeactivated;

        /// <summary>
        /// 새로운 모디파이어를 추가하고 시너지를 재평가합니다.
        /// </summary>
        /// <param name="modifier">추가할 모디파이어</param>
        public void AddModifier(ModifierType modifier)
        {
            if (_activeModifiers.Add(modifier))
            {
                EvaluateSynergies();
            }
        }

        /// <summary>
        /// 기존 모디파이어를 제거하고 시너지를 재평가합니다.
        /// </summary>
        /// <param name="modifier">제거할 모디파이어</param>
        public void RemoveModifier(ModifierType modifier)
        {
            if (_activeModifiers.Remove(modifier))
            {
                EvaluateSynergies();
            }
        }

        /// <summary>
        /// 특정 시너지가 활성화되어 있는지 확인합니다.
        /// </summary>
        /// <param name="synergy">확인할 시너지 타입</param>
        /// <returns>활성화 여부</returns>
        public bool HasSynergy(SynergyType synergy)
        {
            return _activeSynergies.Contains(synergy);
        }

        /// <summary>
        /// 현재 활성화된 모디파이어를 기반으로 시너지 규칙을 평가합니다.
        /// 복잡도는 O(R)입니다. (R은 전체 규칙의 개수)
        /// </summary>
        private void EvaluateSynergies()
        {
            if (_synergyRuleData == null || _synergyRuleData.Rules == null) return;

            var newActiveSynergies = new HashSet<SynergyType>();
            foreach (var rule in _synergyRuleData.Rules)
            {
                if (rule.Modifier1 != ModifierType.None && rule.Modifier2 != ModifierType.None &&
                    _activeModifiers.Contains(rule.Modifier1) && _activeModifiers.Contains(rule.Modifier2))
                {
                    newActiveSynergies.Add(rule.Synergy);
                }
            }

            var oldActiveSynergies = _activeSynergies;
            _activeSynergies = newActiveSynergies;

            // 비활성화된 시너지에 대한 이벤트 발생
            foreach (var synergy in oldActiveSynergies)
            {
                if (!_activeSynergies.Contains(synergy))
                {
                    SynergyDeactivated?.Invoke(synergy);
                    Debug.Log($"[MutationSynergyManager] 시너지 비활성화: {synergy}");
                }
            }

            // 새로 활성화된 시너지에 대한 이벤트 발생
            foreach (var synergy in _activeSynergies)
            {
                if (!oldActiveSynergies.Contains(synergy))
                {
                    SynergyActivated?.Invoke(synergy);
                    Debug.Log($"[MutationSynergyManager] 시너지 활성화: {synergy}");
                }
            }
        }
    }
}
