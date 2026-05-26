# [TARGET: Assets/Scripts/Core/Save/ISaveSystem.cs] [TASK: 9.3]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.3` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `ISaveSystem`, `EventBroker`
- **Delta**: `docs/delta/9.2.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Core/Save/ISaveSystem.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Save Interface**: Define `ISaveSystem` for saving, loading, and deletion.
2. **Local JSON Serialization**: Implement `LocalJSONSaveSystem.cs` writing game state to `Application.persistentDataPath` in JSON. Include a lightweight data verification/encryption step to protect save files.
3. **Auto-save Triggering**: Register auto-saving on important game loop transitions via `EventBroker`.

### Verification Criteria (QA Perspective)
1. **Save/Load Assertions**: Write unit tests loading mock data, serializing, saving to disk, reloading, and asserting data equality.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
