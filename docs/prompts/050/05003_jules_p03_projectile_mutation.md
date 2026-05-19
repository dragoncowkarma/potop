# [Phase 050] [jules] [p03] 투사체 변이 물리 로직
- parallel: [p01](05001_jules_p01_upgrade_table.md), [p02](05002_jules_p02_mutation_synergy.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
> [!IMPORTANT]
> **Before starting, review [`AGENTS.md`](../../../AGENTS.md) and [`potop_client/AGENTS.md`](../../../potop_client/AGENTS.md).**

<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Projectile Mutation Physics (05003)
- Background: Phase 050 — 관통/도탄/유도/거대화 각 투사체 변이의 물리 로직 구현
- Related Systems: Projectile (Phase 1), IModifier, Physics Layers
- GDD Reference: `02_gameplay_mechanics.md` §물리 규칙, `03_data_and_balance.md`
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `PierceModifier.cs`: 투사체가 적 관통 시 HP 감소 후 계속 진행. 최대 관통 횟수 제한 (SO 필드).
3. Create `BounceModifier.cs`: 적/벽 충돌 시 반사 벡터 계산. 최대 도탄 횟수 제한. 레이캐스트 기반 반사각.
4. Create `HomingModifier.cs`: 투사체가 가장 가까운 적 방향으로 회전 (RotateTowards). 회전 속도 SO 필드.
5. Create `ScaleModifier.cs`: 투사체 크기 배율 적용 + 충돌 범위 비례 확대.
6. 모든 Modifier는 `IModifier` 인터페이스 구현. `Apply(Projectile)` / `Remove(Projectile)` 패턴.
7. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Required] 관통/도탄 무한 루프 방지: 최대 횟수 초과 시 투사체 즉시 Despawn.
- [Required] 도탄 반사각 계산은 `Vector3.Reflect` 사용. 자체 구현 금지.
- **[CRITICAL]** Scope 외 파일 수정 금지.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Weapons/Modifiers/PierceModifier.cs`, `BounceModifier.cs`, `HomingModifier.cs`, `ScaleModifier.cs`
- Reference (Read-only): `Assets/Scripts/Gameplay/Weapons/WeaponBase.cs`, `Assets/Scripts/Gameplay/Projectile.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the requirements and constraints.
- Plan the implementation strategy and edge cases.
- Verification of `AGENTS.md` compliance.
</thinking>
<implementation>
- [Instructions: Use agent tools or Diff format]
</implementation>
<verification>
- [ ] Requirements met
- [ ] EOF empty line and comment cleanup completed
- [ ] Magic Numbers removed
</verification>
</output_format>
