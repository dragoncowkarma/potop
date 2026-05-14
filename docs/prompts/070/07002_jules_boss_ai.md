# [Phase 08] [jules] 보스 AI 3페이즈

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**. (Jules)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Boss AI - Titan Core (08002)
- Background: Phase 8 — 3페이즈 FSM 기반 보스 AI. Phase 1~3 패턴 구현
- Related Systems: EnemyBase (상속), FSM 패턴 (Phase 4), EventBroker, TitanCore.prefab (08001)
- GDD Reference: `02_gameplay_mechanics.md` §타이탄 코어 — 회전 쉴드, 타겟 레이저, 광폭화
</context>

# 🛠️ Task
<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `TitanCoreAI.cs` (EnemyBase 상속):
   - **Phase 1 (HP 100%~60%)**: 회전 쉴드 활성. 정면만 피격 가능. 쉴드에 맞은 투사체 반사.
   - **Phase 2 (HP 60%~30%)**: 쉴드 파괴 연출. 타겟 레이저 패턴 — 플레이어 방향 1초 차징 → 2초 발사 (대미지 20/tick, 0.1s 간격).
   - **Phase 3 (HP 30%~0%)**: 광폭화. 이동 속도 2배. 3초마다 360도 탄막 발사 (8방향). 피격 시 넉백 면제.
3. FSM 스테이트: `BossIdle`, `BossPhase1`, `BossPhase2`, `BossPhase3`, `BossDeath`.
4. HP 임계값 전환 시 EventBroker: `OnBossPhaseChanged(int phase)` 발행 → 비주얼 팀 연동.
5. 사망 시 `OnBossDefeated` 이벤트 → 오버클럭 모드 진입 트리거.
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Required] 기존 `EnemyBase` 시그니처 변경 금지. `virtual` 메서드 `override`만 사용.
- [Required] 레이저 대미지/탄막 수치는 SO 필드로 외부화.
- [Prohibited] Coroutine 사용 최소화. FSM 스테이트 전환으로 대체.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Enemies/Boss/TitanCoreAI.cs`, `BossPhase1State.cs`, `BossPhase2State.cs`, `BossPhase3State.cs`, `Assets/Data/Enemies/TitanCoreData.asset`
- Reference (Read-only): `Assets/Scripts/Gameplay/Enemies/EnemyBase.cs`, `Assets/Scripts/Core/EventBroker.cs`
</input_data>
