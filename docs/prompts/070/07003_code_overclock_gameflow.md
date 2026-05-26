# [TARGET: Assets/Scripts/Gameplay/Flow/OverclockMode.cs] [TASK: 7.3]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.3` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `WaveManager`, `EventBroker`, `OverclockData`
- **Delta**: `docs/delta/7.2.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Gameplay/Flow/OverclockMode.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Scaling Calculations**: Implement `OverclockMode.cs` managing infinite enemy scaling upon subscribing to `OnBossDefeated`.
2. **Frequency & Data Extraction**: Every 30 seconds (using frame-rate independent `Time.deltaTime` calculations), scale enemy stats (HP, speed, damage) by 1.5x. Extract scaling factors and base rates into `OverclockData.asset` ScriptableObject.
3. **Wave Integration**: Command `WaveManager` to enter continuous, rapid spawn mode with no wave pauses.

### Verification Criteria (QA Perspective)
1. **Calculations Unit Tests**: Write unit tests in `OverclockModeTests.cs` asserting:
   - Enemy stat multiplier updates exactly every 30s.
   - Verify scaling logic does not cause float overflow or compounding precision drift.

### Phase Constraints
- **RED Phase**: Write failing tests in `tests/`.
- **GREEN Phase**: Implement logic.
- **DOCUMENT Phase**: Update docs.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
