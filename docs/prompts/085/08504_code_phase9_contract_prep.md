# [TARGET: Pre-Phase 9 Runtime Contracts] [TASK: 8.5.4]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.5.4` |
| **Assigned Agent** | `Codex` |
| **Primary LLM** | `codex-5.4` |
| **Reasoning Level** | `높음` |
| **Agent Role** | `Senior Architecture Contract Writer` |
| **Priority** | `Medium` |

---

## Model-Specific Operating Mode

This prompt is optimized for `codex-5.4` at `높음` reasoning because the work is a bounded architecture contract, not broad implementation.

- Read Phase 9 prompts only to identify expected inputs and handoff fields.
- Produce compact contract tables with owner, field, type expectation, source system, and consuming Phase 9 prompt.
- Avoid speculative server API design; Phase 10 awareness is limited to future field needs.
- Do not edit C# files unless an existing contract document is stale and cannot be corrected in docs.
- Keep the document actionable for the next agent without creating Phase 9 systems.

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, `docs/management/07_development_milestones.md`, Phase 9 prompts `09001` through `09099`, and `docs/management/08_5_phase8_5_expert_gap_closure.md`.

- **Architecture**: `docs/architecture/04_technical_architecture.md`
- **Progression**: `docs/requirements/05_meta_and_progression.md`
- **Audio/VFX Hooks**: `docs/architecture/06_art_and_sound.md`
- **Output Contract**: `docs/management/08_5_phase9_handoff_contract.md`

---

## Work Scope

**Target Files**:
- `docs/management/08_5_phase9_handoff_contract.md`
- `docs/architecture/04_technical_architecture.md` only if EventBroker contracts are missing or stale
- `docs/requirements/05_meta_and_progression.md` only if settlement/meta fields are undocumented
- `docs/architecture/06_art_and_sound.md` only if audio or quality hooks are undocumented

### Technical Requirements (10-Year Expert Feedback)
1. **Document, Do Not Implement**: Define contracts Phase 9 will consume, but do not create save files, achievements, ads, localization tables, mobile input, or store packaging.
2. **Settlement Payload**: Document fields needed for result, score, overclock time, multiplier, kills, run duration, selected turret, earned gems, and tactical skill usage.
3. **Settings Hooks**: Document audio volume categories, quality-tier knobs, and warning cue categories exposed by Phase 8 systems.
4. **Tutorial Prototype Boundary**: Document what Phase 7 tutorial prototype already provides and what Phase 9 must harden later.
5. **EventBroker Contracts**: List event names, payload owners, and expected idempotency rules for result, reward, audio, VFX, and game-flow handoff.
6. **Future Backend Awareness**: Note fields likely needed by Phase 10 leaderboards, but avoid server API or cloud-save implementation.

### Verification Criteria (QA Perspective)
1. The handoff contract maps every Phase 9 prompt to the Phase 8/8.5 data it depends on.
2. Each contract field has owner, type expectation, and source system.
3. Phase 9 out-of-scope systems are explicitly listed as not implemented.
4. No code files are changed unless the project already contains a documented contract file that must be corrected.
5. Links to Phase 9 prompts resolve.
6. Contract language uses `MUST`, `SHOULD`, and `OUT OF SCOPE` consistently.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
