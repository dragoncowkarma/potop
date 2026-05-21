using UnityEngine;
using UnityEngine.InputSystem;
using Potop.Client.Core.Events;

namespace Potop.Client.Gameplay.Combat
{
    /// <summary>
    /// 오버차지 시스템의 상태를 정의합니다.
    /// </summary>
    public enum OverchargeState
    {
        Idle,
        Active,
        Overheat
    }

    /// <summary>
    /// 오버차지 상태가 변경되었을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct OverchargeStateChangedEvent
    {
        public OverchargeState State;
    }

    /// <summary>
    /// 오버차지 시스템의 설정을 관리하는 ScriptableObject입니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewOverchargeData", menuName = "POTOP/Combat/Overcharge Data")]
    public class OverchargeData : ScriptableObject
    {
        [Tooltip("최대 게이지 값")]
        public float MaxGauge = 100f;

        [Tooltip("버튼 유지 시 초당 충전량")]
        public float ChargeRate = 20f;

        [Tooltip("버튼 해제 시 초당 자연 감소량")]
        public float DecayRate = 15f;

        [Tooltip("과열 상태 지속 시간 (초)")]
        public float OverheatDuration = 3f;

        [Tooltip("활성화 시 공격 속도 배율")]
        public float AttackSpeedMultiplier = 2f;
    }

    /// <summary>
    /// 플레이어의 오버차지 게이지와 상태를 관리하는 컨트롤러입니다.
    /// </summary>
    public class OverchargeController : MonoBehaviour
    {
        [SerializeField, Tooltip("오버차지 설정 데이터")]
        private OverchargeData _overchargeData;

        [SerializeField, Tooltip("오버차지 입력을 처리할 액션 레퍼런스")]
        private InputActionReference _overchargeAction;

        private OverchargeState _currentState = OverchargeState.Idle;
        private float _currentGauge = 0f;
        private bool _isButtonHeld = false;
        private float _overheatTimer = 0f;

        public OverchargeState CurrentState => _currentState;
        public float CurrentGauge => _currentGauge;

        private void OnEnable()
        {
            if (_overchargeAction != null && _overchargeAction.action != null)
            {
                _overchargeAction.action.started += OnOverchargeStarted;
                _overchargeAction.action.canceled += OnOverchargeCanceled;
                _overchargeAction.action.Enable();
            }
        }

        private void OnDisable()
        {
            if (_overchargeAction != null && _overchargeAction.action != null)
            {
                _overchargeAction.action.started -= OnOverchargeStarted;
                _overchargeAction.action.canceled -= OnOverchargeCanceled;
                _overchargeAction.action.Disable();
            }

            // 상태 초기화
            _isButtonHeld = false;
            ChangeState(OverchargeState.Idle);
        }

        private void Update()
        {
            if (_overchargeData == null) return;

            switch (_currentState)
            {
                case OverchargeState.Idle:
                    UpdateIdleState();
                    break;
                case OverchargeState.Active:
                    UpdateActiveState();
                    break;
                case OverchargeState.Overheat:
                    UpdateOverheatState();
                    break;
            }
        }

        private void OnOverchargeStarted(InputAction.CallbackContext context)
        {
            _isButtonHeld = true;
        }

        private void OnOverchargeCanceled(InputAction.CallbackContext context)
        {
            _isButtonHeld = false;
        }

        private void UpdateIdleState()
        {
            if (_isButtonHeld)
            {
                ChangeState(OverchargeState.Active);
                return;
            }

            if (_currentGauge > 0f)
            {
                _currentGauge -= _overchargeData.DecayRate * Time.deltaTime;
                _currentGauge = Mathf.Max(0f, _currentGauge);
            }
        }

        private void UpdateActiveState()
        {
            if (!_isButtonHeld)
            {
                ChangeState(OverchargeState.Idle);
                return;
            }

            _currentGauge += _overchargeData.ChargeRate * Time.deltaTime;

            if (_currentGauge >= _overchargeData.MaxGauge)
            {
                _currentGauge = _overchargeData.MaxGauge;
                ChangeState(OverchargeState.Overheat);
            }
        }

        private void UpdateOverheatState()
        {
            _overheatTimer -= Time.deltaTime;

            if (_overheatTimer <= 0f)
            {
                _currentGauge = 0f;
                ChangeState(_isButtonHeld ? OverchargeState.Active : OverchargeState.Idle);
            }
        }

        private void ChangeState(OverchargeState newState)
        {
            if (_currentState == newState) return;

            _currentState = newState;

            if (_currentState == OverchargeState.Overheat)
            {
                if (_overchargeData != null)
                {
                    _overheatTimer = _overchargeData.OverheatDuration;
                }
            }

            EventBroker.Publish(new OverchargeStateChangedEvent { State = _currentState });
        }
    }
}
