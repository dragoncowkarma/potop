# [TARGET: Phase 8 Combat Readability] [TASK: 8.5.3]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.5.3` |
| **Assigned Agent** | `Antigravity` |
| **Primary LLM** | `gemini 3.5` |
| **Reasoning Level** | `high` |
| **Fallback LLM** | `sonnet 4.6 (thinking)` for text-only remediation notes if Unity access is blocked |
| **Agent Role** | `Senior Unity Gamefeel and Readability Engineer` |
| **Priority** | `Medium` |

---

## Model-Specific Operating Mode

This prompt is optimized for Antigravity with `gemini 3.5 high` because the task depends on Unity scene inspection, visual hierarchy awareness, and screenshot-based readability judgment.

- Use Unity/MCP inspection and screenshots before recommending asset or code changes.
- Keep the response visual and concrete: object name, material/VFX name, screenshot path, observed failure, and fix.
- Prefer small Unity-side tuning only when a Phase 8 readability defect is directly observed.
- Do not broaden into Phase 9 mobile UI, Safe Area, localization, joystick, store, or monetization work.
- If Unity MCP is unstable, switch to the fallback model for a text-only audit plan and log the blocker.

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, `docs/requirements/02_gameplay_mechanics.md`, `docs/architecture/06_art_and_sound.md`, and `docs/management/08_5_phase8_5_expert_gap_closure.md`.

- **Related Phase 8 Prompts**: `08001_unity_combat_juice.md`, `08003_unity_vfx_polish.md`
- **Evidence Target**: `docs/walkthroughs/8.5_readability_audit.md`
- **Boundary**: This is a combat readability audit, not Phase 9 mobile UI implementation

---

## Work Scope

**Target Files**:
- `potop_client/Assets/Scripts/UI/HUD/ThreatIndicatorWidget.cs` only if a direct Phase 8 defect is found
- `potop_client/Assets/Scripts/Camera/CameraShakeController.cs` only if shake readability violates Phase 8 gates
- Existing VFX prefab/material settings used by enemy death, boss phase, and overclock entry
- `docs/walkthroughs/8.5_readability_audit.md`

### Technical Requirements (10-Year Expert Feedback)
1. **Threat Band Readability**: Verify Yellow/Orange/Red threat bands remain distinguishable under normal combat, boss phase transition, and overclock entry.
2. **Boss Telegraph Priority**: Ensure VFX, camera shake, and hitstop do not obscure boss laser/shield telegraphs or HUD warning states.
3. **Hitstop and Slow-Motion Legibility**: Confirm UI Toolkit timers, cooldown overlays, and warning indicators continue using unscaled time.
4. **Audio Warning Hierarchy**: Verify warning cues remain audible over rapid-fire hits and explosions through voice caps and mixer routing.
5. **VFX Budget Communication**: Ensure lower-cost quality flags or fallback settings are present for Phase 9 optimization to consume later.
6. **No Phase 9 Scope Leakage**: Do not add Safe Area layouts, joystick controls, localization keys, store screenshots, or mobile-specific settings screens.

### Verification Criteria (QA Perspective)
1. Capture before/after or representative screenshots for dense combat, boss phase transition, and overclock entry.
2. Document any occlusion or contrast failure with exact object, material, or widget names.
3. Confirm no red errors or critical warnings after the readability scene/run.
4. If no code or asset changes are needed, record that explicitly in the audit walkthrough.
5. Update `docs/architecture/06_art_and_sound.md` only if budgets, naming, or fallback semantics change.
6. The audit separates measured screenshot findings from subjective feel notes.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
