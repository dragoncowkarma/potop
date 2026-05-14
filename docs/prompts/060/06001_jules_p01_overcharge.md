# [Phase 07] [jules] [p01] 오버차지 시스템
- parallel: [p02](07002_jules_p02_tactical_skills.md), [p03](07003_jules_p03_item_drop.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Overcharge System (07001)
- Background: Phase 7 — 수동 조작으로 공격 속도 200% 과부하, 게이지 과열 시 3초 패널티
- Related Systems: WeaponBase, EventBroker (`OnOverchargeStateChanged`)
- GDD Reference: `02_gameplay_mechanics.md` §오버차지 시스템
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `OverchargeController.cs`:
   - 게이지: 0~100 float. 버튼 유지 중 매초 게이지 +20 & 공속 2배 버프.
   - 과열: 게이지 100 도달 → 3초 Overheat → 사격 중단 → 게이지 0 초기화.
   - 버튼 해제 시 게이지 매초 -15 자연 감소.
3. EventBroker 이벤트: `OnOverchargeStateChanged(OverchargeState state)` (Idle/Active/Overheat).
4. 게이지 수치(충전 속도, 감소 속도, 과열 시간)는 모두 SO 필드로 외부화.
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Prohibited] Update()에서 입력 직접 감지 금지. Input System Action 콜백만 사용.
- **[CRITICAL]** Scope 외 파일 수정 금지.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Combat/OverchargeController.cs`, `Assets/Data/Combat/OverchargeData.asset`
- Reference (Read-only): `Assets/Scripts/Gameplay/Weapons/WeaponBase.cs`, `Assets/Scripts/Core/EventBroker.cs`
</input_data>
