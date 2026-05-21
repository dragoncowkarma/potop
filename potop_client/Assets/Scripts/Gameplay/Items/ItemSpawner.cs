using System.Collections.Generic;
using UnityEngine;
using Potop.Client.Core.Events;
using Potop.Client.Core.Pooling;
using Potop.Client.Data.Items;
using Potop.Client.Core;
using Potop.Client.Gameplay.Wave;
using Potop.Client.Gameplay.Combat;

namespace Potop.Client.Gameplay.Items
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private List<ItemDropData> _dropTable;
        [SerializeField] private LayerMask _enemyLayerMask;
        [SerializeField] private LayerMask _gemLayerMask;
        [SerializeField, Tooltip("Base drop chance multiplier. Increases with waves.")] private float _baseDropMultiplier = 1.0f;

        private int _currentWave = 0;
        private static readonly Collider[] _overlapResults = new Collider[100];

        private void OnEnable()
        {
            EventBroker.Subscribe<EnemyKilledEvent>(OnEnemyKilled);
            EventBroker.Subscribe<WaveStartedEvent>(OnWaveStarted);
            EventBroker.Subscribe<ItemCollectedEvent>(OnItemCollected);
        }

        private void OnDisable()
        {
            EventBroker.Unsubscribe<EnemyKilledEvent>(OnEnemyKilled);
            EventBroker.Unsubscribe<WaveStartedEvent>(OnWaveStarted);
            EventBroker.Unsubscribe<ItemCollectedEvent>(OnItemCollected);
        }

        private void OnWaveStarted(WaveStartedEvent e)
        {
            _currentWave = e.WaveIndex;
        }

        private void OnEnemyKilled(EnemyKilledEvent e)
        {
            if (_dropTable == null || _dropTable.Count == 0) return;

            // 보장 드랍 로직 (특수 적 처치 시)
            if (e.ExpValue >= 200)
            {
                ItemDropData guaranteedItem = _dropTable[Random.Range(0, _dropTable.Count)];
                SpawnItem(guaranteedItem, e.Position);
                return;
            }

            // 웨이브 진행도에 따른 추가 드랍 확률 (예: 1웨이브당 5% 증가)
            float waveMultiplier = _baseDropMultiplier + (_currentWave * 0.05f);

            // 웨이브별 확률 기반 스폰
            float totalProbability = 0f;
            foreach (var item in _dropTable)
            {
                totalProbability += item.DropProbability * waveMultiplier;
            }

            float randomValue = Random.Range(0f, 1f);
            float cumulativeProbability = 0f;
            foreach (var itemData in _dropTable)
            {
                // Multiply actual drop probability by wave modifier
                cumulativeProbability += itemData.DropProbability * waveMultiplier;
                if (randomValue <= cumulativeProbability)
                {
                    SpawnItem(itemData, e.Position);
                    break; // Spawns 1 item max
                }
            }
        }

        private void SpawnItem(ItemDropData itemData, Vector3 position)
        {
            if (itemData.Prefab != null && PoolManager.Instance != null)
            {
                GameObject instance = PoolManager.Instance.Spawn(itemData.Prefab, position, Quaternion.identity);
                ItemDrop itemDrop = instance.GetComponent<ItemDrop>();
                if (itemDrop != null)
                {
                    itemDrop.Initialize(itemData);
                }
            }
        }

        private void OnItemCollected(ItemCollectedEvent e)
        {
            switch (e.ItemData.Type)
            {
                case ItemDropType.RepairKit:
                    ApplyRepairKit(e.ItemData);
                    break;
                case ItemDropType.Magnet:
                    ApplyMagnet(e.ItemData);
                    break;
                case ItemDropType.SmartBomb:
                    ApplySmartBomb(e.ItemData, e.Position);
                    break;
            }
        }

        private void ApplyRepairKit(ItemDropData data)
        {
            if (GameManager.Instance != null)
            {
                // Heal the player securely
                GameManager.Instance.TakeDamage(-data.HealAmount); // Assuming GameManager handles negative damage gracefully for healing
            }
        }

        private void ApplyMagnet(ItemDropData data)
        {
            if (GameManager.Instance != null && GameManager.Instance.PlayerTransform != null)
            {
                // Always start the coroutine to keep the magnet active for the duration
                StartCoroutine(MagnetCoroutine(data.MagnetRadius, data.MagnetDuration));
            }
        }

        private System.Collections.IEnumerator MagnetCoroutine(float radius, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                if (GameManager.Instance != null && GameManager.Instance.PlayerTransform != null)
                {
                    // Use LayerMask to filter ONLY Gems, preventing full array saturation
                    int count = Physics.OverlapSphereNonAlloc(GameManager.Instance.PlayerTransform.position, radius, _overlapResults, _gemLayerMask);
                    for (int i = 0; i < count; i++)
                    {
                        EXPGem gem = _overlapResults[i].GetComponent<EXPGem>();
                        if (gem != null)
                        {
                            // Move towards player fast
                            gem.transform.position = Vector3.MoveTowards(gem.transform.position, GameManager.Instance.PlayerTransform.position, 30f * Time.deltaTime);
                        }
                    }
                }
                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        private void ApplySmartBomb(ItemDropData data, Vector3 position)
        {
            int count = Physics.OverlapSphereNonAlloc(position, data.BombRadius, _overlapResults, _enemyLayerMask);
            for (int i = 0; i < count; i++)
            {
                EnemyBase enemy = _overlapResults[i].GetComponentInParent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(new DamageInfo { Amount = data.BombDamage });
                }
            }
        }
    }
}
