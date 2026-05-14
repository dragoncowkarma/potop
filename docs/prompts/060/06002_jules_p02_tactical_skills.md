# [Phase 07] [jules] [p02] 액티브 전술 스킬 3종
- parallel: [p01](07001_jules_p01_overcharge.md), [p03](07003_jules_p03_item_drop.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Tactical Skills (07002)
- Background: Phase 7 — 에너지 소모형 액티브 스킬 3종 구현
- Related Systems: EventBroker (`OnEnergyChanged`), EnemyBase, PoolManager
- GDD Reference: `02_gameplay_mechanics.md` §액티브 전술 스킬 (EMP/궤도 폭격/보호막)
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `TacticalSkillBase.cs`: 추상 클래스. 에너지 소모량, 쿨다운, `Execute()` 추상 메서드.
3. Create `EMPSkill.cs`: 에너지 500 소모. 화면 내 모든 적 5초 정지 (`EnemyBase.Stun(5f)`). 투사체 제거.
4. Create `OrbitalStrikeSkill.cs`: 에너지 700 소모. 화면 내 랜덤 위치 10회 폭격. 각 폭격 반경 2m, 대미지 50.
5. Create `OverloadShieldSkill.cs`: 에너지 1000 소모. 10초간 무적 + 접촉 적 즉시 파괴.
6. 에너지 시스템: `EnergyManager.cs` — 적 처치 시 에너지 획득 (EnemyData.energyValue), 최대 1000.
7. 모든 수치는 SO (`TacticalSkillData.asset` ×3)로 외부화.
8. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Required] EMP의 적 정지는 `EnemyBase`의 기존 인터페이스만 사용. 새 public 메서드 추가 시 `REFACTOR_TRACKING.md` 기록 필수.
- **[CRITICAL]** Scope 외 파일 수정 금지.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Combat/TacticalSkillBase.cs`, `EMPSkill.cs`, `OrbitalStrikeSkill.cs`, `OverloadShieldSkill.cs`, `EnergyManager.cs`, `Assets/Data/Combat/TacticalSkillData.asset` ×3
- Reference (Read-only): `Assets/Scripts/Gameplay/Enemies/EnemyBase.cs`, `Assets/Scripts/Core/EventBroker.cs`
</input_data>
