# [Milestone 040] [jules] [p04] xp_leveling_system
- parallel: 
    - [docs/prompts/040/0400101_weapon_architecture.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400101_weapon_architecture.md)
    - [docs/prompts/040/0400102_weapon_logic.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400102_weapon_logic.md)
    - [docs/prompts/040/0400201_mutation_synergy.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400201_mutation_synergy.md)
    - [docs/prompts/040/0400401_projectile_mutation.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400401_projectile_mutation.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Player XP & Leveling System (04003)
- Background: Managing player progression through XP acquisition and level-up events.
- Related Systems: Enemy Death Events, Upgrade UI, Data Balance
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `LevelManager.cs`: Manage player XP and current level.
3. Design the Level Curve: Implement a mathematical model (e.g., exponential) for increasing XP requirements per level.
4. Integrate with `EventBroker`: Publish `LevelUpEvent` when the XP threshold is reached to trigger the Upgrade UI.
5. Listen for `EnemyKilledEvent` (or similar) to automatically add XP based on the enemy type.
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
- Scope: `Assets/Scripts/Gameplay/Progression/LevelManager.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the XP distribution balance for early vs late game.
- Plan the decoupling between `LevelManager` and UI to ensure the game logic remains independent.
- **Verify Scope Restriction and potential conflicts with parallel tasks p01-p03, p05.**
</thinking>
<implementation>
- Create `LevelManager.cs` and implement the XP/Leveling logic.
</implementation>
<verification>
- [ ] Confirm XP increases correctly when receiving kill events.
- [ ] Verify `LevelUpEvent` is published exactly when the threshold is crossed.
- [ ] Ensure the XP curve values match the intended progression speed.
- [ ] EOF empty line and comment cleanup completed.
- [ ] **Scope Restriction (Level manager only) strictly verified.**
</verification>
</output_format>
