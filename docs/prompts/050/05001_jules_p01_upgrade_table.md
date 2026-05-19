# [Phase 050] [jules] [p01] 업그레이드 확률 테이블
- parallel: [p02](05002_jules_p02_mutation_synergy.md), [p03](05003_jules_p03_projectile_mutation.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
> [!IMPORTANT]
> **Before starting, review [`AGENTS.md`](../../../AGENTS.md) and [`potop_client/AGENTS.md`](../../../potop_client/AGENTS.md).**

<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Upgrade Probability Table (05001)
- Background: Phase 6 — UpgradePool(Phase 5)을 확장하여 레어리티별 가중 확률, Pity 시스템 구현
- Related Systems: UpgradePool (Phase 5), LevelingManager
- GDD Reference: `02_gameplay_mechanics.md` §레벨업 패시브
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `UpgradeTableData.cs` (SO): 레어리티 (Common/Rare/Epic) 별 가중치 필드. 업그레이드 항목 목록.
3. Extend `UpgradePool.cs`: 가중 확률 기반 추출 알고리즘. 중복 방지. Pity 시스템 (Epic 미등장 10회 연속 시 보장).
4. Create `UpgradeTableData.asset` ×1: 기본 확률 테이블 (Common:70%, Rare:25%, Epic:5%).
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Required] 확률 값은 SO 필드로 관리. 하드코딩 금지.
- **[CRITICAL]** Scope 외 파일 수정 금지.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Progression/UpgradePool.cs` (확장), `Assets/Data/Progression/UpgradeTableData.cs`, `Assets/Data/Progression/UpgradeTableData.asset`
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
