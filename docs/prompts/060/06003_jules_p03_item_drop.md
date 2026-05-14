# [Phase 07] [jules] [p03] 아이템 드랍 시스템
- parallel: [p01](07001_jules_p01_overcharge.md), [p02](07002_jules_p02_tactical_skills.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**. (Jules)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Item Drop System (07003)
- Background: Phase 7 — 수리키트/초강력 자석/스마트 폭탄 아이템 드랍
- Related Systems: PoolManager, EventBroker, WaveManager
- GDD Reference: `02_gameplay_mechanics.md` §아이템 및 파밍
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `ItemDrop.cs`: PoolManager에 등록되는 아이템 베이스. 충돌 시 효과 발동 후 Despawn.
3. Create `ItemDropData.asset` ×3 (SO):
   - **수리키트**: HP 30 회복.
   - **초강력 자석**: 5초간 맵 전체 보석 흡수.
   - **스마트 폭탄**: 화면 내 모든 적 파괴.
4. Create `ItemSpawner.cs`: WaveManager와 연동. 웨이브별 드랍 확률 테이블 (SO). 특수 적 처치 시 보장 드랍.
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Required] PoolManager 통한 Spawn/Despawn만 사용.
- **[CRITICAL]** Scope 외 파일 수정 금지.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Items/ItemDrop.cs`, `ItemSpawner.cs`, `Assets/Data/Items/ItemDropData.asset` ×3
- Reference (Read-only): `Assets/Scripts/Core/PoolManager.cs`, `Assets/Scripts/Core/EventBroker.cs`
</input_data>
