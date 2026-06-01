# [TARGET: Assets/Scripts/Core/Ads/IAdProvider.cs] [TASK: 9.5]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.5` |
| **Agent Role** | `Jules/Antigravity (Logic & UI)` |
| **Priority** | `Medium` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and `docs/requirements/05_meta_and_progression.md`.

- **Map**: `docs/map.md` — Required symbols: `IAdProvider`, `AdManager`
- **Delta**: `docs/delta/9.4.json`
- **Related Systems**: `GameFlowController`, revive prompt UI, local save/settings, lobby settings

---

## Work Scope

**Target Files**:
- `Assets/Scripts/Core/Ads/IAdProvider.cs`
- `Assets/Scripts/Core/Ads/AdManager.cs`
- Revive prompt UI/controller files if already present

### Technical Requirements (10-Year Expert Feedback)
1. **Ad Interface**: Define `IAdProvider` for availability, load, show rewarded revive, show interstitial, callbacks, and failure reasons.
2. **Provider Isolation**: `AdManager` must depend on `IAdProvider`; gameplay and UI must not call a concrete SDK directly.
3. **Rewarded Revive Rule**: Rewarded revive is limited to 1 use per run and returns the player through GameFlow events, not direct scene mutation.
4. **Interstitial Cadence**: Interstitial ads follow the GDD cadence of every 3-5 lobby returns and must be disabled by the no-ads entitlement when present.
5. **Consent and Availability Guard**: If consent is missing, ads are unavailable, or provider errors, show a non-blocking fallback path. Do not trap the player.
6. **Callback Safety**: Reward callbacks must be idempotent; duplicate SDK callbacks cannot revive twice or grant duplicate rewards.
7. **Test Provider**: Include a fake/null provider for editor tests and offline QA.

### Verification Criteria (QA Perspective)
1. **Ad Callback Verification**: Simulate successful rewarded playback and assert exactly one revive command is published.
2. **Duplicate Callback Test**: Fire success twice and assert the second callback is ignored.
3. **Failure Path Test**: Simulate unavailable/failure/cancel states and assert gameplay remains in a valid state.
4. **Cadence Test**: Verify interstitial cadence respects the 3-5 run interval and no-ads entitlement.
5. **UI Separation Test**: Revive prompt reacts to ad state events without depending on a concrete ad SDK class.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
