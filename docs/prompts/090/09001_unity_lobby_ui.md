# [TARGET: Assets/UI/Lobby/LobbyScreen.uxml] [TASK: 9.1]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.1` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `High` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and the Phase 9 block in `docs/management/07_development_milestones.md`.

- **Map**: `docs/map.md` — Required symbols: `LobbyController`, `EventBroker`
- **Delta**: `docs/delta/8.6.json`
- **GDD**: `docs/requirements/05_meta_and_progression.md` — meta upgrades, monetization, game flow

---

## Work Scope

**Target Files**:
- `Assets/UI/Lobby/LobbyScreen.uxml`
- `Assets/UI/Lobby/LobbyScreen.uss`
- `Assets/Scripts/UI/LobbyController.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Launch-Ready Lobby Layout**: Complete turret selection, meta upgrade shop, Gem wallet, settings entry, achievements entry, daily reward entry, and game start flow using UI Toolkit.
2. **Mobile Hierarchy Discipline**: Keep UXML hierarchy shallow and avoid decorative nested containers that increase layout cost.
3. **USS-Only Styling**: Reusable visual rules must live in `LobbyScreen.uss`; inline UXML styles are prohibited.
4. **Safe-Area and Aspect Support**: Layout must handle mobile safe area, 16:9, 19.5:9, tablet aspect ratios, and desktop fallback without text overlap.
5. **EventBroker Boundary**: `LobbyController` may publish commands and subscribe to state events only. It must not reach directly into gameplay singletons except through existing project-approved services.
6. **Persistence Handoff**: Expose UI state points required by `09003` save/load: selected turret, purchased upgrades, settings volume, language, and first-run tutorial flag.
7. **No Store Coupling Yet**: Placeholder store buttons may exist, but payment/ad SDK calls belong to `09005` and `09009`.

### Verification Criteria (QA Perspective)
1. **UI Layout Assertions**: EditMode test ensures required visual element names exist and bind without null references.
2. **Safe-Area Snapshot Check**: Capture representative mobile and desktop resolutions; no clipped text or overlapping controls.
3. **Event Boundary Test**: Assert main actions publish expected EventBroker events without direct gameplay object lookups.
4. **Console Gate**: Loading lobby and returning from a completed run produce zero red errors.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
