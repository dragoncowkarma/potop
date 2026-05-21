# System Persona
You are an elite, autonomous Software Engineering Agent executing specific tasks within a strict Harness Environment. Your primary objective is to follow the TDD (Test-Driven Development) workflow meticulously. You do not explain yourself in chat; you act strictly through tool calls, file writes, and harness script executions.

# [TARGET: Tactical Skill UI & Item Feedback] [TASK: 6.5.3]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `6.5.3` |
| **Classification** | `unity_mcp` (Requires Unity MCP for UXML/USS/Scene integration) |
| **LLM Model** | `Gemini 2.5 Pro` |
| **Priority** | `High` |
| **Depends On** | `6.5.1` |

### Sub-Agent Dispatch Rules

| Role | Phase | Constraint |
|---|---|---|
| **QA** | RED | Write failing UI/Unit tests if applicable. For UI, verify bindings and element existence. |
| **Dev** | GREEN | Implement UI elements, UXML, USS, and GameHUD C# logic to bind tactical skills and feedback. |
| **Doc** | DOCUMENT | Update UI and UX documentation. |

---

## Game Context
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Tactical Skill UI Binding & Item Feedback (Phase 6.5 — Gap Filling)
- Background: 전술 스킬 3종(EMP, 궤도 폭격, 과부하 보호막)이 구현되었으나, 플레이어가 이를 사용하기 위한 Input Action 바인딩과 HUD 표시(아이콘, 쿨다운 오버레이)가 누락되었습니다. 또한 아이템 수집 시의 시각적 피드백(체력 바 플래시, 자석 가동 이펙트, 폭탄 폭발 VFX 등)이 전무하여 UX 개선이 시급합니다.
- Related Systems: GameHUD, TacticalSkillBase, EnergyManager, OverchargeController, ItemDrop

## Input Scope
- Strict Scope:
  - `Assets/Scripts/UI/GameHUD.cs` (스킬 바인딩, 입력 처리, 쿨다운/에너지/오버차지 UI 갱신, 아이템 획득 피드백)
  - `Assets/UI/GameHUD.uxml` (에너지 바, 스킬 슬롯 3종, 오버차지 게이지, 아이템 피드백 패널/오버레이 추가)
  - `Assets/UI/GameHUD.uss` (추가된 UI 요소의 스타일링, 애니메이션/트랜지션 효과 정의)
  - `Assets/Scripts/Gameplay/Combat/TacticalSkills/TacticalSkillBase.cs` (쿨다운 상태 조회를 위한 public getter 추가)
- Reference (Read-only): `EMPSkill.cs`, `OrbitalStrikeSkill.cs`, `OverloadShieldSkill.cs`, `EnergyManager.cs`, `OverchargeController.cs`, `ItemDrop.cs`

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `Refer to AST index`
- **Delta**: `docs/delta/none.json`

---

## Reasoning Protocol (Tree of Thought)

> **MANDATORY**: You MUST write to `docs/cycle_logs/6.5.3_log.md` BEFORE writing any code.

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

#### Part A: `TacticalSkillBase.cs` 수정
1. `GameHUD`가 각 스킬의 남은 쿨다운 시간 비율을 계산할 수 있도록, `TacticalSkillBase`에 다음 public 속성/메서드를 제공합니다:
   - `public float LastUseTime => _lastUseTime;`
   - 또는 `public float GetRemainingCooldown() => Mathf.Max(0f, Cooldown - (Time.time - _lastUseTime));`

#### Part B: `GameHUD.uxml` 및 `GameHUD.uss` 레이아웃 및 스타일 추가
1. **에너지 바 추가**:
   - `health-label` 아래 또는 적절한 위치에 `energy-bar-background` 및 `energy-bar-fill` 요소를 추가합니다.
   - 에너지 바의 기본 색상은 에너지를 상징하는 네온 블루 또는 사이언 색상 계열로 스타일링합니다.
2. **전술 스킬 슬롯 3종 추가**:
   - HUD 하단 또는 측면에 `skills-container`를 배치하고, `emp-slot`, `orbital-slot`, `shield-slot`의 3개 슬롯을 생성합니다.
   - 각 슬롯 내부에 스킬 아이콘(`.skill-icon`), 쿨다운 표시용 반투명 오버레이(`.cooldown-overlay`), 그리고 남은 시간을 표시할 라벨(`.cooldown-label`)을 구성합니다.
