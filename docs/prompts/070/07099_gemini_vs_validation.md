# [TARGET: Multiple Files (Verification)] [TASK: 7.6]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.6` |
| **Agent Role** | `Gemini CLI (QA Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `All gameplay systems`
- **Delta**: `docs/delta/7.5.json`

---

## Work Scope

**Target File**: `Multiple Files`

### Technical Requirements (10-Year Expert Feedback)
1. **Integration Test Suite**: Execute comprehensive integration test validations verifying the entire Vertical Slice:
   `Lobby -> Selection -> In-game (15 mins time-jump) -> Boss Spawn -> 3-Phase transitions -> Boss defeat -> Overclock scaling -> Player death -> Result screen`.
2. **Performance Audits**: Log console logs, check memory spikes, and verify zero errors/warnings.
3. **Harness Verification Command**: Run the Unity CLI runner to verify all tests in EditMode and PlayMode.

### Verification Command
```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 7.6 --cmd "./UnityProject/run_tests.sh"
```

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
