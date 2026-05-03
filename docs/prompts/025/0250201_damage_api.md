# [Milestone 025] [jules] [p02] damage_system_api
- parallel: 
    - [docs/prompts/025/0250101_weapon_architecture.md](file:///Users/macbook/Desktop/potop/docs/prompts/025/0250101_weapon_architecture.md)
    - [docs/prompts/025/0250202_health_system.md](file:///Users/macbook/Desktop/potop/docs/prompts/025/0250202_health_system.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Damage System API (02502)
- Background: Phase 2.5 Foundation & Modularization
- Related Systems: IDamageable, DamageInfo
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Define `IDamageable` interface: Include `void TakeDamage(DamageInfo info)`.
3. Define `DamageInfo` struct: Include `int Amount`, `Vector3 HitPoint`, `Vector3 HitNormal`, `GameObject Instigator`, and `DamageType Type`.
4. Define `DamageType` enum: Include basic types like `Normal`, `Fire`, `Electric`, and `Explosive`.
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
- Standard: Adhere to Unity 6.0 LTS (C# 12) standards.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Combat/IDamageable.cs`, `Assets/Scripts/Gameplay/Combat/DamageInfo.cs`, `Assets/Scripts/Gameplay/Combat/DamageType.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the extensibility of `DamageInfo` for future status effects.
- Ensure the `IDamageable` interface is minimal and easy to implement.
- **Verify Scope Restriction and potential conflicts with parallel tasks p01/p03.**
</thinking>
<implementation>
- Create scripts for `IDamageable`, `DamageInfo`, and `DamageType`.
</implementation>
<verification>
- [ ] Confirm `DamageInfo` struct is properly balanced for performance (stack vs heap).
- [ ] Verify `DamageType` includes all required base types.
- [ ] **Scope Restriction (Damage API files only) strictly verified.**
</verification>
</output_format>
