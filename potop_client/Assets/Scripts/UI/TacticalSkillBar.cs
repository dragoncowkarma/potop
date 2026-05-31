using System.Collections;
using Potop.Client.Core;
using Potop.Client.Core.Events;
using Potop.Client.Gameplay.Combat;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Potop.Client.UI {
    /// <summary>
    /// 전술 스킬 입력, 에너지 바, 스킬 쿨다운 표시를 관리하는 위젯입니다.
    /// </summary>
    public class TacticalSkillBar : MonoBehaviour {
        private VisualElement _energyBarFill;
        private VisualElement _empCooldownOverlay;
        private Label _empCooldownLabel;
        private VisualElement _orbitalCooldownOverlay;
        private Label _orbitalCooldownLabel;
        private VisualElement _shieldCooldownOverlay;
        private Label _shieldCooldownLabel;

        private InputActionReference _empAction;
        private InputActionReference _orbitalStrikeAction;
        private InputActionReference _shieldAction;

        private EMPSkill _empSkill;
        private OrbitalStrikeSkill _orbitalStrikeSkill;
        private OverloadShieldSkill _overloadShieldSkill;

        private Coroutine _empCoroutine;
        private Coroutine _orbitalCoroutine;
        private Coroutine _shieldCoroutine;

        public void Initialize(
            VisualElement energyBarFill,
            VisualElement empCooldownOverlay, Label empCooldownLabel,
            VisualElement orbitalCooldownOverlay, Label orbitalCooldownLabel,
            VisualElement shieldCooldownOverlay, Label shieldCooldownLabel,
            InputActionReference empAction, InputActionReference orbitalStrikeAction, InputActionReference shieldAction,
            EMPSkill empSkill, OrbitalStrikeSkill orbitalStrikeSkill, OverloadShieldSkill overloadShieldSkill
        ) {
            _energyBarFill = energyBarFill;
            _empCooldownOverlay = empCooldownOverlay;
            _empCooldownLabel = empCooldownLabel;
            _orbitalCooldownOverlay = orbitalCooldownOverlay;
            _orbitalCooldownLabel = orbitalCooldownLabel;
            _shieldCooldownOverlay = shieldCooldownOverlay;
            _shieldCooldownLabel = shieldCooldownLabel;

            _empAction = empAction;
            _orbitalStrikeAction = orbitalStrikeAction;
            _shieldAction = shieldAction;

            _empSkill = empSkill;
            _orbitalStrikeSkill = orbitalStrikeSkill;
            _overloadShieldSkill = overloadShieldSkill;

            EnsureReferences();

            if (EnergyManager.Instance != null) {
                UpdateEnergy(EnergyManager.Instance.CurrentEnergy, EnergyManager.MAX_ENERGY);
            } else {
                UpdateEnergy(0, 1000);
            }

            CheckInitialCooldowns();
        }

        private void OnEnable() {
            EventBroker.Subscribe<EnergyChangedEvent>(OnEnergyChanged);
            EventBroker.Subscribe<SkillCooldownEvent>(OnSkillCooldown);

            if (_empAction != null && _empAction.action != null) {
                _empAction.action.Enable();
                _empAction.action.started += OnEmpTriggered;
            }
            if (_orbitalStrikeAction != null && _orbitalStrikeAction.action != null) {
                _orbitalStrikeAction.action.Enable();
                _orbitalStrikeAction.action.started += OnOrbitalStrikeTriggered;
            }
            if (_shieldAction != null && _shieldAction.action != null) {
                _shieldAction.action.Enable();
                _shieldAction.action.started += OnShieldTriggered;
            }
        }

        private void OnDisable() {
            EventBroker.Unsubscribe<EnergyChangedEvent>(OnEnergyChanged);
            EventBroker.Unsubscribe<SkillCooldownEvent>(OnSkillCooldown);

            if (_empAction != null && _empAction.action != null) {
                _empAction.action.started -= OnEmpTriggered;
                _empAction.action.Disable();
            }
            if (_orbitalStrikeAction != null && _orbitalStrikeAction.action != null) {
                _orbitalStrikeAction.action.started -= OnOrbitalStrikeTriggered;
                _orbitalStrikeAction.action.Disable();
            }
            if (_shieldAction != null && _shieldAction.action != null) {
                _shieldAction.action.started -= OnShieldTriggered;
                _shieldAction.action.Disable();
            }

            StopAllSkillCoroutines();
        }

        private void OnEnergyChanged(EnergyChangedEvent evt) {
            UpdateEnergy(evt.CurrentEnergy, evt.MaxEnergy);
        }

        private void OnSkillCooldown(SkillCooldownEvent evt) {
            EnsureReferences();
            if (evt.SkillName == nameof(EMPSkill)) {
                if (_empCoroutine != null) StopCoroutine(_empCoroutine);
                _empCoroutine = StartCoroutine(CooldownRoutine(_empSkill, _empCooldownOverlay, _empCooldownLabel));
            } else if (evt.SkillName == nameof(OrbitalStrikeSkill)) {
                if (_orbitalCoroutine != null) StopCoroutine(_orbitalCoroutine);
                _orbitalCoroutine = StartCoroutine(CooldownRoutine(_orbitalStrikeSkill, _orbitalCooldownOverlay, _orbitalCooldownLabel));
            } else if (evt.SkillName == nameof(OverloadShieldSkill)) {
                if (_shieldCoroutine != null) StopCoroutine(_shieldCoroutine);
                _shieldCoroutine = StartCoroutine(CooldownRoutine(_overloadShieldSkill, _shieldCooldownOverlay, _shieldCooldownLabel));
            }
        }

        private void OnEmpTriggered(InputAction.CallbackContext context) {
            EnsureReferences();
            if (_empSkill != null) {
                _empSkill.TryExecute();
            }
        }

        private void OnOrbitalStrikeTriggered(InputAction.CallbackContext context) {
            EnsureReferences();
            if (_orbitalStrikeSkill != null) {
                _orbitalStrikeSkill.TryExecute();
            }
        }

        private void OnShieldTriggered(InputAction.CallbackContext context) {
            EnsureReferences();
            if (_overloadShieldSkill != null) {
                _overloadShieldSkill.TryExecute();
            }
        }

        private void EnsureReferences() {
            if (_empSkill != null && _orbitalStrikeSkill != null && _overloadShieldSkill != null) {
                return;
            }

            if (GameManager.Instance != null && GameManager.Instance.PlayerTransform != null) {
                var playerGo = GameManager.Instance.PlayerTransform.gameObject;
                if (_empSkill == null) _empSkill = playerGo.GetComponent<EMPSkill>();
                if (_orbitalStrikeSkill == null) _orbitalStrikeSkill = playerGo.GetComponent<OrbitalStrikeSkill>();
                if (_overloadShieldSkill == null) _overloadShieldSkill = playerGo.GetComponent<OverloadShieldSkill>();
            }

            if (_empSkill == null) _empSkill = GetComponent<EMPSkill>();
            if (_orbitalStrikeSkill == null) _orbitalStrikeSkill = GetComponent<OrbitalStrikeSkill>();
            if (_overloadShieldSkill == null) _overloadShieldSkill = GetComponent<OverloadShieldSkill>();
        }

        private void CheckInitialCooldowns() {
            EnsureReferences();
            if (_empSkill != null && _empSkill.GetRemainingCooldown() > 0f) {
                if (_empCoroutine != null) StopCoroutine(_empCoroutine);
                _empCoroutine = StartCoroutine(CooldownRoutine(_empSkill, _empCooldownOverlay, _empCooldownLabel));
            }
            if (_orbitalStrikeSkill != null && _orbitalStrikeSkill.GetRemainingCooldown() > 0f) {
                if (_orbitalCoroutine != null) StopCoroutine(_orbitalCoroutine);
                _orbitalCoroutine = StartCoroutine(CooldownRoutine(_orbitalStrikeSkill, _orbitalCooldownOverlay, _orbitalCooldownLabel));
            }
            if (_overloadShieldSkill != null && _overloadShieldSkill.GetRemainingCooldown() > 0f) {
                if (_shieldCoroutine != null) StopCoroutine(_shieldCoroutine);
                _shieldCoroutine = StartCoroutine(CooldownRoutine(_overloadShieldSkill, _shieldCooldownOverlay, _shieldCooldownLabel));
            }
        }

        private void UpdateEnergy(int current, int max) {
            if (_energyBarFill != null) {
                float pct = (float)current / max * 100f;
                _energyBarFill.style.width = new Length(Mathf.Clamp(pct, 0f, 100f), LengthUnit.Percent);
            }
        }

        private IEnumerator CooldownRoutine(TacticalSkillBase skill, VisualElement overlay, Label label) {
            if (skill == null || overlay == null || label == null) {
                yield break;
            }

            overlay.style.display = DisplayStyle.Flex;
            label.style.display = DisplayStyle.Flex;

            float cooldown = skill.Cooldown;
            int lastTenths = -1;

            while (true) {
                float remaining = skill.GetRemainingCooldown();
                if (remaining <= 0f) {
                    break;
                }

                if (cooldown > 0f) {
                    float pct = (remaining / cooldown) * 100f;
                    overlay.style.height = new Length(pct, LengthUnit.Percent);
                }

                int currentTenths = Mathf.RoundToInt(remaining * 10f);
                if (currentTenths != lastTenths) {
                    lastTenths = currentTenths;
                    label.text = string.Format("{0:F1}s", remaining);
                }

                yield return new WaitForSeconds(0.1f);
            }

            overlay.style.display = DisplayStyle.None;
            label.style.display = DisplayStyle.None;
        }

        private void StopAllSkillCoroutines() {
            if (_empCoroutine != null) {
                StopCoroutine(_empCoroutine);
                _empCoroutine = null;
            }
            if (_orbitalCoroutine != null) {
                StopCoroutine(_orbitalCoroutine);
                _orbitalCoroutine = null;
            }
            if (_shieldCoroutine != null) {
                StopCoroutine(_shieldCoroutine);
                _shieldCoroutine = null;
            }
        }
    }
}
