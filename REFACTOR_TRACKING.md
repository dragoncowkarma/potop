# 🛠️ Project Refactor Tracking (REFACTOR_TRACKING.md)

This file tracks technical debt, deprecated fields, and pending refactors that cannot be completed in a single pass. Agents MUST check this file at the start of every task and resolve eligible items.

## Pending Refactors

### Unity Client (potop_client)

- [x] **EnemyBase FSM**: Refactored EnemyBase to use a pure FSM pattern and time-sliced rotation. (Phase 3.5 QA Verified: Transitions Chase->Attack->Death confirmed).
- [ ] **Combat Integration**: `TurretShooter.cs` should be refactored to inherit from or use `WeaponBase` and `IFireStrategy` to align with the new modular weapon architecture.
- [ ] **Feedback Integration**: `CameraShakeController` should be integrated into the combat loop (e.g., triggered via `WeaponBase` or `Projectile` on impact).
- [ ] **VFX Optimization**: `VFXTrigger` uses a coroutine for despawning; consider moving this logic into a dedicated `PooledObject` component or `PoolManager` to reduce coroutine overhead.
- [x] **Validation**: Phase 3 & 3.5 Gameplay Performance Audit completed. 
    - Findings: `PoolManager` efficiently recycles enemies (verified 44 objects, 12 inactive); memory is stable (~750MB GC).
    - Fixed: `WaveManager` and `FeverManager` presence on `Managers` GameObject verified.
    - Fixed: `ComboCalculator` was missing from scene; added to `Managers`.
    - Fixed: `WaveData` assignment to `WaveManager` persistent in `MainScene`.
    - Fixed: `EnemyBase` NRE in `OnEnable` due to potential early access to `StateMachine`.
    - Fixed: `ComboCalculator` logic bug where it used `BaseScore` instead of `ScoreValue`.
- [x] **Scene Persistence**: Ensure `MainScene.unity` is saved with the newly added `WaveManager`, `FeverManager`, and `ComboCalculator` components. (Phase 3.5 QA Verified: Scene GUIDs confirmed on disk).
- [x] **FeverManager Decoupling**: Refactored `FeverManager` to use EventBroker and separated combo logic into `ComboCalculator`. (Phase 3.5 QA Verified: Event flow from EnemyDied -> ComboChanged -> FeverManager confirmed).
- [ ] **Stability**: Investigate Unity MCP connection drops during Play Mode transitions.

---
*Note: Delete items from this list once they are fully resolved and verified.*
