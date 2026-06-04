using System.Collections.Generic;
using UnityEngine;
using Potop.Client.Core.Events;

namespace Potop.Client.Core.Audio {
    /// <summary>
    /// 게임 내 모든 오디오 재생을 관리하는 싱글톤 매니저입니다.
    /// EventBroker를 통해 전투, 진행, UI 이벤트를 구독하고
    /// 풀링된 AudioSource로 SFX를 재생하며, 전용 소스로 BGM을 관리합니다.
    /// </summary>
    public class SoundManager : MonoBehaviour {
        // ─── 싱글톤 ──────────────────────────────────────────────────────────
        public static SoundManager Instance { get; private set; }

        // ─── 풀 설정 ─────────────────────────────────────────────────────────
        [Header("Pool")]
        [SerializeField, Min(8)] private int _poolCapacity = 32;

        // ─── SFX 키 (Inspector에서 AudioData 에셋 할당) ──────────────────────
        [Header("SFX — assign AudioData assets in Inspector")]
        [SerializeField] private AudioData _sfxCombatHit;
        [SerializeField] private AudioData _sfxCombatKill;
        [SerializeField] private AudioData _sfxPlayerDamage;
        [SerializeField] private AudioData _sfxGemPickup;
        [SerializeField] private AudioData _sfxBossIntro;

        // ─── BGM 상태 매핑 ───────────────────────────────────────────────────
        [Header("BGM — map BgmState → AudioData")]
        [SerializeField] private BgmStateEntry[] _bgmStates;

        [System.Serializable]
        private struct BgmStateEntry {
            public BgmState State;
            public AudioData Data;
        }

        // ─── 내부 상태 ────────────────────────────────────────────────────────
        private AudioPool _pool;
        private AudioSource _bgmSource;
        private BgmState _currentBgmState = BgmState.Normal;

        // 보이스 버짓: 키별 현재 활성 소스 수 추적
        private readonly Dictionary<AudioData, int> _activeVoices = new Dictionary<AudioData, int>();
        // 쿨다운: 키별 마지막 재생 시각 (Time.unscaledTime 기준)
        private readonly Dictionary<AudioData, float> _lastPlayTime = new Dictionary<AudioData, float>();

        // 반환 추적: 어떤 소스가 어떤 AudioData에서 대여됐는지 (자동-반환 시 카운트 감소용)
        private readonly Dictionary<AudioSource, AudioData> _sourceOwner = new Dictionary<AudioSource, AudioData>();

        // ─── 구독 중복 방지 플래그 ────────────────────────────────────────────
        private bool _subscribed = false;

        // LateUpdate에서 반환 대상 소스를 임시 보관하는 재사용 리스트 (GC-free)
        private readonly List<AudioSource> _toReturn = new List<AudioSource>();

        // ─── 유니티 라이프사이클 ─────────────────────────────────────────────
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            if (Application.isPlaying) {
                DontDestroyOnLoad(gameObject);
            }

            // BGM 전용 소스 (풀과 무관하게 상시 유지)
            _bgmSource = gameObject.AddComponent<AudioSource>();
            _bgmSource.playOnAwake = false;
            _bgmSource.loop = true;

            // AudioPool 자식 오브젝트 생성 및 사전 할당
            GameObject poolGo = new GameObject("AudioPool");
            poolGo.transform.SetParent(transform, false);
            _pool = poolGo.AddComponent<AudioPool>();
            _pool.Prewarm(_poolCapacity);
        }

        private void OnEnable() {
            Subscribe();
        }

        private void OnDisable() {
            Unsubscribe();
        }

        private void OnDestroy() {
            if (Instance == this) {
                Unsubscribe();
                Instance = null;
            }
        }

