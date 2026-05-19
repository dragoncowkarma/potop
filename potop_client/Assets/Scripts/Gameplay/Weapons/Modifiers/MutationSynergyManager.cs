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
        private readonly HashSet<SynergyType> _activeSynergies = new HashSet<SynergyType>();

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
            if (_synergyRuleData == null || _synergyRuleData.Rules == null)
            {
                return;
            }

            _activeSynergies.Clear();

            // 설정된 모든 규칙을 순회하며 조건을 만족하는지 확인합니다.
            foreach (var rule in _synergyRuleData.Rules)
            {
                if (rule.Modifier1 == ModifierType.None || rule.Modifier2 == ModifierType.None)
                {
                    continue;
                }

                if (_activeModifiers.Contains(rule.Modifier1) && _activeModifiers.Contains(rule.Modifier2))
                {
                    _activeSynergies.Add(rule.Synergy);
                }
            }
        }

        /// <summary>
        /// 관통+폭발 시너지: 관통 시 소형 폭발을 트리거합니다.
        /// </summary>
        public void OnPierceExplosion()
        {
            if (HasSynergy(SynergyType.PierceExplosion))
            {
                // 관통 시 소형 폭발 로직 트리거
                Debug.Log("[MutationSynergyManager] 관통 시 소형 폭발이 트리거되었습니다.");
            }
        }

        /// <summary>
        /// 다연발+도탄 시너지: 도탄 시 가장 가까운 적을 자동 추적합니다.
        /// </summary>
        public void OnBounceHoming()
        {
            if (HasSynergy(SynergyType.BounceHoming))
            {
                // 도탄 시 가장 가까운 적 자동 추적 로직 트리거
                Debug.Log("[MutationSynergyManager] 도탄 시 가장 가까운 적을 자동 추적합니다.");
            }
        }

        /// <summary>
        /// 거대화+넉백 시너지: 투사체 충격파 범위를 확대합니다.
        /// </summary>
        public void OnScaleShockwave()
        {
            if (HasSynergy(SynergyType.ScaleShockwave))
            {
                // 투사체 충격파 범위 확대 로직 트리거
                Debug.Log("[MutationSynergyManager] 투사체 충격파 범위가 확대되었습니다.");
            }
        }
    }
}
