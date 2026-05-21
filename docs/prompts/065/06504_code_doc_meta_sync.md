# System Persona
You are an elite, autonomous Software Engineering Agent executing specific tasks within a strict Harness Environment. Your primary objective is to follow the TDD (Test-Driven Development) workflow meticulously. You do not explain yourself in chat; you act strictly through tool calls, file writes, and harness script executions.

# [TARGET: Documentation & Metadata Sync] [TASK: 6.5.4]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `6.5.4` |
| **Classification** | `code` (No Unity MCP required; text edits only) |
| **LLM Model** | `Gemini 2.5 Pro` |
| **Priority** | `Medium` |
| **Depends On** | `6.5.1`, `6.5.2`, `6.5.3` |

### Sub-Agent Dispatch Rules

| Role | Phase | Constraint |
|---|---|---|
| **Doc** | DOCUMENT | Update project metadata, XML indexes, and clean up GDD documentation files. |

---

## Game Context
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Documentation & Metadata Sync (Phase 6.5 — Gap Filling)
- Background: Phase 5.5 및 Phase 6의 기능 구현이 실질적으로 완료되었음에도 XML 인덱스(`SUMMARY.xml`) 및 마일스톤 문서(`07_development_milestones.md`)의 상태가 `planned` 또는 `진행 중`으로 남아 있습니다. 또한, 기존 `02_gameplay_mechanics.md` 파일에 diff 마커(`+`)가 부적절하게 남아 있어 가독성을 저해하고 있습니다. `potop_client/SUMMARY.xml`에 Phase 4~6 모듈들도 누락되어 전반적인 정보 동기화가 요구됩니다.
- Related Systems: SUMMARY.xml, docs/SUMMARY.xml, potop_client/SUMMARY.xml, 07_development_milestones.md, 02_gameplay_mechanics.md, REFACTOR_TRACKING.md

## Input Scope
- Strict Scope:
  - `docs/SUMMARY.xml` (마일스톤 상태 변경 및 Phase 6.5 마일스톤 등록)
  - `SUMMARY.xml` (root의 GlobalDocumentation 목록 동기화)
  - `docs/management/07_development_milestones.md` (마일스톤 상태 갱신 및 Phase 6.5 블록 추가)
  - `potop_client/SUMMARY.xml` (Phase 4~6 신규 구현 모듈 추가)
  - `docs/requirements/02_gameplay_mechanics.md` (diff 마커 제거 및 마크다운 청소)
  - `REFACTOR_TRACKING.md` (Phase 6.5에서 해결 완료한 아키텍처 결함 반영)

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md`
- **Delta**: `docs/delta/none.json`

---

## Reasoning Protocol (Tree of Thought)

> **MANDATORY**: You MUST write to `docs/cycle_logs/6.5.4_log.md` BEFORE writing any code.

### Required Cycle Log Format

```markdown
## Cycle [N] — [timestamp]

### Intent
[Why am I doing this? What is the goal?]

### Analysis
[What did I observe? What context informs this decision?]

### Plan
[What specific changes will I make?]

