using UnityEngine;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Weapons.Parts;
using Potop.Client.Gameplay.Weapons.Strategies;

namespace Potop.Client.Gameplay.Weapons {
    /// <summary>
    /// 모든 무기의 기반이 되는 추상 클래스입니다.
    /// 데이터(ScriptableObject)와 파츠 시스템을 결합하여 무기의 최종 동작을 결정합니다.
    /// </summary>
    public abstract class WeaponBase : MonoBehaviour, IWeapon {
        [SerializeField, Tooltip("무기의 기본 스탯 데이터")]
        protected WeaponData _weaponData;

        [SerializeField, Tooltip("투사체가 발사될 위치")]
        protected Transform _firePoint;

        // 전략 패턴: 런타임에 발사 방식을 교체하기 위함
        protected IFireStrategy _fireStrategy;

        // 파츠 시스템: 무기의 성능을 동적으로 변경하기 위함
        protected WeaponBody _weaponBody;
        protected WeaponBarrel _weaponBarrel;
        protected WeaponMagazine _weaponMagazine;

        protected float _lastFireTime;
        protected int _currentAmmo;

        protected virtual void Start() {
            // 초기화 시 장탄수를 꽉 채웁니다.
            _currentAmmo = _weaponMagazine != null ? _weaponMagazine.GetMaxAmmo() : 30;
        }

        /// <summary>
        /// 무기의 발사 방식을 설정합니다. 런타임 교체가 가능합니다.
        /// </summary>
        public void SetFireStrategy(IFireStrategy strategy) {
            _fireStrategy = strategy;
        }

        /// <summary>
        /// 무기에 파츠를 장착합니다.
        /// </summary>
        public void EquipParts(WeaponBody body, WeaponBarrel barrel, WeaponMagazine magazine) {
            _weaponBody = body;
            _weaponBarrel = barrel;
            _weaponMagazine = magazine;
        }

        /// <summary>
        /// 무기를 발사합니다. 발사 쿨다운 및 잔탄을 확인하고, 발사 전략에 위임합니다.
        /// </summary>
        public virtual void Fire() {
            if (!CanFire()) return;

            _lastFireTime = Time.time;
            _currentAmmo--;

            // 발사 로직을 전략 객체에 위임하여 결합도를 낮춥니다.
            _fireStrategy?.ExecuteFire(this);

            EventBroker.Publish(new WeaponFiredEvent { Weapon = this });
        }

        /// <summary>
        /// 무기를 재장전합니다. 잔탄을 최대치로 회복합니다.
        /// </summary>
        public virtual void Reload() {
            // 장착된 탄창 파츠가 있다면 해당 파츠의 탄창 용량을 사용, 없으면 기본값 사용
            _currentAmmo = _weaponMagazine != null ? _weaponMagazine.GetMaxAmmo() : 30;

            EventBroker.Publish(new WeaponReloadedEvent { Weapon = this });
        }

        /// <summary>
        /// 무기의 상태(예: 쿨다운)를 갱신합니다. 매 프레임 호출될 수 있습니다.
        /// </summary>
        public virtual void UpdateState(float deltaTime) {
            // 추후 재장전 시간 계산이나 과열 상태 업데이트 등 공통 상태 갱신 로직 구현
        }

        /// <summary>
        /// 현재 계산된 발사 속도를 반환합니다. 파츠의 영향을 받습니다.
        /// </summary>
        public virtual float GetModifiedFireRate() {
            if (_weaponData == null) return 0f;
            return _weaponBody != null ? _weaponBody.ModifyFireRate(_weaponData.BaseFireRate) : _weaponData.BaseFireRate;
        }

        /// <summary>
        /// 현재 발사가 가능한 상태인지 확인합니다.
        /// </summary>
        protected virtual bool CanFire() {
            if (_weaponData == null || _currentAmmo <= 0) return false;

            float currentFireRate = GetModifiedFireRate();
            if (currentFireRate <= 0) return false;

            float fireInterval = 1f / currentFireRate;
            return Time.time >= _lastFireTime + fireInterval;
        }

        /// <summary>
        /// 현재 계산된 피해량을 반환합니다. 파츠의 영향을 받습니다.
        /// </summary>
        public float GetCalculatedDamage() {
            if (_weaponData == null) return 0f;
            return _weaponBody != null ? _weaponBody.ModifyDamage(_weaponData.BaseDamage) : _weaponData.BaseDamage;
        }

        /// <summary>
        /// 현재 계산된 투사체 속도를 반환합니다. 파츠의 영향을 받습니다.
        /// </summary>
        public float GetCalculatedProjectileSpeed() {
            if (_weaponData == null) return 0f;
            return _weaponBarrel != null ? _weaponBarrel.ModifyProjectileSpeed(_weaponData.BaseProjectileSpeed) : _weaponData.BaseProjectileSpeed;
        }

        /// <summary>
        /// 투사체가 발사될 위치입니다.
        /// </summary>
        public Transform FirePoint => _firePoint;

        /// <summary>
        /// 발사할 투사체 프리팹입니다.
        /// </summary>
        public GameObject ProjectilePrefab => _weaponData != null ? _weaponData.ProjectilePrefab : null;
    }
}
