using System.Collections.Generic;
using UnityEngine;
using Potop.Client.Core.Events;

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

            if (_currentWeapon != null)
            {
                Destroy(_currentWeapon.gameObject);
            }

            if (mapping.WeaponPrefab != null && _weaponMountPoint != null)
            {
                _currentWeapon = Instantiate(mapping.WeaponPrefab, _weaponMountPoint.position, _weaponMountPoint.rotation, _weaponMountPoint);
                Debug.Log($"[OverdriveEvolution] 궁극 진화 발동! {mapping.Data.name}");
            }
            else
            {
                Debug.LogWarning("[OverdriveEvolution] 무기 프리팹 또는 장착 위치가 설정되지 않았습니다.");
            }
        }
    }
}
