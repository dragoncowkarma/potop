# [TARGET: Assets/Scripts/VFX/VFXTrigger.cs] [TASK: 8.3]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.3` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `PoolManager`, `VFXTrigger`
- **Delta**: `docs/delta/8.2.json`

---

## Work Scope

**Target File**: `Assets/Scripts/VFX/VFXTrigger.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Recyclable VFX**: Implement `VFXTrigger.cs` linking all particle systems and dynamic visuals to the `PoolManager`.
2. **Emission Memory Cleanups**: Ensure that returning a particle system to the pool completely resets its particle emissions, trails, and sub-emitters to avoid visual artifacts.

### Verification Criteria (QA Perspective)
1. **VFX Despawn Tests**: Write unit tests to verify that VFX objects return to the pool after their designated particle lifetime completes.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