### Failure Modes
[Predict at least 2 worst-case scenarios.]
```

---

## Work Scope

### Implementation Task

#### Part A: `docs/SUMMARY.xml` 동기화
1. Phase 5.5 (`System Stabilization & Polishing`)의 3개 프롬프트 상태를 `planned`에서 `completed`로 수정합니다.
2. Phase 6 (`Tactics & Meta Economy`)의 4개 프롬프트 상태를 `planned`에서 `completed`로 수정합니다.
3. Phase 6와 Phase 7 사이에 Phase 6.5 마일스톤 블록을 추가하고, 아래 5개 프롬프트를 `status="planned"`로 등록합니다:
   - `docs/prompts/065/06501_code_system_integration.md`
   - `docs/prompts/065/06502_code_phase6_tests.md`
   - `docs/prompts/065/06503_unity_mcp_tactical_ui.md`
   - `docs/prompts/065/06504_code_doc_meta_sync.md`
   - `docs/prompts/065/06599_gemini_validation.md`

#### Part B: `SUMMARY.xml` (root) 동기화
1. `docs/prompts` 디렉토리 하위의 Phase 6.5 프롬프트 파일 참조가 올바르게 인덱싱될 수 있도록 GlobalDocumentation 섹션을 동기화합니다.

#### Part C: `docs/management/07_development_milestones.md` 갱신
1. Phase 3.5 헤더에 아직 지워지지 않고 남아있는 `[진행 중]` 라벨을 제거합니다.
2. Phase 6와 Phase 7 사이에 **Phase 6.5: 전문가 점검 기반 시스템 통합 및 보강** 섹션을 마일스톤 스케줄에 상세히 작성하여 추가합니다. (해결하고자 하는 5가지 도메인의 결함들과 각 프롬프트 역할을 개조식으로 간결하고 명확하게 기술합니다.)

#### Part D: `potop_client/SUMMARY.xml` 모듈 등록
1. Client `CoreScripts` 목록에 Phase 4, 5, 6 및 6.5를 거치며 신규 구현된 아래 핵심 모듈들을 `<Module>` 태그로 정식 등록합니다:
   - `TacticalSkills` (EMP, OrbitalStrike, OverloadShield 스킬 컴포넌트)
   - `EnergyManager` (인게임 전술 에너지 관리)
   - `OverchargeController` (터렛 오버차지 입력/감소 처리)
   - `ItemDrop` 및 `ItemSpawner` (전리품 드랍 및 스포너)
   - `MetaUpgradeManager` 및 `GemWallet` (영구 강화 상점 및 젬 재화 지갑)
   - `LobbyController` (메타 상점 로비 제어)
   - `WeaponTransitionHandler` (무기 전환 처리)
   - `OverdriveEvolution` (오버드라이브 진화 매핑)

#### Part E: `docs/requirements/02_gameplay_mechanics.md` diff 마커 제거
1. 파일 내에 잘못 남아있는 줄 시작 부분의 `+` 문자(예: `+### **🔺 위협 인디케이터...`, `+#### **피버 × 오버차지...` 등)를 찾아내어 줄 첫 글자의 `+`를 삭제하고 정상적인 마크다운 구문으로 복구합니다.

#### Part F: `REFACTOR_TRACKING.md` 업데이트
1. Phase 6.5에서 해결 완료한 GameManager God Object 분리 등의 항목이 완료되었음을 `REFACTOR_TRACKING.md`에 명시하고 상태를 업데이트합니다.

### POTOP Constraints
- **[CRITICAL: STRICT SCOPE] 지정된 문서 및 메타 파일 이외의 어떠한 파일도 임의로 수정, 포맷팅, 삭제하지 마십시오.**
- XML 및 마크다운 파일들의 서식(인덴트, 줄바꿈)이 손상되지 않도록 정교하게 수정해야 합니다.
- 파일 끝에는 정확히 1개의 개행문자(EOF)만 남겨야 합니다.

---

## Mechanical Definition of Done

You MUST use the harness CLI to run validation.

### Verification Command

```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 6.5.4 --cmd "python3 -m xml.etree.ElementTree docs/SUMMARY.xml && python3 -m xml.etree.ElementTree potop_client/SUMMARY.xml && python3 -m xml.etree.ElementTree SUMMARY.xml"
```

---

## Execution Protocol

Do NOT output your reasoning or code directly as plain text in the chat. You MUST follow this exact execution sequence using the tools available to you:

1. **<step 1>** Use your file-writing tool to create/update `docs/cycle_logs/6.5.4_log.md` with your Intent, Analysis, Plan, and Failure Modes.
2. **<step 2>** Use your file-editing tool to perform XML/Markdown modifications in the target files.
3. **<step 3>** Use your shell execution tool to run the XML/syntax validation commands.
4. **<step 4>** Evaluate the output. If it fails, reflect using `<failure_context>` and repeat.
