## **7. 개발 마일스톤 및 AI 작업 상세 (Milestones & AI Prompts)**

### **Phase 1: 핵심 게임 루프 및 기초 시스템 (MVP)**

*   **[Milestone 1] 기본 포탑 회전 및 투사체 발사**
    *   **내용:** 입력 시스템에 따른 포탑 회전 로직 및 기본 사격 구현.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        1. Implement Assets/Scripts/TurretShooter.cs:
           - Members: [SerializeField] GameObject projectilePrefab, [SerializeField] Transform firePoint, [SerializeField] float fireRate = 0.5f.
           - Logic: In Update(), rotate transform around Y-axis using Input.GetAxis("Mouse X") * sensitivity (default 5.0f).
           - Shooting: In Update(), if Input.GetButton("Fire1") and Time.time >= nextFireTime, instantiate projectilePrefab at firePoint position/rotation and set nextFireTime = Time.time + fireRate.
        2. Implement Assets/Scripts/Projectile.cs:
           - Members: [SerializeField] float speed = 20f, [SerializeField] float lifeTime = 3f.
           - Logic: Move forward using transform.Translate(Vector3.forward * speed * Time.deltaTime) in Update().
           - Cleanup: Destroy(gameObject, lifeTime) in Start().
        ```
    *   **[Step 2] Antigravity 프롬프트:**
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
           - Assign 'Projectile.prefab' to 'TurretShooter.projectilePrefab' slot.
           - Assign 'FirePoint' to 'TurretShooter.firePoint' slot.
        ```

*   **[Milestone 2] 1인칭 마우스 룩**
    *   **내용:** FPS 방식의 시점 제어 및 클램핑.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/FirstPersonLook.cs:
        - Members: [SerializeField] float sensitivity = 2.0f, [SerializeField] float smoothing = 2.0f.
        - Logic: 
          - Track internal verticalRotation (float).
          - Increment verticalRotation -= Input.GetAxis("Mouse Y") * sensitivity.
          - Clamp verticalRotation between -90f and 90f.
          - Apply localRotation = Quaternion.Euler(verticalRotation, 0, 0) to camera transform.
        - Integration: Access GameManager.Instance.CurrentState to ignore input if state is not 'Playing'.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
        1. Find 'Main Camera' and parent it to 'Player' (Cube at center).
        2. Set Main Camera position to [0, 0.8, 0] (head level).
        3. Attach FirstPersonLook.cs to 'Main Camera'.
        4. Ensure Cursor.lockState = CursorLockMode.Locked in a simple Start() or GameManager setup.
        ```

*   **[Milestone 3] 적 스폰 및 기초 AI**
    *   **내용:** 360도 전 방향 스폰 및 플레이어 추적.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        1. Implement Assets/Scripts/EnemySpawner.cs:
           - Members: [SerializeField] GameObject enemyPrefab, [SerializeField] float spawnRadius = 25f, [SerializeField] float spawnInterval = 2f.
           - Logic: Coroutine 'SpawnRoutine' calculates random point: Vector3 pos = Random.insideUnitCircle.normalized * spawnRadius; 
           - Set pos.y = Random.Range(1f, 5f); pos.z = pos.y; (Convert 2D circle to 3D ring).
           - Instantiate enemyPrefab at pos.
        2. Implement Assets/Scripts/Enemy.cs:
           - Members: [SerializeField] float moveSpeed = 3f, [SerializeField] int health = 10.
           - Logic: Find Player by tag "Player". Use transform.LookAt(player) and transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime).
           - Methods: TakeDamage(int damage) decrement health, Destroy(gameObject) if <= 0.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
        1. Scene Setup: Create empty 'Spawner' at [0,0,0] and attach EnemySpawner.cs.
        2. Prefab: Create Capsule named 'EnemyBot'. Attach Enemy.cs.
        3. Tags/Layers: Create tag "Player" and assign to Player. Create tag "Enemy" for EnemyBot.
        4. Save 'EnemyBot' as 'Assets/Prefabs/EnemyBot.prefab'.
        ```

