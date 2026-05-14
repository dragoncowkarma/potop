# [Phase 3.5] [jules] [p02] 피버 디커플링
- parallel: [p01](03501_jules_p01_enemy_ai.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. (Jules)

# 📋 Context
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Fever Decoupling (03502)
- Background: Phase 3.5 — FeverManager를 EventBroker 기반으로 리팩토링, 콤보 계산 분리
- Related Systems: FeverManager (Phase 3), EventBroker (Phase 2), ComboSystem
- GDD Reference: `02_gameplay_mechanics.md` §콤보/피버 시스템
</context>

# 🛠️ Task
> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**

<task>
1. Read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md`.
2. Refactor `FeverManager.cs`: 직접 참조 대신 EventBroker 이벤트 기반으로 전환.
3. 콤보 계산 로직을 `ComboCalculator.cs`로 분리 (SRP 원칙).
4. 피버 레벨(1~5)별 EventBroker 이벤트 발행: `OnFeverLevelChanged(int level)`.
5. After completion, resolve "FeverManager Decoupling" entry in `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints
<constraints>
- [Required] EOF 1줄, Namespace: `Potop.Client.Gameplay`.
- [Required] 기존 FeverManager public API 유지. 호출부 수정 최소화.
- **[CRITICAL]** Scope 외 파일 수정 금지.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Combat/FeverManager.cs` (수정), `Assets/Scripts/Gameplay/Combat/ComboCalculator.cs` (신규)
- Reference (Read-only): `Assets/Scripts/Core/EventBroker.cs`
</input_data>
