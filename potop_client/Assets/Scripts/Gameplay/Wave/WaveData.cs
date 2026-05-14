using System;
using System.Collections.Generic;
using UnityEngine;

namespace Potop.Client.Gameplay.Wave {
    /// <summary>
    /// 웨이브의 적 스폰 정보를 정의하는 구조체입니다.
    /// 메모리 단편화를 줄이기 위해 struct로 정의되었습니다.
    /// </summary>
    [Serializable]
    public struct EnemySpawnData {
        /// <summary>
        /// 인스턴스화할 적의 프리팹입니다.
        /// </summary>
        [SerializeField] private GameObject _prefab;

        /// <summary>
        /// 해당 프리팹을 몇 마리 생성할지 나타내는 수량입니다.
        /// </summary>
        [SerializeField] private int _count;

        public GameObject Prefab => _prefab;
        public int Count => _count;
    }

    /// <summary>
    /// 단일 웨이브의 구성을 저장하는 데이터 컨테이너입니다.
    /// 기획자가 인스펙터에서 웨이브 밸런스를 조정할 수 있도록 ScriptableObject를 상속합니다.
    /// </summary>
    [CreateAssetMenu(fileName = "NewWaveData", menuName = "POTOP/Wave Data")]
    public class WaveData : ScriptableObject {
        /// <summary>
        /// 이 웨이브에서 생성될 적들의 목록입니다.
        /// </summary>
        [SerializeField] private List<EnemySpawnData> _enemySpawns = new List<EnemySpawnData>();

        /// <summary>
        /// 이 웨이브가 지속되는 시간(초)입니다.
        /// </summary>
        [SerializeField] private float _duration = 60f;

        /// <summary>
        /// 웨이브 진행도(0~1)에 따른 스폰 강도 변화를 정의합니다.
        /// </summary>
        [SerializeField] private AnimationCurve _spawnIntensityCurve = AnimationCurve.Linear(0, 1, 1, 1);

        /// <summary>
        /// 기본 스폰 간격입니다. (초)
        /// </summary>
        [SerializeField] private float _baseSpawnInterval = 2f;

        public IReadOnlyList<EnemySpawnData> EnemySpawns => _enemySpawns;
        public float Duration => _duration;
        public AnimationCurve SpawnIntensityCurve => _spawnIntensityCurve;
        public float BaseSpawnInterval => _baseSpawnInterval;
    }
}