*   **[Milestone 4] GameManager 및 기본 HUD**
    *   **내용:** 게임 상태 관리 싱글톤 및 기본 UI 표시.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        1. Implement Assets/Scripts/GameManager.cs (Singleton):
           - Properties: int Score { get; private set; }, int Health { get; private set; }.
           - State: public enum GameState { Start, Playing, GameOver }.
           - Methods: AddScore(int), TakeDamage(int), ChangeState(GameState).
        2. Implement Assets/Scripts/GameHUD.cs:
           - Members: [SerializeField] TMPro.TextMeshProUGUI scoreText, [SerializeField] TMPro.TextMeshProUGUI healthText.
           - Logic: Poll GameManager in Update() or use Events to refresh text strings.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
        1. UI Setup: 
           - Create Canvas. Add 'ScoreText' and 'HealthText' (TextMeshPro - Text).
           - Set Anchor to Top-Left and Top-Right respectively.
        2. Management:
           - Create 'Manager' GameObject. Attach GameManager.cs and GameHUD.cs.
           - Link TMP text components to GameHUD serialized fields.
        ```

*   **[Milestone 5] URP 및 물리 설정**
    *   **내용:** 그래픽 파이프라인 최적화 및 레이어 충돌 설정.
    *   **Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Settings]
        1. Graphics: 
           - Create Global Volume. Add Post-processing: Bloom (Intensity: 1.5, Threshold: 1.1), Color Adjustments (Contrast: 10).
        2. Physics:
           - Open Physics Settings collision matrix.
           - Uncheck 'Enemy' vs 'Enemy' (allow overlapping).
           - Uncheck 'Projectile' vs 'Player' (prevent self-damage).
        ```

---

### **Phase 2: 시스템 확장 및 구조 고도화**

*   **[Milestone 6] 이벤트 브로커 시스템**
    *   **내용:** `Action<T>` 기반 중앙 이벤트 버스를 통한 디커플링.
    *   **Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic/Arch]
        Implement Assets/Scripts/EventBroker.cs:
        - Pattern: Static class with public static Action<int> OnScoreChanged, Action<int> OnHealthChanged, Action<GameState> OnStateChanged.
        - Integration: 
          - Update Enemy.cs: Call EventBroker.OnScoreChanged?.Invoke(scoreValue) on death.
          - Update GameManager.cs: Subscribe to OnHealthChanged to update internal state.
        ```

*   **[Milestone 7] 오브젝트 풀링 매니저**
    *   **내용:** `UnityEngine.Pool`을 활용한 런타임 최적화.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/ObjectPoolManager.cs (Singleton):
        - Use UnityEngine.Pool.ObjectPool<GameObject>.
        - Methods: GetEnemy(), ReleaseEnemy(GameObject), GetProjectile(), ReleaseProjectile(GameObject).
        - Refactor: TurretShooter and EnemySpawner must call Get() instead of Instantiate and Release() instead of Destroy.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
        1. Update 'Manager' GameObject: Add ObjectPoolManager.cs.
        2. Prefab Modification: Ensure Projectile.prefab and EnemyBot.prefab have scripts that call ObjectPoolManager.Release() instead of Destroy().
        ```

