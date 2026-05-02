# Phase 4: 전투 시스템 및 성장 아키텍처 - AI Prompts

---

## [Milestone 15] 무기 시스템 아키텍처
### [Step 1] Claude Opus 4.6 프롬프트
```text
[TASK:Architecture/Design]
1. Design a modular Weapon System: IWeapon (Interface) and WeaponBase (Abstract).
2. Requirement: Support different fire modes (Single, Auto, Burst) and projectile types.
```

### [Step 2] Jules 프롬프트
```text
[SCOPE: Potop.Client.Gameplay.Weapons (Assets/Scripts/Gameplay/Weapons/)][TASK:Logic/Implementation]
1. Implement the modular weapon system designed by Claude Opus.
2. Refactor TurretShooter.cs to delegate firing logic to the active IWeapon.
```

---

## [Milestone 16] 레벨업 시스템 및 UI
### [Step 1] Jules 프롬프트
```text
[SCOPE: Potop.Client.Progression (Assets/Scripts/Progression/)][TASK:Logic/System]
1. Implement LevelManager.cs: XP tracking, LevelUp thresholds.
2. Upgrade Data: Create ScriptableObject for different player upgrades (e.g., +10% Damage).
```

### [Step 2] Gemini 3.1 Pro 프롬프트
```text
[TASK:Unity/UI Toolkit]
1. Create 'UpgradeUI' UXML with 3 random upgrade options.
2. Handle UI pause/resume and selection logic.
```

---

## [Milestone 17] 투사체 변이 물리 로직
### [Step 1] Jules 프롬프트
```text
[SCOPE: Potop.Client.Gameplay.Projectiles (Assets/Scripts/Gameplay/Projectiles/)][TASK:Logic/Physics]
1. Add physics modifiers to Projectile.cs:
   - Pierce (Ignore first collision).
   - Bounce (Reflect on walls).
   - Explosive (AOE damage on hit).
```
