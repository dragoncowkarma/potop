using NUnit.Framework;
using UnityEngine;
using Potop.Client.Core;
using Potop.Client.Core.Events;

namespace Potop.Client.Core.Tests {
    public class GameManagerTests {
        private GameManager _gameManager;
        private GameObject _gameObject;

        [SetUp]
        public void SetUp() {
            _gameObject = new GameObject("GameManager");
            _gameManager = _gameObject.AddComponent<GameManager>();

            // private Awake() 메서드를 리플렉션으로 호출하여 싱글톤 인스턴스를 설정합니다.
            var awakeMethod = typeof(GameManager).GetMethod("Awake", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            awakeMethod?.Invoke(_gameManager, null);

            _gameManager.StartGame();
        }

        [TearDown]
        public void TearDown() {
            if (_gameObject != null) {
                Object.DestroyImmediate(_gameObject);
            }

            // 싱글톤 인스턴스 정적 필드를 명시적으로 초기화하여 테스트 간 간섭을 방지합니다.
            var instanceField = typeof(GameManager).GetProperty("Instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            instanceField?.SetValue(null, null);
        }

        [Test]
        public void TakeDamage_ReducesHealthCorrectly() {
            // Arrange
            int initialHealth = _gameManager.Health;
            int damageAmount = 30;

            // Act
            _gameManager.TakeDamage(damageAmount);

            // Assert
            Assert.AreEqual(initialHealth - damageAmount, _gameManager.Health);
        }

        [Test]
        public void TakeDamage_ClampsHealthAtZero() {
            // Act
            _gameManager.TakeDamage(_gameManager.MaxHealth + 50);

            // Assert
            Assert.AreEqual(0, _gameManager.Health);
        }

        [Test]
        public void TakeDamage_PublishesHealthChangedEvent() {
            // Arrange
            HealthChangedEvent receivedEvent = default;
            bool eventPublished = false;
            System.Action<HealthChangedEvent> handler = e => {
                receivedEvent = e;
                eventPublished = true;
            };
            EventBroker.Subscribe(handler);

            try {
                // Act
                _gameManager.TakeDamage(20);

                // Assert
                Assert.IsTrue(eventPublished);
                Assert.AreEqual(_gameManager.Health, receivedEvent.CurrentHealth);
                Assert.AreEqual(_gameManager.MaxHealth, receivedEvent.MaxHealth);
            } finally {
                // Clean up subscription
                EventBroker.Unsubscribe(handler);
            }
        }

        [Test]
        public void TakeDamage_WhenHealthReachesZero_TriggersGameOver() {
            // Act
            _gameManager.TakeDamage(_gameManager.MaxHealth);

            // Assert
            Assert.IsTrue(_gameManager.IsGameOver);
            Assert.AreEqual(GameState.GameOver, _gameManager.CurrentState);
        }

        [Test]
        public void TakeDamage_AfterGameOver_DoesNotReduceHealthFurther() {
            // Arrange
            _gameManager.TakeDamage(_gameManager.MaxHealth); // Set to Game Over
            int healthAfterGameOver = _gameManager.Health;

            // Act
            _gameManager.TakeDamage(10);

            // Assert
            Assert.AreEqual(healthAfterGameOver, _gameManager.Health);
        }
    }
}
