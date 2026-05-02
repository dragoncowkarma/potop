# Phase 4: 전투 시스템 및 성장 아키텍처 - AI Prompts

### 1. [Claude Opus 4.6] Weapon Architecture Design
- Pre-flight: Review `docs/gdd/02_gameplay_mechanics.md`.
- Design: Define `IWeapon`, `WeaponBase`. Support fire modes (Single/Auto/Burst).
- Specs: Define property-based modifiers for speed/damage/fire-rate.

### 2. [Jules] [Parallel Execution] Combat & Progression
**[Scope A: Assets/Scripts/Gameplay/Weapons/][Task: WeaponLogic]**
- Pre-flight: Implement `Claude Opus` design.
- Create `WeaponBase.cs`, `TurretWeapon.cs`. Refactor `TurretShooter.cs`.

**[Scope B: Assets/Scripts/Gameplay/Progression/][Task: Leveling]**
- Pre-flight: Read `05_meta_and_progression.md`.
- Implement `LevelManager.cs`: XP tracking, LevelUp thresholds, choice triggers.

**[Scope C: Assets/Scripts/Gameplay/Projectiles/][Task: PhysicsMutation]**
- Pre-flight: Modify `Projectile.cs`.
- Implement: Pierce, Bounce, and Explosive logic.

---

### 3. [Antigravity: Gemini 3.1 Pro] Upgrade UI Toolkit
- Pre-flight: Verify scripts compiled.
- UI: Create `UpgradeMenu.uxml` (3 slots). Implement `UpgradeUIController.cs`.
- Action: Handle pause/resume and selection binding.

### 4. [Gemini CLI] Build & Linkage Audit
- Pre-flight: Ensure scene is saved.
- Audit: `read_console count="50"` (Check for refactoring errors).
- Validation: `manage_scene action="validate"` (Serialized link check).
