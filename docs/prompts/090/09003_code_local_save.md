# [TARGET: Assets/Scripts/Core/Save/ISaveSystem.cs] [TASK: 9.3]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.3` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `High` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and Phase 9 prompts `09001` and `09002`.

- **Map**: `docs/map.md` — Required symbols: `ISaveSystem`, `EventBroker`
- **Delta**: `docs/delta/9.2.json`
- **GDD**: `docs/requirements/05_meta_and_progression.md` — meta upgrades and overall game flow

---

## Work Scope

**Target Files**:
- `Assets/Scripts/Core/Save/ISaveSystem.cs`
- `Assets/Scripts/Core/Save/LocalJSONSaveSystem.cs`
- Save data models for meta upgrades, achievements, settings, selected turret, and tutorial flags

### Technical Requirements (10-Year Expert Feedback)
1. **Save Interface**: Define async-safe `ISaveSystem` methods for load, save, delete, existence check, backup restore, and schema migration.
2. **Versioned Save Schema**: Store `schemaVersion`, `appVersion`, timestamp, selected turret, Gem balance, meta upgrades, achievements snapshot, settings, language, and tutorial completion flags.
3. **Atomic Local JSON**: Write to a temp file, validate, then replace the active save to avoid corruption on app suspend.
4. **Integrity Not Security Theater**: Add checksum/HMAC-style verification for tamper detection. Do not describe this as strong encryption unless a real key-management strategy exists.
5. **Auto-Save Events**: Save on game settlement, upgrade purchase, achievement reward claim, settings change, app pause, and app quit.
6. **Migration Path**: Include a migration handler from empty/no-save and at least one older schema fixture.
7. **PlayerPrefs Cleanup**: Do not rely on PlayerPrefs for critical progression after this system is active.

### Verification Criteria (QA Perspective)
1. **Save/Load Assertions**: Save mock data to a temporary path, reload, and assert equality for all critical fields.
2. **Atomic Write Test**: Simulate interrupted write and verify backup or previous valid save remains loadable.
3. **Migration Test**: Load an older fixture and assert migrated data matches current schema.
4. **Tamper Test**: Modify saved JSON and assert verification catches the mismatch without crashing.
5. **Event Trigger Test**: Publish key EventBroker events and assert save requests are coalesced rather than spammed.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
