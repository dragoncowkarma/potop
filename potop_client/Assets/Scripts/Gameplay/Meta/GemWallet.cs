using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Meta {
    /// <summary>
    /// 플레이어의 보석(Gem) 잔고를 관리하는 싱글톤 클래스입니다.
    /// PlayerPrefs를 통해 잔고를 임시 저장합니다.
    /// </summary>
    public class GemWallet : MonoBehaviour {
        private const string PREFS_KEY = "gem_wallet_balance";

        public static GemWallet Instance { get; private set; }

        /// <summary>현재 보석 잔고입니다.</summary>
        public int Balance { get; private set; }

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Balance = PlayerPrefs.GetInt(PREFS_KEY, 0);
        }

        /// <summary>
        /// 보석을 획득하여 잔고에 추가하고 이벤트를 발행합니다.
        /// </summary>
        public void Earn(int amount) {
            if (amount <= 0) return;
            Balance += amount;
            Save();
            EventBroker.Publish(new GemChangedEvent { NewBalance = Balance });
        }

        /// <summary>
        /// 보석을 소모합니다. 잔고가 부족하면 false를 반환합니다.
        /// </summary>
        public bool TrySpend(int amount) {
            if (amount <= 0 || Balance < amount) return false;
            Balance -= amount;
            Save();
            EventBroker.Publish(new GemChangedEvent { NewBalance = Balance });
            return true;
        }

        private void Save() {
            PlayerPrefs.SetInt(PREFS_KEY, Balance);
            PlayerPrefs.Save();
        }
    }
}
