using UnityEngine;
using Potop.Client.Core.Pooling;
using Potop.Client.Data.Items;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Items
{
    public struct ItemCollectedEvent
    {
        public ItemDropData ItemData;
        public Vector3 Position;
    }

    [RequireComponent(typeof(Collider))]
    public class ItemDrop : MonoBehaviour
    {
        [SerializeField] private ItemDropData _itemData;

        public void Initialize(ItemDropData data)
        {
            _itemData = data;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && _itemData != null)
            {
                Collect();
            }
        }

        private void Collect()
        {
            EventBroker.Publish(new ItemCollectedEvent
            {
                ItemData = _itemData,
                Position = transform.position
            });

            if (PoolManager.Instance != null)
            {
                PoolManager.Instance.Despawn(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
