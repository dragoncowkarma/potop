# [Milestone 025] [jules] [p03] health_system_integration
- parallel: 
    - [docs/prompts/025/0250101_weapon_architecture.md](file:///Users/macbook/Desktop/potop/docs/prompts/025/0250101_weapon_architecture.md)
    - [docs/prompts/025/0250201_damage_api.md](file:///Users/macbook/Desktop/potop/docs/prompts/025/0250201_damage_api.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Health System & Entity Integration (02502)
- Background: Phase 2.5 Foundation & Modularization
- Related Systems: Health Component, EnemyBot, Projectile
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `Health.cs`: A universal component for all attackable objects. Manage `MaxHealth` and `CurrentHealth`.
3. Publish `OnHealthChanged`, `OnDeath`, and `OnDamaged` events via `EventBroker`.
4. Refactor `EnemyBot.cs`: Remove internal health variables and use the `Health` component instead. Implement `IDamageable` to call `Health.TakeDamage`.
5. Refactor `Projectile.cs`: Update collision logic to find `IDamageable` and call `TakeDamage` upon impact.
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
- Encapsulation: Ensure all fields are properly encapsulated with `SerializedField`.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Combat/Health.cs`, `Assets/Scripts/Gameplay/AI/EnemyBot.cs`, `Assets/Scripts/Gameplay/Combat/Projectile.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the event publishing order (OnDamaged vs OnDeath) for visual feedback timing.
- Plan the dependency injection for `Health` within `EnemyBot`.
- **Verify Scope Restriction and potential conflicts with parallel tasks p01/p02.**
</thinking>
<implementation>
- Create `Health.cs` and refactor `EnemyBot.cs` and `Projectile.cs`.
</implementation>
<verification>
- [ ] Confirm `Health` component correctly tracks damage and triggers death events.
- [ ] Verify `Projectile` successfully transfers damage to `IDamageable` targets.
- [ ] EOF empty line and encapsulated field naming verified.
- [ ] **Scope Restriction (Specified combat/AI files only) strictly verified.**
</verification>
</output_format>
