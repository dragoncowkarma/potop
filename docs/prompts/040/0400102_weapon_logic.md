# [Milestone 040] [jules] [p02] weapon_core_logic
- parallel: 
    - [docs/prompts/040/0400101_weapon_architecture.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400101_weapon_architecture.md)
    - [docs/prompts/040/0400201_mutation_synergy.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400201_mutation_synergy.md)
    - [docs/prompts/040/0400301_xp_leveling.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400301_xp_leveling.md)
    - [docs/prompts/040/0400401_projectile_mutation.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400401_projectile_mutation.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Modular Weapon Architecture (04001)
- Background: Implementation of specific weapon types based on the new architecture.
- Related Systems: Hitscan Weapon, Projectile Weapon, Weapon Controller
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `HitscanWeapon.cs`: Inherit from `WeaponBase` and implement raycast-based shooting logic.
3. Implement `ProjectileWeapon.cs`: Inherit from `WeaponBase` and implement physical projectile spawning logic.
4. Implement `WeaponController.cs`: Manage weapon switching, aiming behavior, and input mapping for the player turret.
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
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Weapons/HitscanWeapon.cs`, `Assets/Scripts/Gameplay/Weapons/ProjectileWeapon.cs`, `Assets/Scripts/Gameplay/Weapons/WeaponController.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the performance implications of Raycast vs Projectile for high-frequency fire.
- Plan the weapon switching sequence to ensure a smooth transition and animation synchronization.
- **Verify Scope Restriction and potential conflicts with parallel tasks p01, p03-p05.**
</thinking>
<implementation>
- Implement the specialized weapon classes and the controller logic.
</implementation>
<verification>
- [ ] Confirm `HitscanWeapon` correctly detects hits on enemies.
- [ ] Verify `ProjectileWeapon` instantiates the correct projectile prefab.
- [ ] Ensure `WeaponController` switches weapons without state corruption.
- [ ] EOF empty line and comment cleanup completed.
- [ ] **Scope Restriction (Weapon logic files only) strictly verified.**
</verification>
</output_format>