*   **[Milestone 8] ScriptableObject 기반 데이터화**
    *   **내용:** 에셋 기반 데이터 관리 (Enemy, Weapon, Wave).
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        1. Create Assets/Scripts/Data/EnemyData.cs (ScriptableObject): [CreateAssetMenu] float hp, speed, scoreValue.
        2. Create Assets/Scripts/Data/WeaponData.cs (ScriptableObject): fireRate, damage, projectileSpeed.
        3. Create Assets/Scripts/Data/WaveData.cs (ScriptableObject): List<EnemyData> enemyList, float spawnInterval.
        4. Update Enemy.cs to include 'public void Initialize(EnemyData data)'.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Data]
        1. Create folder 'Assets/Data/Enemies'. 
        2. Create 3 assets: 'Grunt', 'Speedster', 'Tanker' with varying stats.
        3. Create 'Assets/Data/Waves/Wave1.asset' and add 10 'Grunt' entries.
        ```

*   **[Milestone 9] 웨이브 매니저 구현**
    *   **내용:** 시간 경과에 따른 난이도 및 스폰 규칙 관리.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/WaveManager.cs:
        - Members: [SerializeField] List<WaveData> waves.
        - Logic: Track currentWaveIndex. When all enemies in WaveData are spawned and killed, increment wave.
        - Methods: StartNextWave(), OnEnemyKilled().
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Data]
        1. Setup 'WaveManager' in scene.
        2. Populate 'waves' list with Wave1, Wave2... assets created in Milestone 8.
        ```

*   **[Milestone 10] 무기 시스템 아키텍처**
    *   **내용:** 인터페이스 기반 확장형 무기 시스템 설계.
    *   **Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic/Arch]
        1. Define Assets/Scripts/IWeapon.cs: interface with void Fire(), void Reload().
        2. Implement Assets/Scripts/WeaponBase.cs (abstract): common logic for ammo and cooldowns.
        3. Implement Assets/Scripts/TurretWeapon.cs (inherits WeaponBase): Specific fire logic.
        4. Refactor TurretShooter.cs to hold IWeapon reference.
        ```

---

### **Phase 3: 로그라이트 성장 및 투사체 진화**

*   **[Milestone 11] 레벨업 시스템 및 UI**
    *   **내용:** 경험치 기반 성장 및 3개 무작위 강화 선택.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/LevelUpManager.cs:
        - Logic: Collect EXP from EventBroker.OnEnemyKilled. 
        - Math: nextLevelExp = baseExp * (level ^ 1.5).
        - Selection: Randomly pick 3 UpgradeTypes (Speed, Damage, Pierce) from an Array/List.
        - Pause: Set Time.timeScale = 0f during UI display.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/UI]
        1. UI Layout: Create 'LevelUpOverlay' (Panel). Add 3 'ChoiceButton' objects.
        2. Script Link: Link ChoiceButton.OnClick to LevelUpManager.SelectUpgrade(index).
        ```

*   **[Milestone 12] 투사체 변이 물리 로직**
    *   **내용:** 다연발, 관통, 도탄, 폭발 등 물리적 속성 확장.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Update Projectile.cs:
        - Fields: bool canPierce, int bounceCount, bool isExplosive.
        - OnCollisionEnter:
          - If isExplosive: Physics.OverlapSphere(pos, radius) -> deal damage.
          - If bounceCount > 0: Calculate Vector3.Reflect(velocity, normal) and update direction.
          - Else: Release to pool.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Physics]
        1. Layer Assignment: Create layer 'Obstacle'. Assign to all boundary walls.
        2. Effect: Create a simple 'ExplosionSphere' particle system for visual feedback.
        ```

---

### **Phase 4: 콘텐츠 폴리싱 및 모바일 출시 준비**

*   **[Milestone 13] 보스전: 타이탄 코어**
    *   **내용:** 15분 시점 보스 페이즈 전환 및 패턴 구현.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/Boss/TitanCore.cs:
        - FSM: Idle, Phase1 (LaserSweep), Phase2 (CoreVulnerable).
        - Logic: Trigger Phase2 when Health < 50%.
        - Attacks: Use Raycast for laser beams and instantiate smaller missiles.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
        1. Boss Creation: Build large Sphere core with orbiting Cube satellites.
        2. Animation: Create 'Entrance' animation moving boss from sky to center-top.
        3. UI: Create 'BossHealthBar' (Slider) at top of screen.
        ```

