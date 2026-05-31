# [TARGET: Multiple Files (Verification)] [TASK: 7.5.6]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.5.6` |
| **Agent Role** | `Gemini CLI (QA Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `All gameplay systems`
- **Delta**: `docs/delta/7.5.5.json`

---

## Work Scope

**Target File**: `Multiple Files`

### Technical Requirements (10-Year Expert Feedback)
1. **컴파일 검증**: asmdef 적용 후 Unity 에디터 컴파일 에러 0건 확인.
2. **테스트 실행**: 전체 EditMode 테스트 통과 검증 (기존 52개 + 신규 테스트).
3. **정적 분석**: EventBroker 구독/해제 짝 확인. Subscribe 호출마다 대응하는 Unsubscribe 존재 검증.
4. **문서 정합성**: KANBAN ↔ SUMMARY.xml ↔ milestones 3자 교차 검증. 모든 Phase 7/7.5 태스크 상태 일치 확인.
5. **GDD 용어 일관성**: GDD 용어 일관성 검증 (오개념 용어, 중복 넘버링, 머지 충돌 잔재 0건 확인).

### Verification Criteria (QA Perspective)
1. 전체 테스트 0 failures.
2. 콘솔 Red Error 0건.
3. 문서 정합성 체크리스트 100% 통과.

### Verification Command
```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 7.5.6 --cmd "./UnityProject/run_tests.sh"
```

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
