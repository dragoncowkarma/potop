# [Phase 050] [jules] [p02] 무기 변이 & 시너지
- parallel: [p01](05001_jules_p01_upgrade_table.md), [p03](05003_jules_p03_projectile_mutation.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
> [!IMPORTANT]
> **Before starting, review [`AGENTS.md`](../../../AGENTS.md) and [`potop_client/AGENTS.md`](../../../potop_client/AGENTS.md).**

<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Mutation Synergy System (05002)
- Background: Phase 6 — 특정 변이 조합 감지 시 시너지 보너스 적용
- Related Systems: IModifier, WeaponBase, Modifier System (Phase 2.5)
- GDD Reference: `02_gameplay_mechanics.md` §무기 변이 및 시너지 (Pierce+Explosion, Multi+Bounce, Scale+Knockback)
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `MutationSynergyManager.cs`: 활성 모디파이어 조합을 감지하여 시너지 보너스 적용/해제.
3. 시너지 규칙 3종 구현:
   - **관통+폭발**: 관통 시 소형 폭발 트리거 (`OnPierceExplosion`)
   - **다연발+도탄**: 도탄 시 가장 가까운 적 자동 추적 (`OnBounceHoming`)
   - **거대화+넉백**: 투사체 충격파 범위 확대 (`OnScaleShockwave`)
4. 시너지 규칙은 `SynergyRuleData.asset` (SO)로 외부화. 코드에 하드코딩 금지.
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Required] 시너지 감지는 O(n²) 이하 복잡도. 모디파이어 변경 시에만 재평가 (매 프레임 금지).
- **[CRITICAL]** Scope 외 파일 수정 금지.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Weapons/Modifiers/MutationSynergyManager.cs`, `Assets/Data/Weapons/SynergyRuleData.cs`, `Assets/Data/Weapons/SynergyRuleData.asset`
- Reference (Read-only): `Assets/Scripts/Gameplay/Weapons/WeaponBase.cs`, `Assets/Scripts/Gameplay/Weapons/IWeapon.cs`
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
