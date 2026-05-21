using UnityEngine;

namespace Potop.Client.Data.Items
{
    public enum ItemDropType
    {
        RepairKit,
        Magnet,
        SmartBomb
    }

    [CreateAssetMenu(fileName = "New ItemDropData", menuName = "Potop/Data/Items/ItemDropData")]
    public class ItemDropData : ScriptableObject
    {
        [Header("Base Settings")]
        [SerializeField] private ItemDropType _type;
        [SerializeField] private GameObject _prefab;
        [SerializeField, Range(0f, 1f)] private float _dropProbability = 0.05f;

        [Header("Repair Kit Settings")]
        [SerializeField, Min(1)] private int _healAmount = 20;

        [Header("Magnet Settings")]
        [SerializeField, Min(0.1f)] private float _magnetDuration = 5f;
        [SerializeField, Min(1f)] private float _magnetRadius = 10f;

        [Header("Smart Bomb Settings")]
        [SerializeField, Min(1)] private int _bombDamage = 9999;
        [SerializeField, Min(1f)] private float _bombRadius = 20f;

        public ItemDropType Type => _type;
        public GameObject Prefab => _prefab;
        public float DropProbability => _dropProbability;

        public int HealAmount => _healAmount;

        public float MagnetDuration => _magnetDuration;
        public float MagnetRadius => _magnetRadius;

        public int BombDamage => _bombDamage;
        public float BombRadius => _bombRadius;
    }
}
