# Phase 8.5 Expert Gap Closure

> **Scope:** Phase 1 through Phase 8 progress review and pre-Phase 9 gap closure  
> **Review Lens:** 10-year experts across game design, Unity client engineering, audio, VFX, UX, QA, production, and backend readiness  
> **Boundary:** Do not implement Phase 9+ launch features in this phase.

## Executive Verdict

Phase 1 through Phase 7.5 are documented as complete or approved, and the project has a verified vertical slice foundation: core loop, boss flow, overclock transition, GameFlow consolidation, HUD decomposition, assembly definitions, and documentation sync have all been represented in milestone, walkthrough, and task artifacts.

Phase 8 is not yet safe to treat as fully accepted from a production-management perspective. The evidence set contains strong completion claims, including `docs/walkthroughs/8.6_walkthrough.md`, but the source-of-truth files disagree: `docs/SUMMARY.xml` marks some Phase 8 prompts as planned and some completed, while `docs/tasks/8.1.json` through `docs/tasks/8.6.json` still report `Ready`.

Phase 8.5 exists to close that gap without stealing work from Phase 9. It verifies the Phase 8 quality bar, reconciles task state, packages evidence, audits readability, and documents handoff contracts that Phase 9 can consume.

## Progress Review Through Phase 8

| Area | Expert Read | Status |
| :--- | :--- | :--- |
| Core Loop / Design | Phase 7.6 validates the Lobby -> InGame -> Boss -> Overclock -> Result path. Phase 8 balance still needs evidence traceability beyond the current summarized report. | Strong foundation, evidence packaging needed |
| Client Architecture | Phase 7.5 removed major structural blockers, but `REFACTOR_TRACKING.md` still lists 8 singletons and partially polling HUD widgets. These are not Phase 8.5 blockers unless they affect evidence gates. | Accept with tracked debt |
| Time / Combat Feel | Phase 8 prompts require a central `TimeController`, nested hitstop recovery, unscaled UI, and camera shake determinism. Task state must be reconciled before completion is claimed. | Needs SSOT reconciliation |
| Audio | `06_art_and_sound.md` defines mixer groups, pooling, voice caps, cooldowns, and BGM states. Completion requires linked tests or profiler evidence, not only architecture text. | Needs evidence ledger |
| VFX | VFX budgets and reset rules are documented. Acceptance requires replay/pool-return evidence and readability screenshots for enemy death, boss phase, and overclock entry. | Needs visual evidence |
| Balance | `balance_simulation_report.md` exists, but it is too compact for final sign-off: it lacks seed/version, mutation matrix detail, boss/overclock rows, and failure-case notes. | Needs expanded simulation artifact |
| UX / Readability | Phase 8 should confirm combat readability: threat bands, boss telegraphs, hitstop visibility, warning audio, and HUD non-occlusion. Safe Area, localization, mobile input, and tutorial hardening remain Phase 9. | Needs narrow audit |
| QA / Production | Kanban is stale relative to walkthrough claims. Phase 8.5 must make task JSON, SUMMARY, walkthroughs, and generated boards agree. | Required before Phase 9 |
| Backend Readiness | Phase 10 server work is out of scope. Phase 8.5 may only document runtime payload expectations that Phase 9 save/achievement work will later stabilize. | Contract docs only |

## Phase 8.5 In Scope

1. Reconcile Phase 8 status across `docs/SUMMARY.xml`, `SUMMARY.xml`, `docs/tasks/*.json`, walkthroughs, and generated Kanban output.
2. Build a Phase 8 evidence ledger that links exact tests, profiler excerpts, screenshots, balance reports, and console/log status.
3. Expand or replace the balance report with matrix coverage for turret, mutation archetype, wave phase, boss, and overclock entry.
4. Audit combat readability for threat indicators, boss telegraphs, VFX opacity, hitstop/slow-motion feedback, and warning audio hierarchy.
5. Document pre-Phase 9 runtime handoff contracts: settlement payload, score/overclock fields, audio settings hooks, quality-tier hooks, and tutorial prototype boundaries.
6. Triage remaining `REFACTOR_TRACKING.md` entries into owner phases without resolving unrelated architecture debt.

## Phase 8.5 Out of Scope

- Lobby UI, upgrade shop, settings screens, or Safe Area redesign.
- Achievement implementation, save files, migration, encryption, or cloud-sync preparation beyond interface notes.
- Mobile input, joystick, gyro, auto-fire, rewarded ads, interstitial cadence, no-ads entitlement, or consent flows.
- Mobile performance implementation, LOD authoring, store packaging, localization tables, or tutorial hardening.
- Backend infrastructure, authentication, leaderboards, cloud saves, server score validation, or live operations.

## Phase 8.5 Prompt Set

| Prompt | Assigned Agent | Primary LLM | Reasoning | Purpose |
| :--- | :--- | :--- | :--- | :--- |
| `08501_code_phase8_ssot_reconciliation.md` | Codex | `codex-5.5` | `매우높음` | Reconcile Phase 8 status and remove documentation contradictions. |
| `08502_code_polish_evidence_pack.md` | Gemini CLI | `gemini-3.1-pro-preview` | High audit depth | Assemble objective Phase 8 evidence for time, audio, VFX, balance, and console gates. |
| `08503_unity_gamefeel_readability_audit.md` | Antigravity | `gemini 3.5` | `high` | Verify combat readability without implementing Phase 9 mobile/UI features. |
| `08504_code_phase9_contract_prep.md` | Codex | `codex-5.4` | `높음` | Document handoff contracts Phase 9 needs, without implementing Phase 9 systems. |
| `08599_gemini_phase8_5_validation.md` | Gemini CLI | `gemini-3.1-pro-preview` | Release-gate audit | Validate Phase 8.5 completion, link integrity, and no Phase 9 scope leakage. |

### Model Assignment Rationale

- **Codex 5.5 / 매우높음** is reserved for cross-document SSOT reconciliation where a single unsupported status edit can corrupt the roadmap.
- **Gemini CLI gemini-3.1-pro-preview** is reserved for evidence sufficiency and release-gate judgment where contradiction detection matters more than code generation speed.
- **Antigravity gemini 3.5 high** is reserved for Unity scene, VFX, HUD, and screenshot-based readability judgment.
- **Codex 5.4 / 높음** is used for bounded architecture contract writing where table accuracy and scope control matter more than broad exploration.

## Exit Criteria

Phase 8.5 is complete only when:

1. Phase 8 task status is consistent across task JSON, SUMMARY files, walkthroughs, and Kanban rendering.
2. `docs/walkthroughs/8.5.99_walkthrough.md` links all required evidence artifacts.
3. Any unsupported Phase 8 completion claim is downgraded or explicitly marked as pending evidence.
4. Remaining technical debt is either resolved in scope or left in `REFACTOR_TRACKING.md` with a future owner phase.
5. Phase 9 starts with clear input contracts and no unplanned implementation hidden inside Phase 8.5.
