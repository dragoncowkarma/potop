# [TARGET: Assets/Data/Balance/WeaponBalanceData.csv] [TASK: 8.4]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.4` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and `docs/requirements/03_data_and_balance.md`.

- **Map**: `docs/map.md` — Required symbols: `WeaponBase`, `EnemyBase`
- **Delta**: `docs/delta/8.3.json`
- **GDD**: `docs/requirements/02_gameplay_mechanics.md` — turret classes, level-up flow, overclock mode

---

## Work Scope

**Target Files**:
- `Assets/Data/Balance/WeaponBalanceData.csv`
- `Assets/Data/Balance/EnemyBalanceData.csv`
- Existing ScriptableObject balance import/editor utility files if present

### Technical Requirements (10-Year Expert Feedback)
1. **Data-Driven Calibration**: Define weapon damage, fire rate, pierce, AoE, enemy HP, enemy damage, speed, EXP, energy yield, and wave spawn weights in CSV or equivalent editable data.
2. **Editor-Time Import**: Parse CSV in editor/import tooling and write validated ScriptableObject fields. Runtime string parsing during combat is prohibited.
3. **Simulation Matrix**: Run a balance matrix across 4 turret classes, core mutation archetypes, wave phases 1-5, boss, and overclock entry.
4. **Fairness Targets**: No turret should fall more than 15% below the median simulated clear capability at 15 minutes unless explicitly justified by utility.
5. **Economy Targets**: Validate 15-minute energy income supports roughly EMP 4-6 uses, orbital strike 2-4 uses, and overload shield 1-2 uses under median play.
6. **Overclock Reward Alignment**: Use the GDD rule of `+0.2x` score multiplier per 30 seconds survived in overclock; update docs if implementation differs.
7. **Performance Constraints**: Balance changes must preserve max active enemies `200`, max active projectiles `500`, and max active VFX particles `10,000`.

### Verification Criteria (QA Perspective)
1. **Parser Assertions**: Verify every CSV row maps to the correct ScriptableObject field and invalid rows fail fast with actionable errors.
2. **Golden Data Test**: Include a tiny fixture CSV with known values and assert deterministic import output.
3. **Simulation Report**: Produce a short table of turret survival/DPS/energy outcomes and attach it to the task walkthrough or cycle log.
4. **Doc Sync**: Update `docs/requirements/03_data_and_balance.md` with final values and any changed formulas.
5. **Regression Gate**: Existing combat tests must still pass after balance data import.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
