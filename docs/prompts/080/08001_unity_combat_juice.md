# [TARGET: Assets/Scripts/Camera/CameraShakeController.cs] [TASK: 8.1]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.1` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `CameraShakeController`, `EventBroker`
- **Delta**: `docs/delta/7.6.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Camera/CameraShakeController.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Frame-Rate Independent Cinemachine Shake**: Implement screen shake using Cinemachine Multi-Channel Perlin. Control shake intensity and duration via code. Ensure calculation is frame-rate independent.
2. **Hitstop Mechanism**: Implement a hitstop (time freeze for a fraction of a second) on heavy combat impacts using `Time.timeScale` manipulations, ensuring UI animation loops remain unimpacted.

### Verification Criteria (QA Perspective)
1. **Time Scale Assertions**: Write tests to assert `Time.timeScale` returns to `1.0` after the designated hitstop duration.

### Phase Constraints
- **RED Phase**: Write failing tests.
- **GREEN Phase**: Implement code.
- **DOCUMENT Phase**: Update documentation.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
