# [TARGET: Assets/Scripts/Gameplay/Flow/GameFlowController.cs] [TASK: 7.4]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.4` |
| **Agent Role** | `Jules/Antigravity (Logic & UI)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `GameFlowController`, `EventBroker`
- **Delta**: `docs/delta/7.3.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Gameplay/Flow/GameFlowController.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Flow Orchestration**: Implement `GameFlowController.cs` as a single-scene loop coordinator with states: `Lobby`, `SelectTurret`, `InGame`, `BossBattle`, `Overclock`, `Result`.
2. **Clean Separation of Concerns**: Keep state variables and loop triggers in `GameFlowController`, separate from database or save files. Use `EventBroker` to broadcast transitions.
3. **Settlement Data Pipeline**: Accumulate game metrics (kills, max waves, gems earned, survival time) into a data transfer object (DTO) and pass it to the UI Result controller. Ensure UI elements are loaded dynamically and cleaned up on return to Lobby.

### Verification Criteria (QA Perspective)
1. **State Flow Tests**: Write unit tests in `GameFlowControllerTests.cs` to trigger each transition event and assert the controller enters the correct state and broadcasts associated events.
2. **Memory Leak Verifications**: Verify event unsubscriptions occur properly on transition back to Lobby.

### Phase Constraints
- **RED Phase**: Write failing test in `tests/`.
- **GREEN Phase**: Implement code.
- **DOCUMENT Phase**: Update docs.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