        // ─── LateUpdate: 자동 반환된 소스의 보이스 카운트 동기화 ─────────────
        private void LateUpdate() {
            // _pool.LateUpdate()가 소스를 자동 반환한 뒤, _sourceOwner 맵을 정리합니다.
            // 소스가 더 이상 재생 중이지 않고 _sourceOwner에 남아있는 경우 카운트를 감소시킵니다.
            _toReturn.Clear();
            foreach (var kv in _sourceOwner) {
                AudioSource src = kv.Key;
                // 풀의 LateUpdate가 먼저 실행되어 소스를 반환했을 때 clip이 null로 초기화됨
                if (src.clip == null && !src.isPlaying) {
                    DecrementVoice(kv.Value);
                    _toReturn.Add(src);
                }
            }
            for (int i = 0; i < _toReturn.Count; i++) {
                _sourceOwner.Remove(_toReturn[i]);
            }
        }

        // ─── 구독 관리 ────────────────────────────────────────────────────────
        private void Subscribe() {
            if (_subscribed) return;
            EventBroker.Subscribe<CombatImpactEvent>(OnCombatImpact);
            EventBroker.Subscribe<EnemyDiedEvent>(OnEnemyDied);
            EventBroker.Subscribe<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
            EventBroker.Subscribe<GemPickedUpEvent>(OnGemPickedUp);
            EventBroker.Subscribe<FeverStateChangedEvent>(OnFeverStateChanged);
            EventBroker.Subscribe<FeverLevelChangedEvent>(OnFeverLevelChanged);
            EventBroker.Subscribe<BossSpawnedEvent>(OnBossSpawned);
            EventBroker.Subscribe<BossPhaseChangedEvent>(OnBossPhaseChanged);
            EventBroker.Subscribe<BossDefeatedEvent>(OnBossDefeated);
            _subscribed = true;
        }

        private void Unsubscribe() {
            if (!_subscribed) return;
            EventBroker.Unsubscribe<CombatImpactEvent>(OnCombatImpact);
            EventBroker.Unsubscribe<EnemyDiedEvent>(OnEnemyDied);
            EventBroker.Unsubscribe<PlayerTakeDamageEvent>(OnPlayerTakeDamage);
            EventBroker.Unsubscribe<GemPickedUpEvent>(OnGemPickedUp);
            EventBroker.Unsubscribe<FeverStateChangedEvent>(OnFeverStateChanged);
            EventBroker.Unsubscribe<FeverLevelChangedEvent>(OnFeverLevelChanged);
            EventBroker.Unsubscribe<BossSpawnedEvent>(OnBossSpawned);
            EventBroker.Unsubscribe<BossPhaseChangedEvent>(OnBossPhaseChanged);
            EventBroker.Unsubscribe<BossDefeatedEvent>(OnBossDefeated);
            _subscribed = false;
        }

        // ─── 이벤트 핸들러 ───────────────────────────────────────────────────
        private void OnCombatImpact(CombatImpactEvent e) => PlaySfx(_sfxCombatHit);
        private void OnEnemyDied(EnemyDiedEvent e) => PlaySfx(_sfxCombatKill);
        private void OnPlayerTakeDamage(PlayerTakeDamageEvent e) => PlaySfx(_sfxPlayerDamage);
        private void OnGemPickedUp(GemPickedUpEvent e) => PlaySfx(_sfxGemPickup);

        private void OnFeverStateChanged(FeverStateChangedEvent e) {
            SetMusicState(e.IsFeverActive ? BgmState.FeverActive : BgmState.Normal);
        }

        private void OnFeverLevelChanged(FeverLevelChangedEvent e) {
            if (e.Level >= 3) SetMusicState(BgmState.FeverMax);
        }

        private void OnBossSpawned(BossSpawnedEvent e) {
            PlaySfx(_sfxBossIntro);
            SetMusicState(BgmState.BossIntro);
        }

        private void OnBossPhaseChanged(BossPhaseChangedEvent e) {
            SetMusicState(BgmState.BossActive);
        }

        private void OnBossDefeated(BossDefeatedEvent e) {
            SetMusicState(BgmState.BossDefeated);
        }

