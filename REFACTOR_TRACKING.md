# 🛠️ Project Refactor Tracking (REFACTOR_TRACKING.md)

This file tracks technical debt, deprecated fields, and pending refactors that cannot be completed in a single pass. Agents MUST check this file at the start of every task and resolve eligible items.

## Pending Refactors

### Unity Client (potop_client)

- [x] **[FIXED] Combat-Progression Flow**: Fixed mismatched event types between `EXPGem.cs` and `LevelingManager.cs`. `EnemyBase.cs` now correctly publishes `EnemyKilledEvent`.
- [x] **[FIXED] Architecture (Data/Items)**: `EXPGemData` (ScriptableObject) is defined inside `EXPGem.cs`. It should be moved to its own file in `Assets/Scripts/Data/Items/` to comply with the single-responsibility principle.
- [x] **[FIXED] Architecture (UI)**: `UpgradeSelectController.cs` uses `Object.FindFirstObjectByType<LevelingManager>()` in `Awake`. This should be replaced with an event-driven or inspector-based reference.
- [x] **[FIXED] Combat Integration**: `TurretShooter.cs` should be refactored to inherit from or use `WeaponBase` and `IFireStrategy` to align with the new modular weapon architecture.
- [ ] **Feature Gap (Weapons)**: `NovaWeapon` and `JuggernautWeapon` lack AoE, Pierce, and Knockback logic in the `Projectile` class (currently marked as TODO).
- [x] **[FIXED] Feedback Integration**: `CameraShakeController` now integrated via `CombatImpactEvent` published by `Projectile`.
- [x] **[FIXED] VFX Optimization**: `VFXTrigger` refactored to use Unity 6 `Awaitable` and explicit `ParticleSystem` reset before pooling.
- [ ] **Stability**: Investigate Unity MCP connection drops during Play Mode transitions.

---
*Note: Delete items from this list once they are fully resolved and verified.*
