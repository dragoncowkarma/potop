# [TARGET: Assets/Scripts/Input/MobileInputManager.cs] [TASK: 9.4]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.4` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `MobileInputManager`, `EventBroker`
- **Delta**: `docs/delta/9.3.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Input/MobileInputManager.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Virtual Joystick & Touch Controls**: Implement mobile controls via virtual joysticks and touch events.
2. **Auto-Fire Integration**: Implement auto-fire logic which scans target ranges and automatically fires when target alignment thresholds are met.
3. **Memory Safety**: Unsubscribe all touch events on disable or scene change.

### Verification Criteria (QA Perspective)
1. **Input Mock Tests**: Simulate touch events in tests and verify the calculated movement vector matches expectations.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
