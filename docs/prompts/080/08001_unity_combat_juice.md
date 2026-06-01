# [TARGET: Assets/Scripts/Camera/CameraShakeController.cs, Assets/Scripts/Gameplay/Flow/TimeController.cs] [TASK: 8.1]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.1` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `High` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and the Phase 8 block in `docs/management/07_development_milestones.md`.

- **Map**: `docs/map.md` — Required symbols: `CameraShakeController`, `EventBroker`
- **Delta**: `docs/delta/7.6.json`
- **GDD**: `docs/requirements/02_gameplay_mechanics.md` — Threat Indicator, HUD tiers, slow-motion level-up flow
- **Architecture**: `docs/architecture/04_technical_architecture.md` — EventBroker and pooling constraints

---

## Work Scope

**Target Files**:
- `Assets/Scripts/Camera/CameraShakeController.cs`
- `Assets/Scripts/Gameplay/Flow/TimeController.cs`
- `Assets/Scripts/UI/HUD/ThreatIndicatorWidget.cs` if the Phase 7.5 HUD split already contains a compatible widget shell

### Technical Requirements (10-Year Expert Feedback)
1. **Centralized Time Control**: Route hitstop and slow-motion through `TimeController`; direct `Time.timeScale` writes outside this controller are prohibited.
2. **Nested Time Effects**: Hitstop, level-up slow-motion (`0.1x`, 5 seconds), and game pause must restore the previous effective time scale without losing state.
3. **Frame-Rate Independent Shake**: Drive Cinemachine Multi-Channel Perlin amplitude/frequency using unscaled time so 30/60/120 FPS captures decay consistently.
4. **Combat Feel Presets**: Move hitstop duration, shake amplitude, shake frequency, and cooldown values into serialized config or constants. Do not leave literal timing numbers inside event handlers.
5. **Threat Indicator Hook**: If the HUD widget exists, connect Yellow/Orange/Red distance bands (`15m`, `8m`, `3m`) through EventBroker without per-frame allocations.
6. **UI Safety**: UI Toolkit animations, cooldown overlays, and audio must keep using unscaled time during hitstop.
7. **No Visual Spam**: Coalesce repeated shake/hitstop events inside a short cooldown window so rapid-fire weapons do not strobe or lock the game.

### Verification Criteria (QA Perspective)
1. **Time Scale Assertions**: EditMode/PlayMode tests assert `Time.timeScale` returns to `1.0` after hitstop, after level-up slow-motion, and after nested hitstop during slow-motion.
2. **Unscaled UI Assertion**: Verify an unscaled UI timer continues while hitstop is active.
3. **Shake Determinism Check**: Capture shake intensity samples at two simulated frame rates and verify final amplitude variance stays within 10%.
4. **Threat Indicator Check**: Unit-test direction and distance band mapping for the `15m/8m/3m` thresholds.
5. **Console Gate**: Unity console contains zero red errors and zero critical warnings after entering combat, triggering heavy hit, level-up, and boss phase events.

### Phase Constraints
- **RED Phase**: Write failing tests.
- **GREEN Phase**: Implement code.
- **DOCUMENT Phase**: Update `docs/requirements/02_gameplay_mechanics.md` if thresholds or HUD behavior change.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
