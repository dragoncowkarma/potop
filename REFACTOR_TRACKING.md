# 🛠️ Project Refactor Tracking (REFACTOR_TRACKING.md)

This file tracks technical debt, deprecated fields, and pending refactors that cannot be completed in a single pass. Agents MUST check this file at the start of every task and resolve eligible items.

## Pending Refactors

### Unity Client (potop_client)

- [ ] **Combat Integration**: `TurretShooter.cs` should be refactored to inherit from or use `WeaponBase` and `IFireStrategy` to align with the new modular weapon architecture.
- [ ] **Feedback Integration**: `CameraShakeController` should be integrated into the combat loop (e.g., triggered via `WeaponBase` or `Projectile` on impact).
- [ ] **VFX Optimization**: `VFXTrigger` uses a coroutine for despawning; consider moving this logic into a dedicated `PooledObject` component or `PoolManager` to reduce coroutine overhead.
- [x] **Validation**: Phase 3 Gameplay Performance Audit completed. 
    - Findings: `PoolManager` efficiently recycles enemies; memory is stable.
    - Critical Issue: `WaveManager` and `FeverManager` were missing from the `MainScene`. Added them to the `Managers` GameObject.
    - Critical Issue: No `WaveData` assets found; created `TestWave01.asset` for validation.
- [ ] **Scene Persistence**: Ensure `MainScene.unity` is saved with the newly added `WaveManager` and `FeverManager` components.
- [x] **FeverManager Decoupling**: Refactored `FeverManager` to use EventBroker and separated combo logic into `ComboCalculator`.
- [ ] **Stability**: Investigate Unity MCP connection drops during Play Mode transitions.

---
*Note: Delete items from this list once they are fully resolved and verified.*
