# [Phase 04] [jules] [p02] 경험치 보석 드랍/수집 시스템
- parallel: [p01](04001_jules_p01_turret_classes.md), [p03](04003_jules_p03_xp_leveling.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: EXP Gem Drop & Collection System (04002)
- Background: Phase 4 — 적 처치 시 경험치 보석 드랍, 자력 흡수 메카닉 구현
- Related Systems: PoolManager (Phase 2), EventBroker (`OnEnemyKilled`), EnemyData SO
- GDD Reference: `02_gameplay_mechanics.md` §아이템 및 파밍 (Blue:10XP, Green:50XP, Red:200XP)
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `EXPGem.cs`: PoolManager에 등록되는 경험치 보석. 3종 (Blue/Green/Red).
3. Create `EXPGemData.asset` ×3: SO로 XP 값(10/50/100), 프리팹 참조, 시각 색상 관리.
4. Implement 자력 흡수 로직: 플레이어 주변 `MagnetRadius`(기본 3m) 내 보석 자동 이동. `Update()` 대신 Physics Overlap 기반.
5. EventBroker `OnEnemyKilled` 구독 → 적 위치에 보석 스폰 (EnemyData.expValue 기준 등급 결정).
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Namespace: `Potop.Client.Gameplay`.
- [Required] PoolManager를 통한 Spawn/Despawn만 사용. `Instantiate`/`Destroy` 금지.
- [Prohibited] `Update()`에서 매 프레임 거리 계산 금지. OverlapSphere + FixedUpdate 또는 타이머 기반으로 구현.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Items/EXPGem.cs`, `Assets/Data/Items/EXPGemData.asset` ×3
- Reference (Read-only): `Assets/Scripts/Core/PoolManager.cs`, `Assets/Scripts/Core/EventBroker.cs`
</input_data>

# 📝 Output Format
<output_format>
<thinking>
- OverlapSphere 주기 결정: FixedUpdate(0.02s) vs 커스텀 타이머(0.1s)로 성능/반응성 트레이드오프 분석
- 보석 이동 로직: Lerp vs MoveTowards vs AddForce — 시각적 만족도와 성능 비교
- **Verify Scope Restriction and potential conflicts with p01 (Turret), p03 (Leveling)**
</thinking>
<implementation>
- Create EXPGem system with PoolManager integration.
</implementation>
<verification>
- [ ] 적 처치 시 적 위치에 보석 정상 스폰
- [ ] 자력 반경 내 보석이 플레이어 방향으로 이동 후 흡수
- [ ] PoolManager를 통한 재활용 확인 (Instantiate/Destroy 0건)
- [ ] **Scope Restriction strictly verified**
</verification>
</output_format>
