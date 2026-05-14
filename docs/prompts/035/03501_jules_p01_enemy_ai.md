# [Phase 3.5] [jules] [p01] 적 AI 최적화
- parallel: [p02](03502_jules_p02_fever_decouple.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Enemy AI Optimization (03501)
- Background: Phase 3.5 — EnemyBase에 FSM 패턴 적용, 시간 분할 회전, PoolManager 연동 안정화
- Related Systems: EnemyBase (Phase 3), PoolManager (Phase 2), WaveManager
- GDD Reference: `04_technical_architecture.md` §Object Pooling, §AI
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Refactor `EnemyBase.cs`: FSM 패턴(Idle/Chase/Attack/Death) 도입. 기존 단순 Update 루프를 스테이트 기반으로 전환.
3. Implement Time-sliced rotation: `Quaternion.RotateTowards` 호출을 N프레임 분산 (기본 3프레임). 50체 이상 동시 회전 시 성능 개선.
4. PoolManager 연동 검증: `OnEnable`/`OnDisable` 시 초기화/정리 보장. 좀비 상태 방지.
5. After completion, resolve "EnemyBase FSM" entry in `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Namespace: `Potop.Client.Gameplay`.
- [Required] 기존 EnemyBase public API 유지. 내부 리팩토링만 수행.
- [Prohibited] Coroutine 기반 FSM 금지. 순수 상태 전환 패턴 사용.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Enemies/EnemyBase.cs`, `Assets/Scripts/Gameplay/Enemies/EnemyStateMachine.cs` (신규)
- Reference (Read-only): `Assets/Scripts/Core/PoolManager.cs`, `Assets/Scripts/Core/EventBroker.cs`
</input_data>

# 📝 Output Format
<output_format>
<thinking>
- FSM 스테이트 전환 조건 분석: HP, 거리, 공격 쿨다운 기준
- 시간 분할 회전의 프레임 분산 전략: Round-robin vs 해시 기반 분산
- **Scope Restriction 검증: p02 (Fever)와 충돌 없음 확인**
</thinking>
<implementation>
- Refactor EnemyBase with FSM and time-sliced rotation.
</implementation>
<verification>
- [ ] EnemyBase FSM 4상태 정상 전환
- [ ] 50체 동시 존재 시 프레임 안정성 (프로파일러 기준)
- [ ] PoolManager Spawn/Despawn 사이클 10회 반복 시 좀비 0건
- [ ] **Scope Restriction strictly verified**
</verification>
</output_format>
