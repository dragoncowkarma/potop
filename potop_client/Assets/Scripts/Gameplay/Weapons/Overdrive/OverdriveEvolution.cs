using System.Collections.Generic;
using UnityEngine;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Weapons;

namespace Potop.Client.Gameplay.Weapons.Overdrive
{
    [System.Serializable]
    public struct OverdriveMapping
    {
        public OverdriveData Data;
        public WeaponBase WeaponPrefab;
    }

    /// <summary>
    /// 보스 상자 획득 시 시너지 완성 상태를 감지하여 궁극 진화 무기를 발동하는 시스템입니다.
    /// WeaponTransitionHandler를 사용하여 기존 상태를 안전하게 정리하고 진화합니다.
    /// </summary>
    public class OverdriveEvolution : MonoBehaviour
    {
        [SerializeField, Tooltip("시너지 완성 상태를 확인할 매니저")]
        private MutationSynergyManager _synergyManager;

        [SerializeField, Tooltip("궁극 진화 무기 매핑 데이터 목록")]
        private List<OverdriveMapping> _overdriveMappings = new List<OverdriveMapping>();

        [SerializeField, Tooltip("무기가 장착될 위치")]
        private Transform _weaponMountPoint;

        [SerializeField, Tooltip("현재 장착된 무기")]
        private WeaponBase _currentWeapon;

        private bool _hasEvolved = false;

        private void OnEnable()
        {
            EventBroker.Subscribe<BossChestOpenedEvent>(OnBossChestOpened);
        }

        private void OnDisable()
        {
            EventBroker.Unsubscribe<BossChestOpenedEvent>(OnBossChestOpened);
        }

        private void OnBossChestOpened(BossChestOpenedEvent eventData)
        {
            if (_hasEvolved || _synergyManager == null) return;

            foreach (var mapping in _overdriveMappings)
            {
                if (mapping.Data != null && _synergyManager.HasSynergy(mapping.Data.RequiredSynergy))
                {
                    EvolveWeapon(mapping);
                    break;
                }
            }
        }

        private void EvolveWeapon(OverdriveMapping mapping)
        {
            _hasEvolved = true;

            if (mapping.WeaponPrefab != null && _weaponMountPoint != null)
            {
                // WeaponTransitionHandler를 사용하여 기존 무기 파괴, 남은 투사체 회수, 새 무기 스폰을 일괄 처리합니다.
                _currentWeapon = WeaponTransitionHandler.TransitionWeapon(_currentWeapon, mapping.WeaponPrefab, _weaponMountPoint);
                Debug.Log($"[OverdriveEvolution] 궁극 진화 발동! {mapping.Data.name}");

                // EventBroker를 통해 진화 이벤트 발행
                EventBroker.Publish(new OverdriveEvolvedEvent
                {
                    OverdriveName = mapping.Data != null ? mapping.Data.name : "Overdrive Weapon",
                    RequiredSynergy = mapping.Data != null ? mapping.Data.RequiredSynergy : SynergyType.None
                });

                // VFX 연출 훅 호출
                TriggerEvolutionVFX(mapping);
            }
            else
            {
                Debug.LogWarning("[OverdriveEvolution] 무기 프리팹 또는 장착 위치가 설정되지 않았습니다.");
            }
        }

        /// <summary>
        /// 무기 진화 시 호출되는 VFX 연출 훅입니다.
        /// 화면 흔들림 및 파티클 재생 등을 처리합니다.
        /// </summary>
        private void TriggerEvolutionVFX(OverdriveMapping mapping)
        {
            // CinemachineImpulseSource를 활성화하기 위해 CombatImpactEvent를 발행합니다.
            EventBroker.Publish(new CombatImpactEvent
            {
                Position = _weaponMountPoint != null ? _weaponMountPoint.position : Vector3.zero,
                Intensity = 1.0f, // 강렬한 화면 흔들림
                IsHeavy = true
            });

            PlayEvolutionParticles();
        }

        /// <summary>
        /// 진화 시 재생할 시각 파티클 연출용 빈 메서드 (VFX Hook)
        /// </summary>
        private void PlayEvolutionParticles()
        {
            // 실제 에셋 바인딩 생략, 연출 후킹용 로그 출력
            Debug.Log("[OverdriveEvolution VFX Hook] 궁극 진화 파티클 연출을 시작합니다.");
        }
    }

    /// <summary>
    /// 무기가 궁극 무기로 진화했을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct OverdriveEvolvedEvent
    {
        public string OverdriveName;
        public SynergyType RequiredSynergy;
    }
}
