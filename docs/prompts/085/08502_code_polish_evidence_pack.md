# [TARGET: Phase 8 Evidence Artifacts] [TASK: 8.5.2]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.5.2` |
| **Assigned Agent** | `Gemini CLI` |
| **Primary LLM** | `gemini-3.1-pro-preview` |
| **Fallback LLM** | `gemini-3-flash-preview` for path/link sweeps only |
| **Agent Role** | `Senior QA Evidence Auditor` |
| **Priority** | `High` |

---

## Model-Specific Operating Mode

This prompt is optimized for `gemini-3.1-pro-preview` because it must audit evidence quality rather than simply collect files.

- Use a strict `Pass / Fail / Missing Evidence` table for every Phase 8 gate.
- Use `gemini-3-flash-preview` only for mechanical file existence and link checks if the CLI supports subtask routing.
- Do not make Unity scene, prefab, material, or C# implementation changes.
- Prefer explicit evidence citations: file path, test name, command, log source, screenshot path, and owner task.
- If evidence is contradictory, record the contradiction and owner instead of resolving it by assumption.

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, Phase 8 prompts `08001` through `08099`, and `docs/management/08_5_phase8_5_expert_gap_closure.md`.

- **Current Evidence**: `docs/walkthroughs/8.6_walkthrough.md`
- **Balance Evidence**: `docs/walkthroughs/balance_simulation_report.md`
- **GDD**: `docs/requirements/02_gameplay_mechanics.md`, `docs/requirements/03_data_and_balance.md`
- **Architecture**: `docs/architecture/04_technical_architecture.md`, `docs/architecture/06_art_and_sound.md`

---

## Work Scope

**Target Files**:
- `docs/walkthroughs/8.5_evidence_ledger.md`
- `docs/walkthroughs/balance_simulation_report.md`
- `docs/walkthroughs/8.6_walkthrough.md` only if its claims need clarification
- Existing Unity test result or profiler artifact paths already used by the project

### Technical Requirements (10-Year Expert Feedback)
1. **Evidence Ledger**: Create a single Phase 8 ledger mapping each acceptance gate to source files, test names, commands, logs, screenshots, and owner.
2. **Time Integrity Evidence**: Link tests for hitstop, slow-motion, pause, nested recovery, and unscaled UI behavior.
3. **Audio Evidence**: Link pool warmup, 0B GC hot-path evidence, voice cap tests, mixer routing, cooldown behavior, and duplicate subscription tests.
4. **VFX Evidence**: Link pool return/replay tests, particle reset behavior, active particle budget, boss readability captures, and overclock entry captures.
5. **Balance Evidence**: Expand the report with version, seed or deterministic setup, turret x mutation x wave/boss/overclock matrix, fairness threshold, energy economy, and failure notes.
6. **Console Evidence**: Record Unity console status and identify the exact log source used to claim zero red errors or critical warnings.
7. **No New Phase 9 Systems**: Do not implement save, achievements, ads, localization, mobile input, or store packaging.

### Verification Criteria (QA Perspective)
1. Every Phase 8 gate has `Pass`, `Fail`, or `Missing Evidence` status.
2. Missing evidence is converted into a task note rather than hidden behind a generic completed label.
3. The balance report includes all 4 turrets and at least one boss and one overclock entry row.
4. The evidence ledger can be read without opening Unity and still explains the acceptance state.
5. Links in the ledger resolve to existing files.
6. The ledger distinguishes measured evidence from expert inference.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
