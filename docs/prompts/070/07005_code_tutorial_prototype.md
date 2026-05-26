# [TARGET: Assets/Scripts/Gameplay/Tutorial/TutorialSystem.cs] [TASK: 7.5]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.5` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `TutorialSystem`, `EventBroker`
- **Delta**: `docs/delta/7.4.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Gameplay/Tutorial/TutorialSystem.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Tutorial Scene**: Implement a basic tutorial sequence including Look/Shoot guides.
2. **Interface Isolation**: Freeze main loop logic during look tutorial. UI indicators must use UI Toolkit elements decoupled from physics.
3. **Spawn Controls**: Spawn a single training enemy with custom weak stats.

### Verification Criteria (QA Perspective)
1. **Step Transition Unit Tests**: Write unit tests in `TutorialSystemTests.cs` to verify step progression (Look Complete -> Shoot Complete) occurs upon correct events.

### Phase Constraints
- **RED Phase**: Test validations first.
- **GREEN Phase**: Core implementation.
- **DOCUMENT Phase**: Documentation updates.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