        // ─── 공개 API ────────────────────────────────────────────────────────

        /// <summary>
        /// 지정된 AudioData 에셋으로 단발 SFX를 재생합니다.
        /// data가 null이거나 클립이 없으면 조용히 무시합니다.
        /// 쿨다운 및 보이스 캡을 초과하면 재생을 건너뜁니다.
        /// </summary>
        public void PlaySfx(AudioData data) {
            if (data == null) return;

            AudioClip clip = data.PickClip();
            if (clip == null) {
                Debug.LogWarning($"[SoundManager] AudioData '{data.name}'에 할당된 클립이 없습니다.");
                return;
            }

            // 쿨다운 검사
            float now = Time.unscaledTime;
            if (_lastPlayTime.TryGetValue(data, out float last) && now - last < data.Cooldown) {
                return;
            }

            // 보이스 캡 검사
            _activeVoices.TryGetValue(data, out int voices);
            if (voices >= data.MaxVoices) return;

            // 풀에서 소스 대여
            AudioSource source = _pool.Rent();
            if (source == null) return; // 전체 풀 소진

            source.clip = clip;
            source.outputAudioMixerGroup = data.MixerGroup;
            source.volume = data.Volume;
            source.pitch = Random.Range(data.PitchMin, data.PitchMax);
            source.loop = false;
            source.Play();

            _lastPlayTime[data] = now;
            _activeVoices[data] = voices + 1;
            _sourceOwner[source] = data;
        }

        /// <summary>
        /// BGM 전용 AudioSource로 지정된 AudioData를 재생합니다.
        /// 이전 BGM을 즉시 중단하고 교체합니다. (크로스페이드는 Phase 9+ 범위)
        /// data가 null이면 BGM을 정지합니다.
        /// </summary>
        public void PlayBgm(AudioData data) {
            if (data == null) {
                StopBgm();
                return;
            }

            AudioClip clip = data.PickClip();
            if (clip == null) return;

            _bgmSource.clip = clip;
            _bgmSource.outputAudioMixerGroup = data.MixerGroup;
            _bgmSource.volume = data.Volume;
            _bgmSource.pitch = 1f;
            _bgmSource.loop = data.Loop;
            _bgmSource.Play();
        }

        /// <summary>BGM을 정지합니다.</summary>
        public void StopBgm() {
            _bgmSource.Stop();
            _bgmSource.clip = null;
        }

        /// <summary>
        /// BGM 상태를 변경하고 BgmStateChangedEvent를 발행합니다.
        /// 실제 레이어드 컴포지션 로직은 Phase 9+ 범위입니다.
        /// </summary>
        public void SetMusicState(BgmState state) {
            if (_currentBgmState == state) return;
            _currentBgmState = state;

            // 상태에 매핑된 BGM 에셋 재생
            for (int i = 0; i < _bgmStates.Length; i++) {
                if (_bgmStates[i].State == state) {
                    PlayBgm(_bgmStates[i].Data);
                    break;
                }
            }

            EventBroker.Publish(new BgmStateChangedEvent { State = state });
        }

        // ─── 내부 유틸 ───────────────────────────────────────────────────────
        private void DecrementVoice(AudioData data) {
            if (_activeVoices.TryGetValue(data, out int count) && count > 0) {
                _activeVoices[data] = count - 1;
            }
        }

        // ─── 테스트 전용 접근자 ───────────────────────────────────────────────
        /// <summary>[테스트 전용] 지정 AudioData의 현재 활성 보이스 수를 반환합니다.</summary>
        internal int GetActiveVoices(AudioData data) {
            _activeVoices.TryGetValue(data, out int count);
            return count;
        }

        /// <summary>[테스트 전용] AudioPool 참조를 반환합니다.</summary>
        internal AudioPool Pool => _pool;

        /// <summary>[테스트 전용] 현재 BGM 상태를 반환합니다.</summary>
        internal BgmState CurrentBgmState => _currentBgmState;
    }
}
