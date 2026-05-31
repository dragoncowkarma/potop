# [TARGET: Multiple Files (Documentation)] [TASK: 7.5.5]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.5.5` |
| **Agent Role** | `Gemini CLI (QA/Documentation Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `N/A (documentation task)`
- **Delta**: `docs/delta/7.5.4.json`

---

## Work Scope

**Target File**: `Multiple Files (Documentation)`

### Technical Requirements (10-Year Expert Feedback)
1. **KANBAN 동기화**: `docs/tasks/` 내 Task 7.2~7.6 JSON 파일의 `status`를 `Approved`로 변경. coverage, duration 등 metrics 갱신.
2. **SUMMARY.xml 동기화**: `docs/SUMMARY.xml`에서 Phase 7 프롬프트 status를 `completed`로 변경. Phase 7.5 섹션 추가.
3. **루트 SUMMARY.xml**: Phase 7.5 프롬프트 참조 추가.
4. **GDD 결함 수정**:
   - `docs/requirements/01_overview.md`: "하이퍼캐주얼" → "캐주얼 로그라이트" 수정.
   - `docs/architecture/06_art_and_sound.md`: "하이퍼캐주얼" 수정 + 공간음향 조항을 "기본 2D 스테레오. VR 빌드에서는 Spatial Audio 활성화"로 수정.
   - `docs/requirements/03_data_and_balance.md`: Lines 24-26의 `+|` 머지 충돌 잔재 제거.
   - `docs/architecture/04_technical_architecture.md`: 두 번째 "2." → "3." 넘버링 수정.
5. **conflict_report.md 현행화**: Phase 7 기준으로 해결된 항목 표시. 여전히 유효한 항목만 남김.
6. **REFACTOR_TRACKING.md 갱신**: 해결 완료 항목 체크, 신규 추적 항목 추가.
7. **07_development_milestones.md**: Phase 7과 8 사이에 Phase 7.5 마일스톤 섹션 삽입.

### Verification Criteria (QA Perspective)
1. grep "하이퍼캐주얼" docs/ → 0건.
2. KANBAN 보드의 7.2~7.6이 Done 열에 표시.
3. SUMMARY.xml Phase 7 프롬프트 전체 completed.

### Phase Constraints
- **RED Phase**: Write failing tests first under `tests/` and verify they fail.
- **GREEN Phase**: Implement code to pass tests.
- **DOCUMENT Phase**: Update documentation.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