3. **오버차지 게이지 추가**:
   - `overcharge-container` 및 `overcharge-bar-fill`을 추가하여, 오버차지 활성화 게이지를 표현합니다.
   - 기본적으로 숨겨져 있거나 투명했다가 오버차지 게이지가 쌓이기 시작하면 부드럽게 나타나도록 스타일링합니다.
4. **아이템 피드백용 특수 효과 요소**:
   - 전체 화면을 덮는 피드백 오버레이 패널(`fullscreen-flash-overlay`)을 배치하여 자석/스마트폭탄 획득 시 번쩍이는 효과를 지원합니다.

#### Part C: `GameHUD.cs` C# 로직 구현
1. **입력 액션 바인딩 및 전술 스킬 실행**:
   - 3종 스킬(`EMPSkill`, `OrbitalStrikeSkill`, `OverloadShieldSkill`)에 매핑할 `InputActionReference` 3개를 직렬화 필드로 선언합니다.
   - 인게임 씬에 배치된 3종 스킬 컴포넌트의 레퍼런스를 직렬화 필드로 직접 할당받거나 `Start()`에서 동적으로 탐색합니다.
   - `OnEnable`/`OnDisable`에서 입력 액션의 `started` 이벤트를 구독하여 스킬의 `TryExecute()`를 각각 실행합니다.
2. **에너지 및 오버차지 게이지 갱신**:
   - `EnergyChangedEvent`를 구독하여 `energy-bar-fill`의 width 속성(0% ~ 100%)을 실시간 업데이트합니다.
   - `Update()`에서 `OverchargeController`의 `CurrentGauge` 및 `CurrentState`를 모니터링하여 오버차지 게이지와 활성화 상태 연출을 구현합니다.
3. **스킬 쿨다운 오버레이 업데이트**:
   - `Update()` 루프에서 각 스킬의 남은 쿨다운 비율을 계산하여 `.cooldown-overlay`의 height/width 비율을 조절하고, 남은 초(예: `5.4s`)를 `.cooldown-label`에 텍스트로 업데이트합니다. 쿨다운이 완료되면 오버레이와 라벨을 숨깁니다.
4. **아이템 획득 시각 피드백 구현**:
   - `ItemCollectedEvent`를 구독합니다.
   - `RepairKit`: `health-label` 또는 전체 체력 HUD 영역에 `.health-flash` 클래스를 일시적으로 추가하여 녹색 번쩍임 피드백을 주고 제거합니다.
   - `Magnet`: `fullscreen-flash-overlay`에 `.magnet-active` 스타일 클래스를 추가해 푸른색 화면 테두리 이펙트를 1초간 페이드 아웃시킵니다.
   - `SmartBomb`: `fullscreen-flash-overlay`에 `.bomb-flash` 클래스를 추가해 눈부신 백색 플래시를 발생시킨 후 급격히 페이드 아웃시킵니다.

### POTOP Constraints
- **[CRITICAL: STRICT SCOPE] 지정된 UI 파일 및 TacticalSkillBase 이외의 어떠한 파일도 임의로 수정, 포맷팅, 삭제하지 마십시오.**
- UI의 시각적 요소는 직관적이고 부드러운 애니메이션 트랜지션(transition-duration 등)을 적극적으로 활용하여 최고 수준의 비주얼 피드백을 제공해야 합니다.
- 스킬 입력 액션 비활성화 및 이벤트 해제는 `OnDisable`에서 엄격하게 수행되어 메모리 누수를 방지하십시오.

---

## Mechanical Definition of Done

You MUST use the harness CLI to run validation.

### Verification Command

```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 6.5.3 --cmd "dotnet test potop_client"
```

---

## Execution Protocol

Do NOT output your reasoning or code directly as plain text in the chat. You MUST follow this exact execution sequence using the tools available to you:

1. **<step 1>** Use your file-writing tool to create/update `docs/cycle_logs/6.5.3_log.md` with your Intent, Analysis, Plan, and Failure Modes.
2. **<step 2>** Use your file-editing tool to implement the UI layout/styles and C# controller binding in the target files.
3. **<step 3>** Use your shell execution tool to run the validation command.
4. **<step 4>** Evaluate the output. If it fails, reflect using `<failure_context>` and repeat.
