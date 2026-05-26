# [TARGET: Assets/Scripts/Core/Achievements/IAchievementSystem.cs] [TASK: 9.2]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.2` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `IAchievementSystem`, `EventBroker`
- **Delta**: `docs/delta/9.1.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Core/Achievements/IAchievementSystem.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Interface Abstraction**: Declare the `IAchievementSystem` interface containing registration, progress updates, and unlock listeners.
2. **Decoupled Achievement Manager**: Implement `AchievementManager.cs` inheriting `IAchievementSystem`. Achievements (AC_001 ~ AC_010) must trigger dynamically from events published on `EventBroker` (e.g., BossDefeated).

### Verification Criteria (QA Perspective)
1. **Achievement Mock Tests**: Write unit tests with a mock achievement implementation, verifying achievements unlock exactly when corresponding events are fired.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
