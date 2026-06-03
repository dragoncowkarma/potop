using UnityEngine;
using UnityEngine.Audio;

namespace Potop.Client.Core.Audio {
    /// <summary>
    /// 단일 사운드 항목의 재생 계약을 정의하는 ScriptableObject입니다.
    /// 모든 SFX/BGM 참조는 이 에셋을 통해 간접적으로 접근하며, 게임플레이 코드에 클립을 직접 하드코딩하는 것을 금지합니다.
    /// 에셋 네이밍: SFX_[Category]_[Name] / BGM_[State]_[Name]
    /// </summary>
    [CreateAssetMenu(fileName = "New AudioData", menuName = "Potop/Audio/AudioData")]
    public class AudioData : ScriptableObject {
        [Header("Clips — randomly selected on each play")]
        [SerializeField] private AudioClip[] _clips;

        [Header("Mixer")]
        [SerializeField] private AudioMixerGroup _mixerGroup;

        [Header("Volume & Pitch")]
        [SerializeField, Range(0f, 1f)] private float _volume = 1f;
        [SerializeField, Range(0.1f, 3f)] private float _pitchMin = 0.9f;
        [SerializeField, Range(0.1f, 3f)] private float _pitchMax = 1.1f;

        [Header("Voice Budget")]
        [Tooltip("동일 사운드의 최소 재생 간격(초). 0이면 쿨다운 없음.")]
        [SerializeField, Min(0f)] private float _cooldown = 0f;
        [Tooltip("동시에 허용되는 최대 보이스 수.")]
        [SerializeField, Min(1)] private int _maxVoices = 4;

        [Header("Mobile — import SFX clips as 'Compressed In Memory'")]
        [SerializeField] private bool _loop = false;

        /// <summary>재생할 오디오 클립 배열입니다. 무작위로 하나가 선택됩니다.</summary>
        public AudioClip[] Clips => _clips;

        /// <summary>라우팅할 Audio Mixer 그룹입니다.</summary>
        public AudioMixerGroup MixerGroup => _mixerGroup;

        /// <summary>재생 볼륨입니다. (0~1)</summary>
        public float Volume => _volume;

        /// <summary>피치 최솟값입니다.</summary>
        public float PitchMin => _pitchMin;

        /// <summary>피치 최댓값입니다.</summary>
        public float PitchMax => _pitchMax;

        /// <summary>동일 사운드의 최소 재생 간격(초)입니다.</summary>
        public float Cooldown => _cooldown;

        /// <summary>동시 최대 보이스 수입니다.</summary>
        public int MaxVoices => _maxVoices;

        /// <summary>루프 재생 여부입니다. BGM에서 사용합니다.</summary>
        public bool Loop => _loop;

        /// <summary>
        /// 클립 배열에서 무작위로 하나를 반환합니다.
        /// 클립이 없으면 null을 반환합니다.
        /// </summary>
        public AudioClip PickClip() {
            if (_clips == null || _clips.Length == 0) return null;
            return _clips.Length == 1 ? _clips[0] : _clips[Random.Range(0, _clips.Length)];
        }
    }
}
