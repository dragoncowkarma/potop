using UnityEngine;

namespace Potop.Client.Core.Audio {
    /// <summary>
    /// 전투 SFX 재생을 위한 AudioSource 오브젝트 풀입니다.
    /// 런타임 핫패스에서 AddComponent 또는 new GameObject 호출을 일절 허용하지 않습니다.
    /// 모든 AudioSource는 Prewarm() 시점에 사전 할당됩니다.
    /// </summary>
    public class AudioPool : MonoBehaviour {
        private AudioSource[] _sources;
        private bool[] _inUse;
        private int _capacity;

        /// <summary>
        /// 풀을 초기화하고 AudioSource를 사전 할당합니다.
        /// SoundManager.Awake()에서 한 번만 호출해야 합니다.
        /// </summary>
        /// <param name="capacity">사전 할당할 AudioSource 개수</param>
        public void Prewarm(int capacity) {
            _capacity = capacity;
            _sources = new AudioSource[capacity];
            _inUse = new bool[capacity];

            for (int i = 0; i < capacity; i++) {
                _sources[i] = gameObject.AddComponent<AudioSource>();
                _sources[i].playOnAwake = false;
            }
        }

        /// <summary>
        /// 유휴 AudioSource를 대여합니다. 가용 소스가 없으면 null을 반환합니다.
        /// </summary>
        public AudioSource Rent() {
            for (int i = 0; i < _capacity; i++) {
                if (!_inUse[i]) {
                    _inUse[i] = true;
                    return _sources[i];
                }
            }
            return null; // 보이스 버짓 초과
        }

        /// <summary>
        /// 사용이 끝난 AudioSource를 풀에 반환하고 초기화합니다.
        /// </summary>
        public void Return(AudioSource source) {
            for (int i = 0; i < _capacity; i++) {
                if (_sources[i] == source) {
                    source.Stop();
                    source.clip = null;
                    source.loop = false;
                    _inUse[i] = false;
                    return;
                }
            }
        }

        /// <summary>
        /// 자연 종료된 클립을 감지하여 풀에 자동 반환합니다. (GC-free 스캔)
        /// </summary>
        private void LateUpdate() {
            for (int i = 0; i < _capacity; i++) {
                if (_inUse[i] && !_sources[i].isPlaying && !_sources[i].loop) {
                    _sources[i].clip = null;
                    _inUse[i] = false;
                }
            }
        }

        // ----- 테스트 전용 접근자 -----

        /// <summary>[테스트 전용] 현재 사용 중인 소스 수를 반환합니다.</summary>
        internal int ActiveCount() {
            int count = 0;
            for (int i = 0; i < _capacity; i++) {
                if (_inUse[i]) count++;
            }
            return count;
        }

        /// <summary>[테스트 전용] 풀의 총 용량을 반환합니다.</summary>
        internal int Capacity => _capacity;
    }
}
