# Phase 3: 로그라이트 성장 및 투사체 진화 - AI Prompts

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

