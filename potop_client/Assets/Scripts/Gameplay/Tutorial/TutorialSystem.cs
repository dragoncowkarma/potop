using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Potop.Client.Core.Events;
using Potop.Client.Core;
using Potop.Client.Core.Pooling;
using Potop.Client.Gameplay.Weapons;

namespace Potop.Client.Gameplay.Tutorial {
    public enum TutorialStep {
        None,
        Look,
        Shoot,
        Complete
    }

    public struct TutorialStepChangedEvent {
        public TutorialStep PreviousStep;
        public TutorialStep NewStep;
    }

    public class TutorialSystem : MonoBehaviour {
        [Header("UI Toolkit")]
        [SerializeField] private UIDocument _uiDocument;

        [Header("Input Controls")]
        [SerializeField] private InputActionReference _lookAction;
        [SerializeField] private InputActionReference _attackAction;

        [Header("Spawn Settings")]
        [SerializeField] private GameObject _trainingEnemyPrefab;
        [SerializeField] private Vector3 _spawnPosition = new Vector3(0f, 1f, 10f);

        [Header("Tutorial Balance")]
        [SerializeField] private float _lookRequiredThreshold = 500f;

        private TutorialStep _currentStep = TutorialStep.None;
        public TutorialStep CurrentStep => _currentStep;

        private float _accumulatedLook = 0f;
        private GameObject _spawnedEnemy;

        private Label _instructionLabel;
        private VisualElement _progressBarFill;

        private void OnEnable() {
            EventBroker.Subscribe<EnemyDiedEvent>(OnEnemyDied);
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<EnemyDiedEvent>(OnEnemyDied);
            Time.timeScale = 1.0f;
        }

        private void Start() {
            InitializeUI();
            TransitionToStep(TutorialStep.Look);
        }

        private void InitializeUI() {
            if (_uiDocument != null && _uiDocument.rootVisualElement != null) {
                var root = _uiDocument.rootVisualElement;
                _instructionLabel = root.Q<Label>("tutorial-instruction");
                _progressBarFill = root.Q<VisualElement>("progress-bar-fill");
            }
            UpdateUI();
        }

        private void Update() {
            if (_currentStep == TutorialStep.Look) {
                float lookDelta = 0f;
                if (_lookAction != null && _lookAction.action != null) {
                    Vector2 lookVal = _lookAction.action.ReadValue<Vector2>();
                    lookDelta = lookVal.magnitude;
                }
                AddLookDelta(lookDelta);
            }
        }

        public void AddLookDelta(float delta) {
            if (_currentStep != TutorialStep.Look) {
                return;
            }

            _accumulatedLook += delta;
            UpdateUI();

            if (_accumulatedLook >= _lookRequiredThreshold) {
                TransitionToStep(TutorialStep.Shoot);
            }
        }

        public void TransitionToStep(TutorialStep newStep) {
            if (_currentStep == newStep) {
                return;
            }

            TutorialStep prevStep = _currentStep;
            _currentStep = newStep;

            switch (_currentStep) {
                case TutorialStep.Look:
                    Time.timeScale = 0f;
                    if (_attackAction != null && _attackAction.action != null) {
                        _attackAction.action.Disable();
                    }
                    break;

                case TutorialStep.Shoot:
                    Time.timeScale = 1f;
                    if (_attackAction != null && _attackAction.action != null) {
                        _attackAction.action.Enable();
                    }
                    SpawnTrainingEnemy();
                    break;

                case TutorialStep.Complete:
                    Time.timeScale = 1f;
                    if (_spawnedEnemy != null) {
                        var enemyBase = _spawnedEnemy.GetComponent<EnemyBase>();
                        if (enemyBase != null) {
                            enemyBase.Despawn();
                        } else {
                            Destroy(_spawnedEnemy);
                        }
                    }
                    break;
            }

            UpdateUI();

            EventBroker.Publish(new TutorialStepChangedEvent {
                PreviousStep = prevStep,
                NewStep = _currentStep
            });
        }

        private void SpawnTrainingEnemy() {
            if (_trainingEnemyPrefab == null) {
                Debug.LogWarning("TutorialSystem: Training Enemy Prefab is null!");
                return;
            }

            if (PoolManager.Instance != null) {
                _spawnedEnemy = PoolManager.Instance.Spawn(_trainingEnemyPrefab, _spawnPosition, Quaternion.identity);
            } else {
                _spawnedEnemy = Instantiate(_trainingEnemyPrefab, _spawnPosition, Quaternion.identity);
            }

            if (_spawnedEnemy != null) {
                var enemyBase = _spawnedEnemy.GetComponent<EnemyBase>();
                if (enemyBase != null) {
                    var health = enemyBase.GetComponent<Combat.Health>();
                    if (health != null) {
                        health.InitializeHealth(1);
                    }
                }
            }
        }

        private void OnEnemyDied(EnemyDiedEvent e) {
            if (_currentStep == TutorialStep.Shoot) {
                TransitionToStep(TutorialStep.Complete);
            }
        }

        private void UpdateUI() {
            if (_instructionLabel == null) {
                return;
            }

            switch (_currentStep) {
                case TutorialStep.Look:
                    _instructionLabel.text = "Look Tutorial: Move the mouse to look around.";
                    if (_progressBarFill != null) {
                        float pct = Mathf.Clamp01(_accumulatedLook / _lookRequiredThreshold) * 100f;
                        _progressBarFill.style.width = new Length(pct, LengthUnit.Percent);
                    }
                    break;

                case TutorialStep.Shoot:
                    _instructionLabel.text = "Shoot Tutorial: Press left click to shoot the training enemy.";
                    if (_progressBarFill != null) {
                        _progressBarFill.style.width = new Length(0f, LengthUnit.Percent);
                    }
                    break;

                case TutorialStep.Complete:
                    _instructionLabel.text = "Tutorial Complete! Loading Main Game...";
                    if (_progressBarFill != null) {
                        _progressBarFill.style.width = new Length(100f, LengthUnit.Percent);
                    }
                    break;
            }
        }
    }
}
