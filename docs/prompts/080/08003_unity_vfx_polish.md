# [TARGET: Assets/Scripts/VFX/VFXTrigger.cs] [TASK: 8.3]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.3` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and `docs/architecture/06_art_and_sound.md`.

- **Map**: `docs/map.md` — Required symbols: `PoolManager`, `VFXTrigger`
- **Delta**: `docs/delta/8.2.json`
- **GDD**: `docs/requirements/03_data_and_balance.md` — performance constraints

---

## Work Scope

**Target Files**:
- `Assets/Scripts/VFX/VFXTrigger.cs`
- `Assets/Scripts/VFX/ExplosionEffect.cs`
- Existing pooled VFX prefabs referenced by combat, boss phase, and UI transition events

### Technical Requirements (10-Year Expert Feedback)
1. **Recyclable VFX**: Link combat hits, enemy deaths, boss phase shifts, and UI transition VFX to `PoolManager`; runtime `Instantiate` in hot combat paths is prohibited after warmup.
2. **Complete Particle Reset**: On pool return, clear particles, trails, sub-emitters, light modules, decals, and any transient material property blocks that would leak visual state.
3. **Budgeted Spectacle**: Respect the GDD constraints: max active enemies `200`, max projectiles `500`, and max active VFX particles `10,000`. Favor GPU particles for lingering debris.
4. **Boss Readability**: Boss phase transition VFX must communicate Phase 1/2/3 state changes without obscuring telegraphs or player HUD.
5. **Mobile Fallback**: Provide a lower-cost fallback or quality flag for expensive bloom/trail/explosion variants used by Phase 9 mobile optimization.
6. **Asset Naming**: New VFX assets must follow `docs/architecture/06_art_and_sound.md` naming rules.

### Verification Criteria (QA Perspective)
1. **VFX Despawn Tests**: Verify VFX objects return to the pool after their configured lifetime and can be replayed without stale particles/trails.
2. **Allocation Gate**: Trigger 100 enemy death VFX in a stress scene and assert no post-warmup GC allocations on the spawn/return path.
3. **Budget Gate**: Profiling evidence shows active particle count stays at or below `10,000` during dense combat.
4. **Readability Review**: Capture before/after screenshots for normal enemy death, boss phase transition, and overclock entry; HUD and telegraphs must remain visible.
5. **Documentation Sync**: Update `docs/architecture/06_art_and_sound.md` if VFX quality tiers, naming, or budgets change.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
