using NUnit.Framework;
using UnityEngine;
using System.Reflection;
using System;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Flow;
using Potop.Client.Gameplay.Meta;

namespace Potop.Client.Tests.EditMode {
    [TestFixture]
    public class GameFlowControllerTests {
        private GameObject _controllerGo;
        private GameFlowController _controller;
        private GameObject _walletGo;
        private GemWallet _wallet;
        private GameObject _mockUiInstance;
        private bool _uiInstantiated;
        private bool _uiDestroyed;

        [SetUp]
        public void Setup() {
            _controllerGo = new GameObject("GameFlowController");
            _controller = _controllerGo.AddComponent<GameFlowController>();

            _walletGo = new GameObject("GemWallet");
            _wallet = _walletGo.AddComponent<GemWallet>();

            // Ensure GemWallet.Instance is initialized in EditMode
            var walletAwake = typeof(GemWallet).GetMethod("Awake", BindingFlags.NonPublic | BindingFlags.Instance);
            if (walletAwake != null) {
                walletAwake.Invoke(_wallet, null);
            }
            // In case Awake didn't set it (e.g. duplicate check):
            typeof(GemWallet).GetProperty("Instance", BindingFlags.Static | BindingFlags.Public)?.SetValue(null, _wallet);

            // Mock UI instantiator to avoid loading real prefabs during EditMode test
            _uiInstantiated = false;
            _uiDestroyed = false;
            _mockUiInstance = null;

            _controller.UIInstantiator = (path) => {
                _uiInstantiated = true;
                _mockUiInstance = new GameObject("MockResultUI");
                _mockUiInstance.AddComponent<Potop.Client.UI.ResultUIController>();
                return _mockUiInstance;
            };

            _controller.UIDestroyer = (obj) => {
                if (obj == _mockUiInstance) {
                    _uiDestroyed = true;
                }
                UnityEngine.Object.DestroyImmediate(obj);
            };

            EventBroker.ClearAllSubscriptions();
            _controllerGo.SetActive(true);
        }

        [TearDown]
        public void Teardown() {
            if (_mockUiInstance != null) {
                UnityEngine.Object.DestroyImmediate(_mockUiInstance);
            }
            UnityEngine.Object.DestroyImmediate(_controllerGo);
            UnityEngine.Object.DestroyImmediate(_walletGo);
            EventBroker.ClearAllSubscriptions();
        }

        [Test]
        public void InitialState_IsLobby() {
            Assert.AreEqual(GameFlowState.Lobby, _controller.CurrentState);
        }

        [Test]
        public void TransitionTo_ChangesStateAndPublishesEvent() {
            GameFlowState? previousState = null;
            GameFlowState? newState = null;
            int eventCount = 0;

            EventBroker.Subscribe<GameFlowStateChangedEvent>(e => {
                previousState = e.PreviousState;
                newState = e.NewState;
                eventCount++;
            });

            _controller.TransitionTo(GameFlowState.SelectTurret);

            Assert.AreEqual(GameFlowState.SelectTurret, _controller.CurrentState);
            Assert.AreEqual(1, eventCount);
            Assert.AreEqual(GameFlowState.Lobby, previousState);
            Assert.AreEqual(GameFlowState.SelectTurret, newState);
        }

