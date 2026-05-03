# [Milestone 040] [jules] [p03] mutation_synergy_system
- parallel: 
    - [docs/prompts/040/0400101_weapon_architecture.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400101_weapon_architecture.md)
    - [docs/prompts/040/0400102_weapon_logic.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400102_weapon_logic.md)
    - [docs/prompts/040/0400301_xp_leveling.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400301_xp_leveling.md)
    - [docs/prompts/040/0400401_projectile_mutation.md](file:///Users/macbook/Desktop/potop/docs/prompts/040/0400401_projectile_mutation.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Mutation Synergy System (04002)
- Background: Implementing hidden synergies between combined weapon mutations.
- Related Systems: Modifier System, Weapon Stats
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `MutationSynergyManager.cs`: A system that detects combinations of active modifiers on a weapon and applies additional synergy bonuses.
3. Design the Synergy Rule Table: Define how specific pairs of mutations (e.g., 'Fire' + 'Explosion') result in unique synergy effects (e.g., 'Lava Trail').
4. Integrate with the weapon modifier system to inject these additional bonuses during the stat calculation phase.
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
- Scope: `Assets/Scripts/Gameplay/Weapons/Modifiers/MutationSynergyManager.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the data structure for storing synergy rules (Dictionary vs ScriptableObject).
- Plan the evaluation loop for modifiers to minimize performance overhead during frequent updates.
- **Verify Scope Restriction and potential conflicts with parallel tasks p01-p02, p04-p05.**
</thinking>
<implementation>
- Create `MutationSynergyManager.cs` and implement the rule evaluation logic.
</implementation>
<verification>
- [ ] Confirm synergy bonuses are correctly applied when the required modifiers are present.
- [ ] Verify synergy bonuses are removed when modifiers are unequipped.
- [ ] Ensure the system handles multiple simultaneous synergies correctly.
- [ ] EOF empty line and encapsulated field naming verified.
- [ ] **Scope Restriction (Synergy manager only) strictly verified.**
</verification>
</output_format>
