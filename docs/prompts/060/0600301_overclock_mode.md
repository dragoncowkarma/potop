# [Milestone 060] [jules] [p03] overclock_mode_implementation
- parallel: 
    - [docs/prompts/060/0600102_boss_ai.md](file:///Users/macbook/Desktop/potop/docs/prompts/060/0600102_boss_ai.md)
    - [docs/prompts/060/0600201_achievement_save.md](file:///Users/macbook/Desktop/potop/docs/prompts/060/0600201_achievement_save.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Overclock Mode (06003)
- Background: Implementing a high-difficulty game mode with increased speed and enemy density.
- Related Systems: Gameplay Loop, Enemy Spawner, Difficulty Scaling
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `OverclockMode.cs`: A game mode controller that modifies game parameters (e.g., global speed multiplier, enemy spawn rates, turret fire rates) when active.
3. Add Activation Logic: Implement a UI-triggered or condition-based activation (e.g., reaching wave 50).
4. Integrate with `Time.timeScale`: Manage global game speed safely without breaking frame-rate independent logic.
5. Apply visual filters: Communicate with Antigravity to trigger a screen-space effect (e.g., chromatic aberration) during Overclock mode.
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] Ensure all speed-related changes are frame-rate independent (use `Time.deltaTime`).
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Modes/OverclockMode.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the impact of increasing `Time.timeScale` on physics stability and navigation.
- Plan the parameter scaling logic to ensure the game remains playable (though difficult) in Overclock mode.
- **Verify Scope Restriction and potential conflicts with parallel tasks p01 and p02.**
</thinking>
<implementation>
- Create `OverclockMode.cs` with the designed scaling and activation logic.
</implementation>
<verification>
- [ ] Confirm Overclock mode correctly modifies targeted game parameters.
- [ ] Verify game speed increases consistently across all systems.
- [ ] Ensure the mode can be deactivated correctly, restoring original values.
- [ ] EOF empty line and naming conventions verified.
- [ ] **Scope Restriction (Overclock script only) strictly verified.**
</verification>
</output_format>
