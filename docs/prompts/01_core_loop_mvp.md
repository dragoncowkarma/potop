# Phase 1: 핵심 게임 루프 및 기초 시스템 (MVP) - AI Prompts

## [Milestone 1] 기본 포탑 회전 및 투사체 발사
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
1. Implement Assets/Scripts/Gameplay/TurretShooter.cs:
   - Members: [SerializeField] private GameObject _projectilePrefab; [SerializeField] private Transform _firePoint; [SerializeField] private float _fireRate = 0.5f;
   - Logic: Use Unity New Input System. In Update(), rotate transform around Y-axis using horizontal mouse delta from Look action.
   - Shooting: In Update(), if Fire action is performed and Time.time >= _nextFireTime, instantiate _projectilePrefab at _firePoint position/rotation and set _nextFireTime = Time.time + _fireRate.
2. Implement Assets/Scripts/Gameplay/Projectile.cs:
   - Members: [SerializeField] private float _speed = 20f; [SerializeField] private float _lifeTime = 3f;
   - Logic: Move forward using transform.Translate(Vector3.forward * _speed * Time.deltaTime) in Update().
   - Cleanup: Destroy(gameObject, _lifeTime) in Start().
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
1. Hierarchy Setup:
   - Create a Cube named 'Turret'. Set scale to [1, 0.5, 1].
   - Create an empty child named 'FirePoint' at [0, 0, 0.6] relative to Turret.
2. Component Setup:
   - Attach TurretShooter.cs to 'Turret'.
3. Prefab Creation:
   - Create a Sphere named 'ProjectileInstance', scale [0.2, 0.2, 0.2].
   - Add Rigidbody: Set Use Gravity = false, Is Kinematic = true.
   - Attach Projectile.cs.
   - Save as 'Assets/Prefabs/Projectile.prefab' and delete instance from scene.
4. Linkage:
   - Assign 'Projectile.prefab' to 'TurretShooter._projectilePrefab' slot.
   - Assign 'FirePoint' to 'TurretShooter._firePoint' slot.
```

## [Milestone 2] 1인칭 마우스 룩
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Gameplay/FirstPersonLook.cs:
- Members: [SerializeField] private float _sensitivity = 2.0f; [SerializeField] private float _smoothing = 2.0f;
- Logic: 
  - Use Unity New Input System (Look action).
  - Track internal _verticalRotation (float).
  - Increment _verticalRotation -= LookAction.y * _sensitivity.
  - Clamp _verticalRotation between -90f and 90f.
  - Apply localRotation = Quaternion.Euler(_verticalRotation, 0, 0) to camera transform.
- Integration: Access GameManager.Instance.CurrentState to ignore input if state is not 'Playing'.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
1. Find 'Main Camera' and parent it to 'Player' (Cube at center).
2. Set Main Camera position to [0, 0.8, 0] (head level).
3. Attach FirstPersonLook.cs to 'Main Camera'.
4. Ensure Cursor.lockState = CursorLockMode.Locked in a simple Start() or GameManager setup.
```

## [Milestone 3] 적 스폰 및 기초 AI
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
1. Implement Assets/Scripts/Gameplay/EnemySpawner.cs:
   - Members: [SerializeField] private GameObject _enemyPrefab; [SerializeField] private float _spawnRadius = 25f; [SerializeField] private float _spawnInterval = 2f;
   - Logic: Coroutine 'SpawnRoutine' calculates random point: Vector3 pos = Random.insideUnitCircle.normalized * _spawnRadius; 
   - Set pos.y = Random.Range(1f, 5f); pos.z = pos.y; (Convert 2D circle to 3D ring).
   - Instantiate _enemyPrefab at pos.
2. Implement Assets/Scripts/Gameplay/Enemy.cs:
   - Members: [SerializeField] private float _moveSpeed = 3f; [SerializeField] private int _health = 10;
   - Logic: Use GameManager.Instance.Player to find target. Use transform.LookAt(target) and transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime).
   - Methods: public void TakeDamage(int damage) decrement _health, Destroy(gameObject) if <= 0.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
1. Scene Setup: Create empty 'Spawner' at [0,0,0] and attach EnemySpawner.cs.
2. Prefab: Create Capsule named 'EnemyBot'. Attach Enemy.cs.
3. Tags/Layers: Create tag "Player" and assign to Player. Create tag "Enemy" for EnemyBot.
4. Save 'EnemyBot' as 'Assets/Prefabs/EnemyBot.prefab'.
```

## [Milestone 4] GameManager 및 기본 HUD
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
1. Implement Assets/Scripts/Core/GameManager.cs (Singleton):
   - Properties: public int Score { get; private set; }; public int Health { get; private set; };
   - State: public enum GameState { Start, Playing, GameOver }.
   - Methods: public void AddScore(int value); public void TakeDamage(int value); public void ChangeState(GameState newState).
2. Implement Assets/Scripts/UI/GameHUD.cs:
   - Members: [SerializeField] private UIDocument _uiDocument;
   - Logic: Use Unity UI Toolkit. Access VisualElements 'score-label' and 'health-label' from _uiDocument.rootVisualElement.
   - Refresh: Update .text properties based on GameManager.Instance values.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
1. UI Setup: 
   - Create a UI Document (UXML). Add Labels with IDs 'score-label' and 'health-label'.
   - Style using USS for layout.
2. Management:
   - Create 'Manager' GameObject. Attach GameManager.cs and GameHUD.cs.
   - Link UIDocument asset to GameHUD._uiDocument slot.
```

## [Milestone 5] URP 및 물리 설정
### Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Settings]
1. Graphics: 
   - Create Global Volume. Add Post-processing: Bloom (Intensity: 1.5, Threshold: 1.1), Color Adjustments (Contrast: 10).
2. Physics:
   - Open Physics Settings collision matrix.
   - Uncheck 'Enemy' vs 'Enemy' (allow overlapping).
   - Uncheck 'Projectile' vs 'Player' (prevent self-damage).
```

