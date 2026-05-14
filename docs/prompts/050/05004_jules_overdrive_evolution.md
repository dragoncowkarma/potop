# [Phase 06] [jules] 궁극 진화 시스템

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Overdrive Evolution System (06004)
- Background: Phase 6 — 최종 시너지 완성 + 보스 상자 획득 시 궁극 진화 무기 발동
- Related Systems: MutationSynergyManager (06002), WeaponBase, EventBroker
- GDD Reference: `02_gameplay_mechanics.md` §궁극 진화 (Overdrive Evolution)
</context>

# 🛠️ Task
<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Create `OverdriveEvolution.cs`: MutationSynergyManager에서 시너지 완성 상태 감지. 보스 상자 아이템 획득 시 진화 트리거.
3. 3종 궁극 무기 구현:
   - **오비탈 스트라이크** (폭발+거대화): 광범위 궤도 폭격 패턴. `OrbitalStrikeWeapon.cs`.
   - **프리즘 체인** (다연발+도탄): 추적 전이 레이저. `PrismChainWeapon.cs`.
   - **개틀링 레일건** (관통+공속): 초고속 무한 관통. `GatlingRailgunWeapon.cs`.
4. 각 궁극 무기의 `OverdriveData.asset` SO 생성 (스탯, 프리팹 참조, 시너지 조건).
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Required] 궁극 진화는 한 번만 발동 (중복 진화 방지 플래그).
- [Required] 기존 WeaponBase 시그니처 변경 금지. 궁극 무기도 WeaponBase 상속.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Weapons/Overdrive/OverdriveEvolution.cs`, `OrbitalStrikeWeapon.cs`, `PrismChainWeapon.cs`, `GatlingRailgunWeapon.cs`, `Assets/Data/Weapons/Overdrive/` (SO 에셋)
- Reference (Read-only): `Assets/Scripts/Gameplay/Weapons/Modifiers/MutationSynergyManager.cs`, `WeaponBase.cs`
</input_data>
