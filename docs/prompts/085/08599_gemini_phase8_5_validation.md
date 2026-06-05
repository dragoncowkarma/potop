# [TARGET: Phase 8.5 Validation] [TASK: 8.5.99]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.5.99` |
| **Assigned Agent** | `Gemini CLI` |
| **Primary LLM** | `gemini-3.1-pro-preview` |
| **Fallback LLM** | `gemini-3.1-flash-lite` for mechanical link/path checks only |
| **Agent Role** | `Senior Release Gate QA Engineer` |
| **Priority** | `High` |

---

## Model-Specific Operating Mode

This prompt is optimized for `gemini-3.1-pro-preview` because Phase 8.5 final validation requires contradiction detection, scope leakage review, and release-gate judgment.

- Start with mechanical checks: XML, JSON, prompt links, task dependencies, and referenced walkthrough paths.
- Use the fallback flash-lite model only for repetitive path/link enumeration if supported.
- Reserve the primary model for status integrity, evidence sufficiency, and Phase 9 leakage decisions.
- Report blockers as `Fail` with owner phase; do not silently convert them to warnings.
- Do not perform Unity scene/prefab modifications; validation may inspect logs and artifacts only.

---

## Context Links

Before validation, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, `docs/management/07_development_milestones.md`, `docs/management/08_5_phase8_5_expert_gap_closure.md`, and prompts `08501` through `08504`.

- **Required Evidence**: `docs/walkthroughs/8.5_evidence_ledger.md`, `docs/walkthroughs/8.5_readability_audit.md`, `docs/management/08_5_phase9_handoff_contract.md`
- **Phase 8 Evidence**: `docs/walkthroughs/8.6_walkthrough.md`
- **Output**: `docs/walkthroughs/8.5.99_walkthrough.md`

---

## Work Scope

**Target Files**:
- `docs/walkthroughs/8.5.99_walkthrough.md`
- `REFACTOR_TRACKING.md` only for unresolved issues discovered during validation

### Technical Requirements (10-Year Expert Feedback)
1. **Status Integrity Gate**: Verify Phase 8 and Phase 8.5 statuses agree across SUMMARY files, task JSON, walkthroughs, and Kanban output.
2. **Evidence Completeness Gate**: Confirm every Phase 8 gate has objective evidence or a clearly owned missing-evidence task.
3. **Readability Gate**: Confirm combat readability audit evidence exists and does not hide Phase 9 work.
4. **Contract Gate**: Confirm pre-Phase 9 handoff contracts exist and do not implement Phase 9 systems.
5. **Link Integrity Gate**: Verify all Phase 8.5 prompt links and referenced documents exist.
6. **Scope Leakage Gate**: Fail validation if Phase 8.5 implemented lobby UI, save, achievements, mobile input, ads, localization, store packaging, or backend systems.

### Verification Command
```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 8.5.99 --cmd "./UnityProject/run_tests_phase8_5_docs.sh"
```

### Required Evidence
1. Record XML validation results for `SUMMARY.xml` and `docs/SUMMARY.xml`.
2. Record link/path validation for all `docs/prompts/085/*.md` files.
3. Record whether `docs/agile/KANBAN.md` was regenerated from task JSON or left unchanged with a reason.
4. Record zero red errors and zero critical warnings if Unity validation was run.
5. List unresolved issues in `REFACTOR_TRACKING.md` with owner phase.
6. Include a final `Go / No-Go` recommendation for starting Phase 9.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