*   **[Milestone 14] 오버클럭 모드 (무한)**
    *   **내용:** 보스 격파 후 기하급수적 난이도 상승 로직.
    *   **Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/OverclockMode.cs:
        - Timer: Start after BossDeathEvent.
        - Progression: Increase global multiplier 'difficultyScale' every 60s.
        - Integration: EnemySpawner uses difficultyScale to multiply EnemyData stats.
        ```

*   **[Milestone 15] 특수 적 AI 구현**
    *   **내용:** 블리츠, 아머드, 스웜, 헬파이어 AI 고도화.
    *   **Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic/AI]
        1. Implement BlitzEnemy.cs: Override Move logic with Sine wave offset (Z-axis).
        2. Implement SwarmPod.cs: On death, Instantiate 3 'MiniEnemy' prefabs with small force.
        3. Implement HellfireEnemy.cs: Check distance to player; if < 2m, trigger explosion and self-destruct.
        ```

*   **[Milestone 16] 카메라 쉐이크 연출**
    *   **내용:** 타격감 향상을 위한 카메라 진동 로직 및 피격/사격 시 흔들림 강도 제어.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/Visuals/ScreenShake.cs:
        - Logic: Coroutine 'Shake' that takes duration and magnitude.
        - Integration: Subscribe to EventBroker.OnEnemyKilled and EventBroker.OnHealthChanged to trigger camera offsets.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
        1. Attach ScreenShake.cs to 'Main Camera'.
        2. Set default shake values for different event types in the inspector.
        ```

*   **[Milestone 17] 피버 타임 시스템**
    *   **내용:** 특정 킬 카운트 달성 시 일시적인 공격 속도 강화 및 시각적 피드백 제공.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/FeverManager.cs:
        - Logic: Accumulate 'FeverPoints' on enemy death. When full, trigger FeverMode (Duration: 5s).
        - Effects: Double fire rate and score multiplier during active state.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/VFX]
        1. Update Global Volume: Increase Bloom intensity and add Chromatic Aberration during FeverMode.
        2. UI: Add a 'FeverBar' (Slider) to the HUD and link to FeverManager.
        ```

*   **[Milestone 18] 사운드 시스템 통합**
    *   **내용:** 배경음악(BGM) 및 효과음(SFX) 관리 시스템 구축 및 오디오 리소스 연동.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/Core/SoundManager.cs (Singleton):
        - Methods: PlayBGM(string), PlaySFX(string, Vector3).
        - Logic: Use an AudioSource pool for concurrent SFX playback.
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
        1. Create 'SoundManager' object.
        2. Configure AudioMixer with Master, BGM, and SFX groups.
        3. Link weapon fire and enemy death events to SoundManager calls.
        ```

*   **[Milestone 19] 모바일 광고 연동**
    *   **내용:** 리워드 광고 및 전면 광고 SDK 통합을 통한 수익화 구조 구축.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/Ads/AdManager.cs:
        - Wrapper: Integration with Google AdMob SDK.
        - Methods: LoadRewardedAd(), ShowRewardedAd(Action onComplete).
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Integration]
        1. Open Google AdMob Settings: Input App IDs for Android and iOS.
        2. Build Settings: Configure for mobile platforms (Portrait orientation).
        ```

*   **[Milestone 20] 다국어 지원 (Multilingual Support)**
    *   **내용:** 글로벌 서비스를 위한 다국어(한국어, 영어, 일본어) 텍스트 로컬라이제이션 시스템.
    *   **[Step 1] Jules 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
        Implement Assets/Scripts/Core/LocalizationManager.cs:
        - Logic: Load localized strings from JSON/CSV files based on SystemLanguage.
        - Method: string GetText(string key).
        ```
    *   **[Step 2] Antigravity 프롬프트:**
        ```text
        [CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Data]
        1. Create 'Assets/Resources/Localization/' folder.
        2. Add 'ko.json', 'en.json', 'ja.json' with key-value pairs for UI text.
        ```

---

> [!TIP]
> Phase 5 이후의 서버 구축, PC/VR/콘솔 출시 등 향후 확장 계획은 **[08. 향후 로드맵](08_future_roadmap.md)** 문서에서 확인하실 수 있습니다.