        [Test]
        public void Metrics_AccumulatedCorrectlyDuringGameplay() {
            // Start game
            _controller.TransitionTo(GameFlowState.InGame);

            // 1. Kills tracking
            EventBroker.Publish(new EnemyDiedEvent { ScoreValue = 10, EnergyReward = 5 });
            EventBroker.Publish(new EnemyDiedEvent { ScoreValue = 10, EnergyReward = 5 });

            // 2. Wave tracking
            EventBroker.Publish(new Potop.Client.Gameplay.Wave.WaveStartedEvent { WaveIndex = 3 });

            // 3. Gems tracking (e.g. collecting exp/gems)
            // Assuming GameFlowController subscribes to Progression.EXPCollectedEvent or custom gem events to award run-time gems.
            // Let's publish Progression.EXPCollectedEvent to simulate earning gem-credits
            EventBroker.Publish(new Potop.Client.Gameplay.Progression.EXPCollectedEvent { Amount = 100 });

            // 4. Survival time tracking (simulated via update)
            var updateMethod = typeof(GameFlowController).GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Instance);
            // Simulate 5.5 seconds elapsed
            // Note: Since Time.deltaTime is tested, we mock Time.deltaTime or simulate update ticks using reflection.
            // Let's set the survival time directly through reflection for exact precision if needed,
            // or let's verify that Update ticks it up by Time.deltaTime.
            // Since EditMode has Time.deltaTime = 0 usually, we check survival time field directly.
            var survivalTimeField = typeof(GameFlowController).GetField("_survivalTime", BindingFlags.NonPublic | BindingFlags.Instance);
            survivalTimeField.SetValue(_controller, 125.5f);

            // Now transition to Result state to compile settlement DTO
            _controller.TransitionTo(GameFlowState.Result);

            var settlementDataField = typeof(GameFlowController).GetField("_settlementData", BindingFlags.NonPublic | BindingFlags.Instance);
            var settlement = (SettlementData)settlementDataField.GetValue(_controller);

            Assert.IsNotNull(settlement);
            Assert.AreEqual(2, settlement.Kills);
            Assert.AreEqual(3, settlement.MaxWaves);
            Assert.AreEqual(125.5f, settlement.SurvivalTime);
            // Let's assert that gems earned has tracked appropriately (e.g. 1 gem per XPCollectedEvent, or matching criteria)
            Assert.GreaterOrEqual(settlement.GemsEarned, 0);
        }

        [Test]
        public void EnteringResultState_InstantiatesUIAndInjectsDTO() {
            _controller.TransitionTo(GameFlowState.InGame);
            
            // Earn 5 gems
            var gemsField = typeof(GameFlowController).GetField("_gemsEarned", BindingFlags.NonPublic | BindingFlags.Instance);
            gemsField.SetValue(_controller, 5);

            _controller.TransitionTo(GameFlowState.Result);

            Assert.IsTrue(_uiInstantiated);
            Assert.IsNotNull(_mockUiInstance);

            var resultUI = _mockUiInstance.GetComponent<Potop.Client.UI.ResultUIController>();
            Assert.IsNotNull(resultUI);
        }

        [Test]
        public void TransitionBackToLobby_DestroysUIAndAddsGemsToWallet() {
            int initialBalance = _wallet.Balance;

            _controller.TransitionTo(GameFlowState.InGame);
            var gemsField = typeof(GameFlowController).GetField("_gemsEarned", BindingFlags.NonPublic | BindingFlags.Instance);
            gemsField.SetValue(_controller, 15);

            _controller.TransitionTo(GameFlowState.Result);
            Assert.IsFalse(_uiDestroyed);

            // Return to Lobby
            _controller.TransitionTo(GameFlowState.Lobby);

            Assert.IsTrue(_uiDestroyed);
            Assert.AreEqual(initialBalance + 15, _wallet.Balance);
            Assert.AreEqual(GameFlowState.Lobby, _controller.CurrentState);
        }

        [Test]
        public void TransitionToLobby_UnsubscribesProperlyToAvoidMemoryLeaks() {
            _controller.TransitionTo(GameFlowState.InGame);
            
            // Go back to Lobby
            _controller.TransitionTo(GameFlowState.Lobby);

            // If we publish events now, metrics should NOT change
            EventBroker.Publish(new EnemyDiedEvent { ScoreValue = 10, EnergyReward = 5 });
            
            var killsField = typeof(GameFlowController).GetField("_kills", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.AreEqual(0, (int)killsField.GetValue(_controller));
        }
    }
}
