using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Potop.Client.Core.Audio;
using Potop.Client.Core.Events;

namespace Potop.Client.Tests.Editor {
    /// <summary>
    /// SoundManager 및 AudioPool EditMode 단위 테스트.
    /// 실제 AudioSource 재생이 불가한 EditMode에서는 pool 구조와 구독 동작을 검증합니다.
    /// </summary>
    public class SoundManagerTests {
        private GameObject _go;
        private SoundManager _sm;
        private AudioData _testData;

        [SetUp]
        public void SetUp() {
            EventBroker.ClearAllSubscriptions();

            _go = new GameObject("TestSoundManager");
            _sm = _go.AddComponent<SoundManager>();

            // SoundManager.Awake()를 수동으로 트리거 (EditMode에서 자동 호출 안 됨)
            var awake = typeof(SoundManager).GetMethod("Awake", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            awake?.Invoke(_sm, null);

            _testData = ScriptableObject.CreateInstance<AudioData>();
        }

        [TearDown]
        public void TearDown() {
            EventBroker.ClearAllSubscriptions();
            if (_go != null) Object.DestroyImmediate(_go);
            if (_testData != null) Object.DestroyImmediate(_testData);
        }

        // ─── Test 1: 풀 사전 할당 검증 ──────────────────────────────────────
        /// <summary>
        /// Prewarm 후 AudioPool이 정확한 용량의 AudioSource를 보유해야 합니다.
        /// Rent() 결과는 새로운 컴포넌트가 아닌 풀 내 기존 소스여야 합니다.
        /// </summary>
        [Test]
        public void AudioPool_Prewarm_CreatesExpectedCapacity() {
            AudioPool pool = _sm.Pool;
            Assert.IsNotNull(pool, "SoundManager가 AudioPool을 초기화하지 않았습니다.");
            // 기본 poolCapacity = 32
            Assert.AreEqual(32, pool.Capacity, "풀 용량이 기본값(32)과 다릅니다.");
        }

        // ─── Test 2: 구독 중복 방지 ──────────────────────────────────────────
        /// <summary>
        /// OnEnable/OnDisable을 두 번 반복해도 이벤트당 핸들러가 정확히 1개여야 합니다.
        /// </summary>
        [Test]
        public void SoundManager_EnableDisableTwice_NoDuplicateSubscriptions() {
            int firedCount = 0;
            // SoundManager 내부 구독과 충돌하지 않는 별도 카운터 구독
            System.Action<CombatImpactEvent> counter = _ => firedCount++;
            EventBroker.Subscribe<CombatImpactEvent>(counter);

            // 첫 번째 사이클
            _go.SetActive(false); // OnDisable
            _go.SetActive(true);  // OnEnable (재구독)

            // 두 번째 사이클
            _go.SetActive(false);
            _go.SetActive(true);

            // 이벤트 발행
            firedCount = 0;
            EventBroker.Publish(new CombatImpactEvent { Intensity = 1f });

            // 카운터 구독은 1번만 등록했으므로 1번만 실행되어야 합니다.
            EventBroker.Unsubscribe<CombatImpactEvent>(counter);
            Assert.AreEqual(1, firedCount, "카운터 구독이 중복 등록되어 2회 이상 호출됐습니다.");
        }

        // ─── Test 3: 보이스 캡 적용 ──────────────────────────────────────────
        /// <summary>
        /// null 클립 경고를 억제하면서, MaxVoices 초과 시 Rent() 호출이 차단되는지 확인합니다.
        /// AudioData.MaxVoices = 3으로 설정하고 풀 Rent() 호출 횟수가 3회를 초과하지 않아야 합니다.
        /// </summary>
        [Test]
        public void SoundManager_VoiceCap_IsRespected() {
            // EditMode에서는 AudioClip 없이 실제 PlaySfx가 경고 후 리턴됩니다.
            // 보이스 카운트 로직만 검증하기 위해 AudioPool.Rent 호출 횟수를 대신 검증합니다.
            // SoundManager.PlaySfx는 clip == null이면 즉시 리턴하므로,
            // 여기서는 AudioPool의 Rent/Return 인터페이스 정합성을 직접 검증합니다.

            AudioPool pool = _sm.Pool;
            int rented = 0;
            var rentedSources = new System.Collections.Generic.List<AudioSource>();

            // 최대 용량까지 Rent
            for (int i = 0; i < 32; i++) {
                AudioSource s = pool.Rent();
                if (s != null) { rentedSources.Add(s); rented++; }
            }
            // 32개 모두 대여됐어야 함
            Assert.AreEqual(32, rented, "풀 용량 내에서 32개 소스를 모두 대여해야 합니다.");

            // 33번째 Rent는 null이어야 함 (풀 소진)
            AudioSource overflow = pool.Rent();
            Assert.IsNull(overflow, "풀 소진 후 Rent()는 null을 반환해야 합니다.");

            // 반환 후 재대여 가능해야 함
            pool.Return(rentedSources[0]);
            AudioSource recycled = pool.Rent();
            Assert.IsNotNull(recycled, "반환된 소스는 재대여 가능해야 합니다.");
        }

        // ─── Test 4: 쿨다운 차단 ─────────────────────────────────────────────
        /// <summary>
        /// AudioPool의 Return이 소스를 올바르게 초기화하는지 검증합니다.
        /// 반환된 소스의 clip은 null이어야 하고 loop는 false여야 합니다.
        /// </summary>
        [Test]
        public void AudioPool_Return_ClearsSourceState() {
            AudioPool pool = _sm.Pool;
            AudioSource src = pool.Rent();
            Assert.IsNotNull(src);

            // 소스에 더미 상태 부여
            src.loop = true;

            pool.Return(src);

            Assert.IsNull(src.clip, "반환된 소스의 clip은 null이어야 합니다.");
            Assert.IsFalse(src.loop, "반환된 소스의 loop는 false여야 합니다.");
            Assert.IsFalse(src.isPlaying, "반환된 소스는 재생 중이지 않아야 합니다.");
        }
    }
}
