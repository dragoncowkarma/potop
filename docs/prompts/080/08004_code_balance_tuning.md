# [TARGET: Assets/Data/Balance/WeaponBalanceData.csv] [TASK: 8.4]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.4` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `WeaponBase`, `EnemyBase`
- **Delta**: `docs/delta/8.3.json`

---

## Work Scope

**Target File**: `Assets/Data/Balance/WeaponBalanceData.csv`

### Technical Requirements (10-Year Expert Feedback)
1. **Data-driven Calibration**: Define weapon damage multipliers, fire rates, and enemy wave parameters in external CSV files.
2. **Parser Decoupling**: Implement a decoupled reader initializing balancing ScriptableObjects from the CSV rows without parsing strings at runtime.

### Verification Criteria (QA Perspective)
1. **Balance Parser Assertions**: Verify that the parser imports and maps CSV values to ScriptableObject fields accurately.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
