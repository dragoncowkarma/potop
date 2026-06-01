# Phase 8+ Expert Review Feedback

> **Date:** 2026-06-01  
> **Scope:** Phase 8 Polish & Game Feel, Phase 9 Mobile Launch, Phase 10 handoff readiness  
> **Method:** 10-year expert review across design, Unity, audio, VFX, mobile, backend, and QA disciplines

## Executive Summary

Phase 8 and Phase 9 were structurally planned, but the previous prompts were too thin to guarantee launch-quality execution. The main risk was that agents could satisfy the task text while leaving unmeasured polish, subjective audio/VFX quality, fragile save data, late mobile input gaps, or incomplete store-readiness work.

This review upgrades Phase 8+ into measurable gates: GC allocation checks, TimeScale recovery, pooled audio/VFX evidence, balance simulation, save schema migration, ad callback idempotency, localization coverage, and full mobile launch loop validation.

## Expert Panel Findings

| Domain | Finding | Applied To |
| :--- | :--- | :--- |
| Technical Direction | Phase 8 polish must not write `Time.timeScale` from scattered systems. A central time controller is required. | `08001`, `08099` |
| Gameplay/Balance | Balance tuning needs a simulation matrix, not only manual playtest notes. Overclock scoring must match the GDD rule. | `08004`, `03_data_and_balance.md` |
| UI/UX | Mobile launch needs Safe Area, aspect ratio, and text-overlap checks before store packaging. | `09001`, `09008`, `09099` |
| Audio | SoundManager requires mixer routing, voice caps, data-driven clips, and no hot-path allocation. | `08002`, `06_art_and_sound.md` |
| VFX | Pooled particles must reset trails/sub-emitters and preserve boss telegraph readability. | `08003`, `06_art_and_sound.md` |
| Mobile Engineering | Input work must close the known rotation cap and keyboard fallback conflicts, not only add touch controls. | `09004`, `conflict_report.md` |
| Monetization/Compliance | Ads need provider isolation, consent/unavailable fallback, no-ads handling, and idempotent reward callbacks. | `09005`, `09009` |
| Save/Backend Readiness | Local save needs schema versioning and migration so Phase 10 cloud sync can be added without data loss. | `09003`, `05_meta_and_progression.md` |
| QA | Phase gates need explicit evidence files and console/log checks, not only a generic test command. | `08099`, `09099` |

## Phase 8 Gate

Phase 8 is accepted only when the following evidence exists:

| Gate | Required Evidence |
| :--- | :--- |
| Combat feel | TimeScale tests for hitstop, slow-motion, pause, and nested recovery |
| Audio | 0B GC hot-path evidence after pool warmup, voice cap tests, duplicate subscription tests |
| VFX | Pool return/replay tests, active particle budget check, readability screenshots |
| Balance | 4 turret x mutation x wave/boss/overclock simulation report |
| QA | `docs/walkthroughs/8.6_walkthrough.md` with test summary and console status |

## Phase 9 Gate

Phase 9 is accepted only when the following loop is validated:

```text
First Launch -> Tutorial -> Lobby -> In-Game -> Game Over/Rewarded Revive -> Settlement -> Lobby
```

| Gate | Required Evidence |
| :--- | :--- |
| Lobby | Safe Area/aspect checks and EventBroker-only UI boundary |
| Achievements | AC_001-AC_010 trigger tests, reward idempotency, snapshot round trip |
| Save | Atomic write, backup restore, migration, tamper-detection tests |
| Input | Mobile touch, auto-fire, 180deg/sec cap, keyboard fallback |
| Ads | Rewarded revive, failure/cancel, no-ads, interstitial cadence |
| Optimization | Draw calls <= 150 target, LOD dry-run report, mobile quality tier check |
| Tutorial | Skip/replay, saved completion flag, localization keys |
| Localization | KO/EN/JP coverage, runtime switching, layout screenshots |
| Store | AOT/IL2CPP preflight, release stripping, store checklist artifact |
| QA | `docs/walkthroughs/9.99_walkthrough.md` with device/simulator matrix and console status |

## Phase 10 Handoff

Phase 10 server work should not start until Phase 9.99 confirms:

1. Local save schema and migration format are stable.
2. Achievement snapshot format is stable.
3. Score/event log fields needed for leaderboard validation are documented.
4. Consent/no-ads/ad-state fields are represented in settings or entitlement data.
5. Store build versioning and release metadata are reproducible.
