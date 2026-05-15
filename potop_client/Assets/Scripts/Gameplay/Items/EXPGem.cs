using System;
using UnityEngine;
using Potop.Client.Core.Events;
using Potop.Client.Core.Pooling;

namespace Potop.Client.Gameplay.Items
{
    /// <summary>
    /// 적이 처치되었을 때 발생하는 이벤트입니다.
    /// 위치와 경험치 가치 정보를 포함합니다.
    /// </summary>
    public struct EnemyKilledEvent
    {
        public Vector3 Position;
        public int ExpValue;
    }

    /// <summary>
    /// 경험치 보석을 획득했을 때 발생하는 이벤트입니다.
    /// </summary>
    public struct EXPGemCollectedEvent
    {
        public int XPValue;
    }

    /// <summary>
    /// 경험치 보석 오브젝트의 자력 흡수 및 획득을 처리하는 컴포넌트입니다.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class EXPGem : MonoBehaviour
    {
        [SerializeField] private EXPGemData _gemData;
        [SerializeField, Min(0.1f)] private float _magnetRadius = 3f;
        [SerializeField, Min(1f)] private float _moveSpeed = 15f;
        [SerializeField] private LayerMask _playerLayerMask;
        [SerializeField] private float _sqrCollectionThreshold = 1.0f; // 매직넘버 제거: 흡수 거리 임계값의 제곱

        private Transform _playerTarget;
        private bool _isAttracted;
        private static Collider[] _overlapResults = new Collider[50];
        private static readonly int ColorPropertyId = Shader.PropertyToID("_Color"); // Shader ID 캐싱
        private static MaterialPropertyBlock _sharedPropertyBlock; // PropertyBlock 재사용

        private Renderer _cachedRenderer;

        // 이벤트 구독 및 스폰 관리를 위한 정적 관리자 역할 변수
        private static bool _isSubscribed = false;
        private static EXPGemData _blueGemData;
        private static EXPGemData _greenGemData;
        private static EXPGemData _redGemData;

        /// <summary>
        /// 초기화 시 정적 이벤트 구독을 설정합니다.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitializeSpawner()
        {
            if (_isSubscribed)
            {
                EventBroker.Unsubscribe<EnemyKilledEvent>(OnEnemyKilled);
            }
            EventBroker.Subscribe<EnemyKilledEvent>(OnEnemyKilled);
            _isSubscribed = true;
        }

        private static void OnEnemyKilled(EnemyKilledEvent e)
        {
            if (_blueGemData == null) _blueGemData = Resources.Load<EXPGemData>("Data/Items/BlueGemData");
            if (_greenGemData == null) _greenGemData = Resources.Load<EXPGemData>("Data/Items/GreenGemData");
            if (_redGemData == null) _redGemData = Resources.Load<EXPGemData>("Data/Items/RedGemData");

            EXPGemData selectedData = GetGemDataByExp(e.ExpValue);

            if (selectedData != null && selectedData.Prefab != null && PoolManager.Instance != null)
            {
                GameObject gemInstance = PoolManager.Instance.Spawn(selectedData.Prefab, e.Position, Quaternion.identity);
                EXPGem gemComponent = gemInstance.GetComponent<EXPGem>();
                if (gemComponent != null)
                {
                    gemComponent.Initialize(selectedData);
                }
            }
        }

        private static EXPGemData GetGemDataByExp(int expValue)
        {
            if (expValue >= 200) return _redGemData;
            if (expValue >= 50) return _greenGemData;
            return _blueGemData;
        }

        private void Awake()
        {
            // Renderer 캐싱 (성능 최적화)
            _cachedRenderer = GetComponentInChildren<Renderer>();
        }

        /// <summary>
        /// 보석 데이터를 초기화하고 시각적 요소를 적용합니다.
        /// </summary>
        public void Initialize(EXPGemData data)
        {
            _gemData = data;

            // 색상 적용 (최적화된 MaterialPropertyBlock 사용)
            if (_cachedRenderer != null)
            {
                if (_sharedPropertyBlock == null)
                {
                    _sharedPropertyBlock = new MaterialPropertyBlock();
                }

                _cachedRenderer.GetPropertyBlock(_sharedPropertyBlock);
                _sharedPropertyBlock.SetColor(ColorPropertyId, data.VisualColor);
                _cachedRenderer.SetPropertyBlock(_sharedPropertyBlock);
            }
        }

        private void OnEnable()
        {
            _isAttracted = false;
            _playerTarget = null;
            // 0.1초마다 주변 플레이어 탐색 (성능 최적화)
            InvokeRepeating(nameof(CheckMagnetRadius), 0.1f, 0.1f);
        }

        private void OnDisable()
        {
            CancelInvoke(nameof(CheckMagnetRadius));
        }

        private void CheckMagnetRadius()
        {
            if (_isAttracted) return;

            int count = Physics.OverlapSphereNonAlloc(transform.position, _magnetRadius, _overlapResults, _playerLayerMask);
            for (int i = 0; i < count; i++)
            {
                if (_overlapResults[i].CompareTag("Player"))
                {
                    _playerTarget = _overlapResults[i].transform;
                    _isAttracted = true;
                    CancelInvoke(nameof(CheckMagnetRadius)); // 이미 이끌림 상태면 더 이상 탐색 불필요
                    break;
                }
            }
        }

        private void FixedUpdate()
        {
            if (_isAttracted && _playerTarget != null)
            {
                // 플레이어 방향으로 이동
                transform.position = Vector3.MoveTowards(transform.position, _playerTarget.position, _moveSpeed * Time.fixedDeltaTime);

                // 흡수 거리 도달 체크 (매직넘버 제거)
                if ((transform.position - _playerTarget.position).sqrMagnitude <= _sqrCollectionThreshold)
                {
                    Collect();
                }
            }
        }

        private void Collect()
        {
            if (_gemData != null)
            {
                // LevelingManager가 기대하는 EXPCollectedEvent로 통일
                EventBroker.Publish(new Progression.EXPCollectedEvent { Amount = _gemData.XPValue });
            }

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
