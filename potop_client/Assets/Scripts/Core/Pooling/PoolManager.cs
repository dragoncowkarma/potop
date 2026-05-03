using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Potop.Client.Core.Pooling {
    /// <summary>
    /// 게임 오브젝트의 생성과 삭제를 최적화하기 위한 오브젝트 풀링 관리자입니다.
    /// 싱글톤 패턴으로 구현되어 전역에서 접근할 수 있습니다.
    /// </summary>
    public class PoolManager : MonoBehaviour {
        /// <summary>
        /// PoolManager의 싱글톤 인스턴스입니다.
        /// </summary>
        public static PoolManager Instance { get; private set; }

        private Dictionary<GameObject, IObjectPool<GameObject>> _pools = new Dictionary<GameObject, IObjectPool<GameObject>>();
        private Dictionary<GameObject, IObjectPool<GameObject>> _spawnedObjects = new Dictionary<GameObject, IObjectPool<GameObject>>();

        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            // DontDestroyOnLoad(gameObject); // 필요한 경우 주석 해제
        }

        private void OnDestroy() {
            if (Instance == this) {
                Instance = null;
            }
        }

        /// <summary>
        /// 풀에서 오브젝트를 가져와 활성화하고, 지정된 위치와 회전값으로 배치합니다.
        /// </summary>
        /// <param name="prefab">스폰할 원본 프리팹</param>
        /// <param name="position">배치할 위치</param>
        /// <param name="rotation">배치할 회전값</param>
        /// <returns>활성화된 게임 오브젝트</returns>
        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation) {
            if (prefab == null) return null;

            if (!_pools.ContainsKey(prefab)) {
                _pools[prefab] = new ObjectPool<GameObject>(
                    createFunc: () => {
                        GameObject obj = Instantiate(prefab);
                        // 프리팹 원본에 대한 참조를 유지하여 나중에 어떤 풀에서 생성되었는지 알 수 있도록 함.
                        // 이 경우에는 _spawnedObjects 딕셔너리로 관리합니다.
                        return obj;
                    },
                    actionOnGet: (obj) => obj.SetActive(true),
                    actionOnRelease: (obj) => obj.SetActive(false),
                    actionOnDestroy: (obj) => Destroy(obj),
                    collectionCheck: true,
                    defaultCapacity: 10,
                    maxSize: 1000
                );
            }

            IObjectPool<GameObject> pool = _pools[prefab];
            GameObject instance = pool.Get();
            instance.transform.SetPositionAndRotation(position, rotation);

            _spawnedObjects[instance] = pool;

            return instance;
        }

        /// <summary>
        /// 활성화된 오브젝트를 다시 풀로 반환하여 비활성화합니다.
        /// </summary>
        /// <param name="instance">비활성화할 오브젝트 인스턴스</param>
        public void Despawn(GameObject instance) {
            if (instance == null) return;

            if (_spawnedObjects.TryGetValue(instance, out IObjectPool<GameObject> pool)) {
                _spawnedObjects.Remove(instance);
                pool.Release(instance);
            } else {
                // 풀을 통해 생성된 오브젝트가 아닌 경우 기본 파괴를 수행
                Destroy(instance);
            }
        }
    }
}
