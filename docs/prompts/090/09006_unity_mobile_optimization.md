# [TARGET: Assets/Scripts/Editor/LODGroupAssigner.cs] [TASK: 9.6]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.6` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `High` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and `docs/requirements/03_data_and_balance.md`.

- **Map**: `docs/map.md` — Required symbols: `LODGroupAssigner`
- **Delta**: `docs/delta/9.5.json`
- **Architecture**: `docs/architecture/04_technical_architecture.md` — rendering optimization constraints

---

## Work Scope

**Target Files**:
- `Assets/Scripts/Editor/LODGroupAssigner.cs`
- Mobile graphics quality settings or renderer profile assets if the project already keeps them in version control

### Technical Requirements (10-Year Expert Feedback)
1. **LOD Group Setup**: Implement an editor utility that scans eligible prefabs and configures LOD groups without modifying unrelated assets.
2. **Mobile Performance Budget**: Preserve GDD budgets: active enemies `200`, projectiles `500`, VFX particles `10,000`, and draw calls `150` or fewer in dense combat.
3. **Quality Tiers**: Provide Low/Medium/High mobile quality knobs for shadows, bloom, trails, particle density, and render scale.
4. **Batching and Instancing**: Verify SRP Batcher/GPU Instancing compatibility for repeated enemies, projectiles, and low-poly props.
5. **Thermal-Aware Defaults**: Prefer stable frame pacing over peak visuals. Do not enable expensive post-processing by default on mobile.
6. **Safe Editor Utility**: The assigner must support dry-run/report mode before writing prefab changes.
7. **No Runtime Asset Scans**: Heavy mesh/material scans must stay in editor tooling, not player startup.

### Verification Criteria (QA Perspective)
1. **LOD Assignment Check**: EditMode test loads representative prefabs and verifies LOD groups, thresholds, and renderers are configured.
2. **Dry-Run Report Check**: Utility produces a report listing changed/skipped assets before write mode.
3. **Performance Snapshot**: Record draw calls, batches, triangle count, and frame time in a dense combat scene.
4. **Quality Tier Check**: Switching quality tiers updates expected render scale/post-processing/particle density settings.
5. **Doc Sync**: Update `docs/requirements/03_data_and_balance.md` or `docs/architecture/04_technical_architecture.md` if performance budgets change.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
