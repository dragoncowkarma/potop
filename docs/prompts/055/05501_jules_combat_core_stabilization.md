# [Phase 055] [jules] 전투 코어 안정화 및 최적화

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game
- Current Module: Combat Core Stabilization (05501)
- Background: Phase 050에서 병렬로 구현된 확률 테이블, 물리 모디파이어(Homing, Bounce 등), 시너지 시스템 간의 결합도가 높아 성능 저하 및 무기 상태 전환(Overdrive) 시 버그 발생 위험이 존재합니다.
- Related Systems: Projectile Pool, IModifier, WeaponBase, MutationSynergyManager
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.
<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. **물리 연산 최적화**: `HomingModifier`의 대상 탐색(FindClosestEnemy)과 `BounceModifier`의 레이캐스트 연산을 매 프레임(Update)에서 틱(Tick, 예: 0.1초~0.2초 단위) 기반 또는 `Physics.OverlapSphereNonAlloc`을 활용한 방식으로 최적화하세요.
3. **메모리 누수 방지**: 투사체가 풀(Pool)로 반환될 때 부착된 모든 `IModifier`가 완벽하게 해제(Remove)되고 초기화되는지 검증 및 보강하세요.
4. **무기 교체 안정화**: `OverdriveEvolution` 트리거 시, 기존 무기(`WeaponBase`)의 이벤트 구독을 해제하고 남은 투사체들을 안전하게 회수(Despawn)한 뒤 궁극 무기로 교체하는 `WeaponTransitionHandler` 로직을 구현하세요.
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not leave unrequested boilerplate, temporary variables, or debug logs.
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring without instruction.
- [Recommended] Implement standard exception handling for each sub-project to prevent crashes.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Weapons/Modifiers/HomingModifier.cs`, `BounceModifier.cs`
- Scope: `Assets/Scripts/Gameplay/Projectile.cs`, `Assets/Scripts/Gameplay/Weapons/Overdrive/OverdriveEvolution.cs`, `Assets/Scripts/Gameplay/Weapons/WeaponTransitionHandler.cs` (New)
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Situation analysis and edge case handling plan
- Verification of `AGENTS.md` compliance
</thinking>

<implementation>
- [Instructions: Use agent tools or Diff format]
</implementation>

<verification>
- [ ] Context/Refactor Tracking verified
- [ ] EOF empty line and comment cleanup completed
- [ ] Magic Numbers removed
</verification>
</output_format>
