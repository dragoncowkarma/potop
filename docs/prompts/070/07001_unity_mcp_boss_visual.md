# [TARGET: Assets/Prefabs/Enemies/TitanCore.prefab] [TASK: 7.1]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.1` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `ShieldRingRotator`, `EnemyBase`
- **Delta**: `docs/delta/none.json`

---

## Work Scope

**Target File**: `Assets/Prefabs/Enemies/TitanCore.prefab`

### Technical Requirements (10-Year Expert Feedback)
1. **Prefab Structure**: Construct `TitanCore.prefab` with hierarchy:
   `Root (EnemyBase, Animator) -> Body (MeshRenderer) -> ShieldRing (ShieldRingRotator) -> LaserEmitter (Transform) -> HitboxCollider (Collider)`. Body size must be scaled 5x larger than normal enemies.
2. **Material Allocation & Memory (Visuals)**: Create 3 URP Lit materials for Phase 1 (Blue), Phase 2 (Purple), and Phase 3 (Red) with emissive intensities. Changing materials at runtime must utilize `MaterialPropertyBlock` on the MeshRenderer, or Animator parameters driving the shader properties directly, to prevent generating garbage-collected material instances at runtime.
3. **Animator Configuration**: Setup an Animator Controller with states: `Idle`, `Phase1`, `Phase2`, `Phase3`, and parameters to trigger transitions.
4. **Shield Rotation**: Implement `ShieldRingRotator.cs` rotating at 120 RPM. Upon entering Phase 2, decouple and trigger particle effects.

### Verification Criteria (QA Perspective)
1. **EditMode Prefab Verification**: Write an EditMode test in `TitanCorePrefabTests.cs` that loads the prefab from `Assets/Prefabs/Enemies/TitanCore.prefab` and asserts:
   - The Root object has `EnemyBase` and `Animator` components.
   - Child components `ShieldRing` and `LaserEmitter` exist.
   - The Root collider is active.

### Phase Constraints
- **RED Phase**: If task_id ends in `-RED`, you are assigned as `QA` and may only edit files in `tests/`.
- **GREEN Phase**: If task_id ends in `-GREEN`, you are assigned as `DEV` and may modify production and test files to satisfy the tests.
- **DOCUMENT Phase**: If you are assigned as `DOC`, you may ONLY update files in `docs/`.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
