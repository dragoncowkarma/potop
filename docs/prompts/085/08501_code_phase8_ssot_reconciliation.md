# [TARGET: Documentation SSOT] [TASK: 8.5.1]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.5.1` |
| **Assigned Agent** | `Codex` |
| **Primary LLM** | `codex-5.5` |
| **Reasoning Level** | `매우높음` |
| **Agent Role** | `Senior Documentation Integrity Engineer` |
| **Priority** | `High` |

---

## Model-Specific Operating Mode

This prompt is optimized for `codex-5.5` at `매우높음` reasoning because the task requires careful multi-file SSOT reconciliation and conservative edits.

- Prefer deterministic repository inspection with `rg`, `sed`, XML parsing, and JSON parsing.
- Build a contradiction table before editing any status field.
- Use minimal patches; do not rewrite generated documents by hand.
- Treat missing evidence as `Missing Evidence`, not as a reason to invent completion proof.
- Keep all edits inside the documentation and task metadata files listed below.

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, `docs/management/07_development_milestones.md`, and `docs/management/08_5_phase8_5_expert_gap_closure.md`.

- **Milestone**: Phase 8.5 SSOT reconciliation
- **Evidence**: `docs/walkthroughs/8.6_walkthrough.md`, `docs/walkthroughs/balance_simulation_report.md`
- **Task Source**: `docs/tasks/8.1.json` through `docs/tasks/8.6.json`
- **Generated Board**: `docs/agile/KANBAN.md` must not be edited manually

---

## Work Scope

**Target Files**:
- `SUMMARY.xml`
- `docs/SUMMARY.xml`
- `docs/tasks/8.1.json`
- `docs/tasks/8.2.json`
- `docs/tasks/8.3.json`
- `docs/tasks/8.4.json`
- `docs/tasks/8.6.json`
- `docs/management/07_development_milestones.md`
- `docs/agile/KANBAN.md` only through the approved render command, if available

### Technical Requirements (10-Year Expert Feedback)
1. **Build a Status Matrix**: Compare Phase 8 status in prompt metadata, task JSON, walkthroughs, SUMMARY files, and Kanban output.
2. **Evidence-First Status**: Do not promote any task to `Verified`, `Approved`, or `completed` unless a linked evidence artifact and exact verification result exist.
3. **Downgrade Unsupported Claims**: If a walkthrough claims completion but task JSON lacks verification metadata, mark the contradiction in the evidence ledger instead of silently accepting it.
4. **Regenerate, Do Not Hand-Edit**: If the Kanban board must change, use the project harness render command. Do not manually edit generated sections.
5. **Preserve Phase 9 Boundary**: Do not start Phase 9 work, edit Phase 9 prompts, or implement mobile launch features.
6. **Refactor Tracking**: Only remove `REFACTOR_TRACKING.md` entries that are actually resolved and verified during this task.

### Verification Criteria (QA Perspective)
1. `SUMMARY.xml` and `docs/SUMMARY.xml` parse as valid XML.
2. Every Phase 8 prompt path listed in SUMMARY files exists.
3. Every Phase 8 task JSON has a status that matches the accepted evidence state.
4. Kanban output, if regenerated, matches `docs/tasks/*.json`.
5. The final report lists unresolved contradictions with an owner task.
6. The walkthrough states which files were edited and why each status change is evidence-backed.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
