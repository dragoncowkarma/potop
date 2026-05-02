# Phase 4: 전투 시스템 및 성장 아키텍처 - AI Prompts

## [Milestone 15 - Weapon System Design]
### 1. [Claude Opus 4.6] Modular Weapon Architecture & Interface Design
- **Task**: Design an extensible combat framework.
- **Action**:
  - Define `IWeapon` interface (Fire, Reload, StopFire).
  - Design `WeaponBase` abstract class: Handle common logic like Cooldowns and Ammo.
  - Support Property Modifiers: Design a system where `ProjectileSpeed`, `Damage`, and `ExplosionRadius` can be modified by external buffs.
  - Output: Provide a detailed C# structural skeleton or pseudo-code for Jules to implement.

---

## [Milestone 15, 16, 17 - Logic Implementation]
### 2. [Jules] [Parallel Execution] Weapon Implementation, XP System, and Projectile Mutations
> [!IMPORTANT]
> **[STRICT SCOPE]**: Implement the `Claude Opus` design exactly. Ensure `XPManager` and `WeaponSystem` are decoupled via `EventBroker`.

**[Scope A: Assets/Scripts/Gameplay/Weapons/][Task: Modular Weapon Logic]**
- Implement `WeaponBase.cs` and `TurretWeapon.cs` (Inheriting from `WeaponBase`).
- Refactor `TurretShooter.cs`: It should now hold an `IWeapon` reference and call `.Fire()` instead of handling its own instantiation logic.
- Support Fire Modes: `Single`, `Auto`, `Burst`.

**[Scope B: Assets/Scripts/Gameplay/Progression/][Task: Player XP & Leveling System]**
- Implement `LevelManager.cs`:
  - State: `int _currentXP`, `int _currentLevel`, `AnimationCurve _xpRequirementCurve`.
  - Logic: Subscribe to `EnemyKilledEvent`. On level up, publish `LevelUpEvent`.
  - UI Interaction: Trigger the Upgrade Menu display when `LevelUpEvent` is published.

**[Scope C: Assets/Scripts/Gameplay/Projectiles/][Task: Physics Mutation Logic]**
- Implement Mutation Scripts:
  - `PiercingProjectile.cs`: Use `Physics.RaycastAll` or internal `_hitsRemaining` counter.
  - `BouncingProjectile.cs`: Use `Vector3.Reflect` on `OnCollisionEnter`.
  - `ExplosiveProjectile.cs`: Use `Physics.OverlapSphere` to apply area damage.

---

## [Milestone 16 - Upgrade UI]
### 3. [Antigravity: Gemini 3.1 Pro] Upgrade Selection Menu & Controller
- **Task**: Implement the interactive upgrade interface.
- **Action**:
  - UXML: Create `UpgradeMenu.uxml` with 3 dynamic buttons (IDs: 'slot-0', 'slot-1', 'slot-2').
  - USS: Add hover effects and glassmorphism styling for a premium feel.
  - Implement `UpgradeUIController.cs`:
    - Logic: When `LevelUpEvent` is received, `Time.timeScale = 0`.
    - Content: Randomly pick 3 upgrades from a `List<UpgradeData>`.
    - Selection: Apply the chosen upgrade and set `Time.timeScale = 1`.

---

## [Phase 4 Validation - Combat Scalability]
### 4. [Gemini CLI] Progression & Physics Audit
- **Task**: Verify level scaling and mutation performance.
- **Action**:
  - `run_command`: Verify `Assets/Scripts/Gameplay/Projectiles/` contains exactly 3 mutation scripts.
  - `manage_scene action="validate"`: Check that `UpgradeMenu` is assigned to a `UIDocument` component in the UI hierarchy.
  - `read_console`: Monitor for `StackOverflowException` if multiple projectiles trigger simultaneous explosions.
  - Audit: `manage_physics action="get_settings"`: Ensure `gravity` is still disabled for all projectile variants.
