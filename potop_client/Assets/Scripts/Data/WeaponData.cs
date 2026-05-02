using UnityEngine;

namespace Potop.Client.Data {
    /// <summary>
    /// 무기의 데이터를 정의하는 ScriptableObject입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "New WeaponData", menuName = "Potop/Data/WeaponData")]
    public class WeaponData : ScriptableObject {
        [SerializeField] private float _fireRate;
        [SerializeField] private int _damage;
        [SerializeField] private GameObject _projectilePrefab;

        /// <summary>
        /// 무기의 발사 주기입니다.
        /// </summary>
        public float FireRate => _fireRate;

        /// <summary>
        /// 무기의 피해량입니다.
        /// </summary>
        public int Damage => _damage;

        /// <summary>
        /// 무기에서 발사할 발사체 프리팹입니다.
        /// </summary>
        public GameObject ProjectilePrefab => _projectilePrefab;
    }
}
