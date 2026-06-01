# [TARGET: Assets/Scripts/Core/Localization/LocalizationService.cs] [TASK: 9.8]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `9.8` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and all Phase 9 UI-related prompts.

- **Map**: `docs/map.md` — Required symbols: `LocalizationService`
- **Delta**: `docs/delta/9.7.json`
- **Related Prompts**: `09001`, `09005`, `09007`, `09009`

---

## Work Scope

**Target Files**:
- `Assets/Scripts/Core/Localization/LocalizationService.cs`
- Localization data files for KO, EN, and JP
- UI Toolkit binding helpers if already present

### Technical Requirements (10-Year Expert Feedback)
1. **Language Map Fetcher**: Create `LocalizationService` supporting KO, EN, and JP with deterministic fallback to EN.
2. **Key-Based Text Binding**: UI Toolkit labels/buttons must bind through localization keys, not hardcoded visible strings.
3. **Runtime Language Switch**: Changing language updates active UI without requiring app restart.
4. **Coverage Discipline**: Lobby, settings, achievements, ads/revive prompts, tutorial, result screen, and store packaging messages need keys.
5. **Font and Layout Safety**: Verify KO/EN/JP font coverage and text expansion without overlap in mobile aspect ratios.
6. **Missing Key Behavior**: Missing keys should log a warning in development builds and show a safe fallback string, not crash.
7. **Save Integration**: Persist selected language through `09003` settings save data.

### Verification Criteria (QA Perspective)
1. **Translation Check**: Verify key-value lookups fetch expected strings per language and fall back to EN for missing optional keys.
2. **Coverage Test**: Assert required key sets are present in KO, EN, and JP files.
3. **Runtime Switch Test**: Switch language while lobby/tutorial UI is open and assert labels update.
4. **Layout Snapshot Check**: Capture mobile lobby/tutorial screens in KO, EN, and JP with no clipped text.
5. **Hardcoded String Audit**: Search relevant UI scripts/UXML for user-facing literals introduced in Phase 9.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
