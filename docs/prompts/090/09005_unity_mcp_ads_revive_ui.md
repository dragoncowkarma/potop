# [TARGET: Assets/Scripts/Core/Ads/IAdProvider.cs] [TASK: 9.5]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.5` |
| **Agent Role** | `Jules/Antigravity (Logic & UI)` |
| **Priority** | `Medium` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `IAdProvider`, `AdManager`
- **Delta**: `docs/delta/9.4.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Core/Ads/IAdProvider.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Ad Interface**: Define the abstract `IAdProvider` containing rewarded revive ads (1x limit) and interstitial methods.
2. **Lobby UI Integration**: Tie ad callback state results directly to the revive prompt UI without violating logical separation.

### Verification Criteria (QA Perspective)
1. **Ad Callback Verification**: Verify that the reward callback executes the revive command within tests when simulating successful ad playback.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
