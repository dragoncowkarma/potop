# [TARGET: Multiple Files (Validation)] [TASK: 9.99]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.99` |
| **Agent Role** | `Gemini CLI (QA Engineer)` |
| **Priority** | `High` |

---

## Context Links

Before validation, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and Phase 9 prompts `09001` through `09009`.

- **Map**: `docs/map.md` — Required symbols: `All mobile modules`
- **Delta**: `docs/delta/9.9.json`
- **Docs**: `docs/requirements/02_gameplay_mechanics.md`, `docs/requirements/05_meta_and_progression.md`, `docs/conflict_report.md`

---

## Work Scope

**Target File**: `Multiple Files`

### Technical Requirements (10-Year Expert Feedback)
1. **Mobile Verification**: Validate battery, thermal, frame pacing, draw calls, memory, layout scaling, and input responsiveness.
2. **Full Launch Loop Testing**: Perform automated First Launch -> Tutorial -> Lobby -> In-Game -> Game Over/Revive -> Settlement -> Lobby loops.
3. **Persistence Gate**: Verify save/load, migration, achievement reward idempotency, selected turret, settings, language, and tutorial flags.
4. **Ads Gate**: Verify rewarded revive, failure/cancel paths, interstitial cadence, no-ads entitlement, and release fake-provider exclusion.
5. **Localization Gate**: Verify KO/EN/JP coverage and screenshots for the most text-heavy mobile screens.
6. **Input Gate**: Verify mobile touch controls, auto-fire, rotation cap, and keyboard fallback conflict-report items.
7. **Store Readiness Gate**: Verify build/package checklist and release build logs.
8. **Console Gate**: Unity console and logs must show zero red errors and zero critical warnings.

### Verification Command
```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 9.99 --cmd "./UnityProject/run_tests_mobile.sh"
```

### Required Evidence
1. Record device/simulator matrix, test summary, console status, and build output paths in `docs/walkthroughs/9.99_walkthrough.md`.
2. Include screenshots or summarized layout checks for lobby, tutorial, revive prompt, settings, and settlement in KO/EN/JP where feasible.
3. If any gate cannot be completed, add a tracked item to `REFACTOR_TRACKING.md` with owner phase and mitigation.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
