# [TARGET: Assets/Scripts/Gameplay/Tutorial/TutorialFlowController.cs] [TASK: 9.7]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.7` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and the Phase 7 tutorial prototype prompt.

- **Map**: `docs/map.md` — Required symbols: `TutorialFlowController`, `EventBroker`
- **Delta**: `docs/delta/9.6.json`
- **Prior Work**: `docs/prompts/070/07005_code_tutorial_prototype.md`

---

## Work Scope

**Target Files**:
- `Assets/Scripts/Gameplay/Tutorial/TutorialFlowController.cs`
- Existing tutorial overlay UI files from Phase 7 if present
- Save flag integration points from `09003`

### Technical Requirements (10-Year Expert Feedback)
1. **Prototype Hardening**: Extend the Phase 7 tutorial prototype; do not rebuild a parallel tutorial system.
2. **Decoupled Step FSM**: Tutorial flow state controls triggers only. Overlay rendering and gameplay systems remain separate subscribers.
3. **Minimal Mandatory Steps**: Cover rotate/look, shoot/auto-fire, collect EXP, choose upgrade, use one tactical skill, and return to lobby.
4. **Flow Halts**: Gate progression between steps without abusing global pause or leaving TimeScale altered.
5. **Skip and Resume**: Support skip, replay from settings, and first-run completion saved through `09003`.
6. **Localization Ready**: All tutorial copy must use localization keys prepared for `09008`.
7. **Mobile Input Alignment**: Tutorial must validate mobile input and desktop fallback paths from `09004`.

### Verification Criteria (QA Perspective)
1. **Step Assertions**: Tests verify tutorial steps trigger, complete, and unlock in chronological sequence.
2. **Skip/Resume Test**: Skip saves completion flag; replay does not corrupt first-run state.
3. **TimeScale Test**: Tutorial gates do not leave the game paused or slowed after completion.
4. **Localization Key Test**: No tutorial visible string is hardcoded outside localization data.
5. **Input Coverage Test**: Simulated mobile and keyboard inputs can complete required steps.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
