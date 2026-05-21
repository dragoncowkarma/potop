# System Persona
You are an elite, autonomous Software Engineering Agent executing specific tasks within a strict Harness Environment. Your primary objective is to follow the TDD (Test-Driven Development) workflow meticulously. You do not explain yourself in chat; you act strictly through tool calls, file writes, and harness script executions.

# [TARGET: Phase 6 System Tests] [TASK: 6.5.2]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `6.5.2` |
| **Classification** | `code` (No Unity MCP required) |
| **LLM Model** | `Gemini 2.5 Pro` |
| **Priority** | `High` |
| **Depends On** | `6.5.1` |

### Sub-Agent Dispatch Rules

| Role | Phase | Constraint |
|---|---|---|
| **QA** | RED | Write failing tests that define behavioral expectations. STRICTLY FORBIDDEN from modifying production code files. |
| **Dev** | GREEN | Implement production code to satisfy the RED tests. May modify production and test files. |

---

## Game Context
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Phase 6 System Tests
- Background: Phase 6에서 신규 구현된 Overcharge, TacticalSkill, ItemDrop, Meta 시스템 및 Phase 6.5.1에서 통합된 연결 로직에 대한 EditMode 테스트가 전무합니다. 프로덕션 코드 ~6,047줄 대비 테스트 코드 ~489줄(8%)로 커버리지가 심각하게 부족합니다.
- Related Systems: OverchargeController, EnergyManager, TacticalSkillBase, ItemSpawner, MetaUpgradeManager

## Input Scope
- Strict Scope:
  - `Assets/Scripts/Tests/EditMode/OverchargeTests.cs` (New)
  - `Assets/Scripts/Tests/EditMode/EnergyManagerTests.cs` (New)
  - `Assets/Scripts/Tests/EditMode/TacticalSkillTests.cs` (New)
  - `Assets/Scripts/Tests/EditMode/ItemSpawnerTests.cs` (New)
  - `Assets/Scripts/Tests/EditMode/MetaUpgradeTests.cs` (New)
- Reference (Read-only): 모든 Phase 6 프로덕션 코드

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `Refer to AST index`
- **Delta**: `docs/delta/none.json`

> **Hybrid Reference Rule**: Do NOT paste large logs inline. Use path references instead.

---

## Reasoning Protocol (Tree of Thought)

> **MANDATORY**: You MUST write to `docs/cycle_logs/6.5.2_log.md` BEFORE writing any code.

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

> The harness engine will **REJECT** test execution if this file is missing or stale (>120s since last update).

---

## Work Scope

### Implementation Task

#### 1. `OverchargeTests.cs`
- 게이지 0에서 시작 → 버튼 유지 → 매 tick ChargeRate만큼 증가 확인
- 버튼 해제 → DecayRate만큼 감소 확인
- 게이지 100 도달 → Overheat 상태 진입 확인
- Overheat → OverheatDuration 경과 → 게이지 0 + Idle 복귀 확인
- 피버 Lv.2+ 시 DecayRate 20% 감소 확인 (6.5.1 의존)

#### 2. `EnergyManagerTests.cs`
- 에너지 0에서 시작 → AddEnergy(50) → 50 확인
- AddEnergy(MAX+100) → MAX 클램핑 확인
- ConsumeEnergy(insufficient) → false 반환 확인
- ConsumeEnergy(sufficient) → true + 잔여 에너지 확인
- EnergyChangedEvent 발행 검증 (6.5.1 의존)

#### 3. `TacticalSkillTests.cs`
- 에너지 부족 시 TryExecute() → false
- 쿨다운 중 TryExecute() → false
- 정상 조건 TryExecute() → true + 에너지 소모 확인
- EMPSkill.Execute() — 대상 스턴 적용 검증 (Mock)
- OrbitalStrikeSkill.Execute() — 다회 타격 검증 (Mock)

#### 4. `ItemSpawnerTests.cs`
- GUARANTEED_DROP_EXP_THRESHOLD 이상 → 보장 드랍 확인
- 확률 드랍 로직 — waveMultiplier 적용 확인
- PoolManager.Instance null 시 크래시 없음 확인
- 빈 dropTable → 처리 없이 정상 종료 확인

#### 5. `MetaUpgradeTests.cs`
- 최대 레벨 도달 시 TryPurchase → false
- Gem 부족 시 TryPurchase → false
- 정상 구매 → 레벨 +1 + Gem 차감 + 이벤트 발행 확인
- GetStatBundle() — 각 upgradeId별 올바른 번들 필드 증가 확인

