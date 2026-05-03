# [Milestone 040] [jules] [p05] projectile_mutation_logic
- parallel: 
    - [docs/prompts/040/0400101_weapon_architecture.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400101_weapon_architecture.md)
    - [docs/prompts/040/0400102_weapon_logic.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400102_weapon_logic.md)
    - [docs/prompts/040/0400201_mutation_synergy.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400201_mutation_synergy.md)
    - [docs/prompts/040/0400301_xp_leveling.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400301_xp_leveling.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Projectile Mutation Logic (04004)
- Background: Implementing complex projectile behaviors influenced by active mutations.
- Related Systems: Projectile Physics, Modifier System, VFX
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `MutatedProjectile.cs`: A projectile class capable of handling trajectory and behavior changes based on applied modifiers.
3. Implement 'Homing' Mutation: Add logic to steer the projectile towards the nearest enemy.
4. Implement 'Ricochet' Mutation: Add logic to bounce off surfaces or enemies to find new targets.
5. Integrate with the `ModifierSystem` to fetch and apply these behaviors dynamically upon instantiation.
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
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Combat/Projectiles/MutatedProjectile.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the performance impact of frequent `Update` calculations for homing projectiles.
- Plan the collision logic for ricochets to prevent infinite loops or unintended overlaps.
- **Verify Scope Restriction and potential conflicts with parallel tasks p01-p04.**
</thinking>
<implementation>
- Create `MutatedProjectile.cs` and implement the trajectory mutation logic.
</implementation>
<verification>
- [ ] Confirm homing projectiles correctly steer towards active enemies.
- [ ] Verify ricochet projectiles bounce correctly upon collision.
- [ ] Ensure modifiers are correctly applied to the projectile instance.
- [ ] EOF empty line and performance audit completed.
- [ ] **Scope Restriction (Mutated projectile only) strictly verified.**
</verification>
</output_format>
