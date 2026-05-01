# Phase 2: 시스템 확장 및 구조 고도화 - AI Prompts

## [Milestone 9] 이벤트 브로커 시스템
### Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic/Arch]
Implement Assets/Scripts/Core/EventBroker.cs:
- Pattern: Static class with public static Action<int> OnScoreChanged, Action<int> OnHealthChanged, Action<GameState> OnStateChanged.
- Integration: 
  - Update Enemy.cs: Call EventBroker.OnScoreChanged?.Invoke(_scoreValue) on death.
  - Update GameManager.cs: Subscribe to OnHealthChanged to update internal state.
```

## [Milestone 10] 오브젝트 풀링 매니저
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Core/ObjectPoolManager.cs (Singleton):
- Use UnityEngine.Pool.ObjectPool<GameObject>.
- Methods: public GameObject GetEnemy(); public void ReleaseEnemy(GameObject enemy); public GameObject GetProjectile(); public void ReleaseProjectile(GameObject projectile).
- Refactor: TurretShooter and EnemySpawner must call Get() instead of Instantiate and Release() instead of Destroy.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
1. Update 'Manager' GameObject: Add ObjectPoolManager.cs.
2. Prefab Modification: Ensure Projectile.prefab and EnemyBot.prefab have scripts that call ObjectPoolManager.Release() instead of Destroy().
```

## [Milestone 11] ScriptableObject 기반 데이터화
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
1. Create Assets/Scripts/Data/EnemyData.cs (ScriptableObject): [CreateAssetMenu] private fields with _hp, _speed, _scoreValue.
2. Create Assets/Scripts/Data/WeaponData.cs (ScriptableObject): _fireRate, _damage, _projectileSpeed.
3. Create Assets/Scripts/Data/WaveData.cs (ScriptableObject): _enemyList (List<EnemyData>), _spawnInterval.
4. Update Enemy.cs to include 'public void Initialize(EnemyData data)'.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Data]
1. Create folder 'Assets/Data/Enemies'. 
2. Create 3 assets: 'Grunt', 'Speedster', 'Tanker' with varying stats.
3. Create 'Assets/Data/Waves/Wave1.asset' and add 10 'Grunt' entries.
```

## [Milestone 12] 웨이브 매니저 구현
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Gameplay/WaveManager.cs:
- Members: [SerializeField] private List<WaveData> _waves;
- Logic: Track _currentWaveIndex. When all enemies in WaveData are spawned and killed, increment wave.
- Methods: public void StartNextWave(); public void OnEnemyKilled().
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Data]
1. Setup 'WaveManager' in scene.
2. Populate 'waves' list with Wave1, Wave2... assets created in Milestone 11.
```

## [Milestone 13] 무기 시스템 아키텍처
### Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic/Arch]
1. Define Assets/Scripts/Gameplay/IWeapon.cs: interface with public void Fire(); public void Reload();
2. Implement Assets/Scripts/Gameplay/WeaponBase.cs (abstract): common logic for ammo and cooldowns using private fields (_ammo, etc).
3. Implement Assets/Scripts/Gameplay/TurretWeapon.cs (inherits WeaponBase): Specific fire logic.
4. Refactor TurretShooter.cs to hold IWeapon reference.
```