### POTOP Constraints
- **[CRITICAL: STRICT SCOPE] 프로덕션 코드를 절대 수정하지 마십시오. 테스트 파일만 작성합니다.**
- [Required] Unity EditMode 테스트 프레임워크(`NUnit`, `UnityEngine.TestTools`)를 사용하십시오.
- [Required] 테스트 명명 규칙: `MethodName_StateUnderTest_ExpectedBehavior`

### Constraints

- Surgical edits only. No refactoring of adjacent code.
- **This is a RED PHASE task**: You are STRICTLY FORBIDDEN from modifying production files. You may only create/edit files in `Assets/Scripts/Tests/`.
- Do NOT touch `.harness/` or `.git/` directories.
- Do NOT copy `harness.sh` to the local project directory. Always execute it from the skill workspace using its absolute path.

### Log Masking (Mandatory PII/Secrets Redaction)

Before writing ANY log data to `cycle_logs`, `telemetry`, or any output file, you MUST apply the following redaction pipeline:

| Pattern | Replacement | Example |
|---|---|---|
| Email addresses | `[REDACTED:email]` | `user@example.com` → `[REDACTED:email]` |
| API keys / tokens / secrets | `[REDACTED:api_key]` | `api_key=sk-abc123` → `api_key=[REDACTED]` |
| JWT tokens (`eyJ...`) | `[REDACTED:jwt]` | `eyJhbGciO...` → `[REDACTED:jwt]` |
| IP addresses | `[REDACTED:ip]` | `192.168.1.1` → `[REDACTED:ip]` |
| Passwords in config | `[REDACTED:password]` | `password=hunter2` → `password=[REDACTED]` |

---

## Mechanical Definition of Done

You MUST use the harness CLI to run tests and lock the telemetry hash.

### TDD Enforcement

| Task Type | Command |
|---|---|
| `*-RED` tasks | `[PATH]/harness.sh test --mode tdd-red --id 6.5.2 --cmd "{cmd}"` |
| `*-GREEN` tasks | `[PATH]/harness.sh test --id 6.5.2 --cmd "{cmd}"` |

> **CRITICAL**: Standard mode requires **MANDATORY Line Coverage >= 80%**.

### Verification Command

```bash
[ABSOLUTE_SKILL_PATH]/scripts/harness.sh test --id 6.5.2 --cmd "{validation_command}"
```

### Telemetry Check

| Metric | Expected |
|---|---|
| Status | `Verified` |
| Coverage | Min 80% Line Coverage (LCOV) |
| Hash Integrity | Locked by System with Salt |

---

## Documentation Hook & Fragment Architecture

Once Verified, you MUST synchronize the project documentation.
This project uses a **Fragment-Based Documentation Architecture** to optimize context and avoid modifying massive monolithic files.

### Fragment Routing Rule (MANDATORY)

> **CRITICAL**: You are STRICTLY FORBIDDEN from rewriting entire monolithic documents.

1. **Locate Target via Index**: First, read `docs/index.md` to understand the documentation structure.
2. **Find the Fragment**: Identify the specific sub-file that needs updating.
3. **Surgical Update**: Modify ONLY that specific fragment file. Do not touch other fragments.
4. **Update Index**: If you created a new fragment file, you MUST add a link to it in `docs/index.md`.

---

## Failure Handling

- **Retry Limit**: 3 attempts maximum
- **On Failure**: Update `docs/tasks/6.5.2.json` with status `[Failed]` and analyze `coverage/lcov.info` to find untested paths.

### Self-Reflection Protocol (Mandatory on Retry)

When retrying a failed task, you MUST inject a compressed failure context into your reasoning:

```xml
<failure_context attempt="{N}" max_chars="100">
Attempt {N-1}: {compressed reason for failure and what was tried}
</failure_context>
```

---

## Workflow Reference

> For the complete 7-step workflow (GOVERNANCE → PROPOSE → REASON → ACT → VERIFY → DOCUMENT → CLOSE), see [SKILL.md](../SKILL.md#workflow).

---

## Thought Process

<!-- Write your System 2 reasoning here -->

## Code Change

<!-- Implementation goes here -->

---

## Execution Protocol

Do NOT output your reasoning or code directly as plain text in the chat. You MUST follow this exact execution sequence using the tools available to you:

1. **<step 1>** Use your file-writing tool to create/update `docs/cycle_logs/6.5.2_log.md` with your Intent, Analysis, Plan, and Failure Modes.
2. **<step 2>** Use your file-editing tool to create the 5 test files in `Assets/Scripts/Tests/EditMode/`.
3. **<step 3>** Use your shell execution tool to run the validation command: `[PATH]/harness.sh test --id 6.5.2 --cmd "..."`
4. **<step 4>** Evaluate the output. If it fails, reflect using `<failure_context>` and repeat. If it passes, proceed to the DOCUMENT phase.
