# [TARGET: Assets/Scripts/Core/Achievements/IAchievementSystem.cs] [TASK: 9.2]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.2` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and `docs/requirements/05_meta_and_progression.md`.

- **Map**: `docs/map.md` — Required symbols: `IAchievementSystem`, `EventBroker`
- **Delta**: `docs/delta/9.1.json`
- **Upcoming Dependency**: `09003_code_local_save.md` will persist achievement snapshots

---

## Work Scope

**Target Files**:
- `Assets/Scripts/Core/Achievements/IAchievementSystem.cs`
- `Assets/Scripts/Core/Achievements/AchievementManager.cs`
- Achievement data ScriptableObject or serializable definition file if already established

### Technical Requirements (10-Year Expert Feedback)
1. **Interface Abstraction**: Declare `IAchievementSystem` for registration, progress updates, unlock queries, reward claiming, and serializable snapshot export/import.
2. **Decoupled Achievement Manager**: Implement `AchievementManager` using EventBroker events only. Achievements `AC_001` through `AC_010` must match `docs/requirements/05_meta_and_progression.md`.
3. **No Disk Writes Here**: Do not implement file I/O in this task. Provide a snapshot contract for `09003` to persist.
4. **Reward Idempotency**: Gem rewards must be claimable once only, even if unlock events are replayed after load.
5. **Offline-First Design**: Achievement unlocks must not require server connectivity; Phase 10 can sync them later.
6. **Analytics Handoff**: Emit a lightweight achievement-unlocked event with ID and timestamp for later mobile analytics/server sync.

### Verification Criteria (QA Perspective)
1. **Achievement Mock Tests**: Simulate relevant EventBroker events and verify each achievement unlocks exactly once.
2. **Reward Idempotency Test**: Replay the same event and imported snapshot; Gem reward is not duplicated.
3. **Snapshot Test**: Export/import achievement state and assert progress, unlocked state, and claimed state survive round trip.
4. **Doc Sync**: If achievement IDs or conditions change, update `docs/requirements/05_meta_and_progression.md`.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
