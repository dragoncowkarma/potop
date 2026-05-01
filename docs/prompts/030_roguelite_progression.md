# Phase 3: 로그라이트 성장 및 투사체 진화 - AI Prompts

## [Milestone 13] 무기 시스템 아키텍처
### Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic/Arch]
1. Define Assets/Scripts/Gameplay/IWeapon.cs: interface with public void Fire(); public void Reload();
2. Implement Assets/Scripts/Gameplay/WeaponBase.cs (abstract): common logic for ammo and cooldowns using private fields (_ammo, etc).
3. Implement Assets/Scripts/Gameplay/TurretWeapon.cs (inherits WeaponBase): Specific fire logic.
4. Refactor TurretShooter.cs to hold IWeapon reference.
```


## [Milestone 14] 레벨업 시스템 및 UI
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Gameplay/LevelUpManager.cs:
- Logic: Collect EXP from EventBroker.OnEnemyKilled. 
- Math: _nextLevelExp = _baseExp * (Mathf.Pow(_level, 1.5f)).
- Selection: Randomly pick 3 UpgradeTypes (Speed, Damage, Pierce) from an Array/List.
- Pause: Set Time.timeScale = 0f during UI display.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/UI]
1. UI Layout: Create 'LevelUpOverlay' (UXML). Add 3 Buttons with class 'choice-button'.
2. Script Link: Link buttons in UI Toolkit to LevelUpManager.SelectUpgrade(index).
```

## [Milestone 15] 투사체 변이 물리 로직
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Update Assets/Scripts/Gameplay/Projectile.cs:
- Fields: [SerializeField] private bool _canPierce; [SerializeField] private int _bounceCount; [SerializeField] private bool _isExplosive;
- OnCollisionEnter:
  - If _isExplosive: Physics.OverlapSphere(pos, radius) -> deal damage.
  - If _bounceCount > 0: Calculate Vector3.Reflect(velocity, normal) and update direction.
  - Else: Release to pool via ObjectPoolManager.Instance.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/Physics]
1. Layer Assignment: Create layer 'Obstacle'. Assign to all boundary walls.
2. Effect: Create a simple 'ExplosionSphere' particle system for visual feedback.
```

## [Milestone 16] 특수 적 AI 구현
### Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic/AI]
1. Implement Assets/Scripts/Gameplay/BlitzEnemy.cs: Override Move logic with Sine wave offset (Z-axis).
2. Implement Assets/Scripts/Gameplay/SwarmPod.cs: On death, Get 3 'MiniEnemy' from pool.
3. Implement Assets/Scripts/Gameplay/HellfireEnemy.cs: Check distance to player; if < 2m, trigger explosion and release to pool.
```

## [Milestone 17] 카메라 쉐이크 연출
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Gameplay/ScreenShake.cs:
- Logic: Coroutine 'Shake' that takes duration and magnitude.
- Integration: Subscribe to EventBroker.OnEnemyKilled and EventBroker.OnHealthChanged to trigger camera offsets.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity]
1. Attach ScreenShake.cs to 'Main Camera'.
2. Set default shake values for different event types in the inspector.
```

## [Milestone 18] 피버 타임 시스템
### [Step 1] Jules 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Logic]
Implement Assets/Scripts/Gameplay/FeverManager.cs:
- Logic: Accumulate '_feverPoints' on enemy death. When full, trigger FeverMode (Duration: 5s).
- Effects: Double fire rate and score multiplier during active state.
```

### [Step 2] Antigravity 프롬프트
```text
[CONTEXT:AGENTS.md,SUMMARY.xml][TASK:Unity/VFX]
1. Update Global Volume: Increase Bloom intensity and add Chromatic Aberration during FeverMode.
2. UI: Add a 'fever-bar' (UI Toolkit ProgressBar) to the HUD and link to FeverManager.
```


