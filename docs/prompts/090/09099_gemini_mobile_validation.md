# [TARGET: Multiple Files (Validation)] [TASK: 9.99]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.99` |
| **Agent Role** | `Gemini CLI (QA Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `All mobile modules`
- **Delta**: `docs/delta/9.9.json`

---

## Work Scope

**Target File**: `Multiple Files`

### Technical Requirements (10-Year Expert Feedback)
1. **Mobile Verification**: Validate battery temperature profiles, draw calls, and layout scaling adjustments.
2. **Lobby loop testing**: Perform automated Lobby -> In-Game transition loops.

### Verification Command
```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 9.99 --cmd "./UnityProject/run_tests_mobile.sh"
```

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
