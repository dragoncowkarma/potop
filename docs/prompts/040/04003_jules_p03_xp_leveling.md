# [Phase 04] [jules] [p03] XP/레벨업 시스템
- parallel: [p02](04002_jules_p02_exp_gem.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: XP & Leveling System (04003)
- Background: Phase 4 — XP 누적, 레벨업 판정, 업그레이드 풀에서 3~4개 선택지 추출
- Related Systems: EventBroker (`OnEXPCollected`, `OnLevelUp`), EXPGem (04002)
- GDD Reference: `03_data_and_balance.md` §경험치 요구량 테이블
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `LevelingManager.cs`: XP 누적, 레벨 판정 (GDD 테이블: Lv1→2: 10XP, Lv2→3: 30XP... 등).
3. Create `UpgradePool.cs`: 전체 업그레이드 목록에서 랜덤 3~4개 추출. 중복 방지. 레어리티 가중치 (Phase 6에서 확장 예정이므로 인터페이스만 준비).
4. EventBroker 연동: `OnEXPCollected(int amount)` 구독 → XP 누적 → 레벨업 시 `OnLevelUp(int level, List<UpgradeOption> options)` 발행.
5. 레벨업 시 `Time.timeScale = 0` 설정 (UI 팀 연동 대비). 선택 완료 후 복원.
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
<constraints>
- [Required] EOF 빈 줄 1개, Namespace: `Potop.Client.Gameplay`.
- [Required] XP 요구량 테이블은 SO(`LevelingData.asset`)로 외부화. 하드코딩 금지.
- [Prohibited] 매직 넘버 금지. Time.timeScale 변경 시 반드시 복원 로직 보장.
- **[CRITICAL]** Scope 외 파일 수정 금지.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Progression/LevelingManager.cs`, `Assets/Scripts/Gameplay/Progression/UpgradePool.cs`, `Assets/Data/Progression/LevelingData.asset`
- Reference (Read-only): `Assets/Scripts/Core/EventBroker.cs`
</input_data>

# 📝 Output Format
<output_format>
<thinking>
- XP 요구량 커브: 선형 증가 vs GDD 테이블의 가변 증가 분석
- UpgradePool 확장성: Phase 6 확률 테이블과의 인터페이스 호환성 설계
- **Scope Restriction 검증: p02 (EXPGem)와 EventBroker 이벤트명 충돌 방지**
</thinking>
<implementation>
- Create LevelingManager and UpgradePool with SO-based configuration.
</implementation>
<verification>
- [ ] XP 누적 → 정확한 레벨업 판정 (GDD 테이블 기준)
- [ ] 업그레이드 선택지 3~4개 중복 없이 추출
- [ ] Time.timeScale 변경/복원 정상 동작
- [ ] **Scope Restriction strictly verified**
</verification>
</output_format>
