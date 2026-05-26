# [TARGET: Assets/UI/Lobby/LobbyScreen.uxml] [TASK: 9.1]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.1` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `LobbyController`, `EventBroker`
- **Delta**: `docs/delta/8.6.json`

---

## Work Scope

**Target File**: `Assets/UI/Lobby/LobbyScreen.uxml`

### Technical Requirements (10-Year Expert Feedback)
1. **Lobby UI Layout**: Design the Lobby screen using UI Toolkit (`uxml`/`uss`). Flatten hierarchy to optimize rendering performance on mobile.
2. **Style Sheets (Visuals)**: Reusable styles must live in `LobbyScreen.uss`. Absolutely no inline styling is allowed in the `uxml` file.
3. **Decoupled Controller**: Wire events to `LobbyController.cs` which communicates with the game core solely via `EventBroker`.

### Verification Criteria (QA Perspective)
1. **UI Layout Assertions**: Write an EditMode test ensuring all required visual elements exist and are correctly bound.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
