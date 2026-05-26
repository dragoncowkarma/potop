# [TARGET: Multiple Files (Validation)] [TASK: 8.6]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.6` |
| **Agent Role** | `Gemini CLI (QA Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `SoundManager`, `CameraShakeController`, `VFXTrigger`
- **Delta**: `docs/delta/8.4.json`

---

## Work Scope

**Target File**: `Multiple Files`

### Technical Requirements (10-Year Expert Feedback)
1. **Polishing Metrics Validation**: Audit gameplay feel metrics. Assert zero garbage collection allocations occur from audio/VFX triggers during active combat.
2. **Verify Event Integrity**: Verify all hitstops, shakes, and BGM loops play properly without interrupting core state transitions.

### Verification Command
```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 8.6 --cmd "./UnityProject/run_tests_polish.sh"
```

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
