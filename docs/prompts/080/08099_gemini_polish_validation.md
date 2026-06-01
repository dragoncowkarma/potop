# [TARGET: Multiple Files (Validation)] [TASK: 8.6]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.6` |
| **Agent Role** | `Gemini CLI (QA Engineer)` |
| **Priority** | `High` |

---

## Context Links

Before validation, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and Phase 8 prompts `08001` through `08004`.

- **Map**: `docs/map.md` — Required symbols: `SoundManager`, `CameraShakeController`, `VFXTrigger`
- **Delta**: `docs/delta/8.4.json`
- **Docs**: `docs/requirements/02_gameplay_mechanics.md`, `docs/requirements/03_data_and_balance.md`, `docs/architecture/06_art_and_sound.md`

---

## Work Scope

**Target File**: `Multiple Files`

### Technical Requirements (10-Year Expert Feedback)
1. **Polish Metrics Validation**: Audit hitstop, shake, audio, VFX, and balance against measurable thresholds rather than subjective feel only.
2. **GC and Pooling Gate**: Assert 0B GC allocations on warmed audio/VFX hot paths and verify pooled objects return cleanly.
3. **Time Integrity Gate**: Verify hitstop, slow-motion level-up, pause, boss transition, and overclock entry do not leave `Time.timeScale` in a bad state.
4. **Event Integrity Gate**: Verify EventBroker subscriptions do not duplicate after scene reload and do not interrupt GameFlow state transitions.
5. **Balance Gate**: Confirm Phase 8 balance report exists, includes all 4 turrets, and records overclock score multiplier behavior.
6. **Console Gate**: Unity console and logs must show zero red errors and zero critical warnings.

### Verification Command
```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 8.6 --cmd "./UnityProject/run_tests_polish.sh"
```

### Required Evidence
1. Record Unity test summary and console status in `docs/walkthroughs/8.6_walkthrough.md`.
2. Include profiler excerpts or summarized allocation evidence for audio and VFX hot paths.
3. List any unresolved issue in `REFACTOR_TRACKING.md` with an owner phase.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
