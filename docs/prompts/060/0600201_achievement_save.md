# [Milestone 060] [jules] [p02] achievement_save_system
- parallel: 
    - [docs/prompts/060/0600102_boss_ai.md](file:///Users/macbook/Desktop/potop/docs/prompts/060/0600102_boss_ai.md)
    - [docs/prompts/060/0600301_overclock_mode.md](file:///Users/macbook/Desktop/potop/docs/prompts/060/0600301_overclock_mode.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Achievement & Save System (06002)
- Background: Implementing persistent data storage and a reward system for player progression.
- Related Systems: JSON Serialization, PlayerPrefs, Game Events
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `SaveManager.cs`: A robust system for saving and loading player progress (e.g., currency, unlocks, settings) using JSON serialization.
3. Implement `AchievementManager.cs`: A system that tracks specific gameplay milestones (e.g., "Kill 100 enemies", "Win without taking damage") and unlocks rewards.
4. Integrate with `EventBroker`: Listen for game events to update achievement progress automatically.
5. Ensure data integrity: Implement basic checksum or validation logic to prevent save file corruption/tampering.
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] Use JSON format for save files to ensure cross-platform compatibility.
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Core/Save/SaveManager.cs`, `Assets/Scripts/Gameplay/Progression/AchievementManager.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the frequency of save operations to avoid performance bottlenecks (e.g., async saving).
- Plan the achievement data structure to allow easy addition of new milestones in the future.
- **Verify Scope Restriction and potential conflicts with parallel tasks p01 and p03.**
</thinking>
<implementation>
- Create `SaveManager.cs` and `AchievementManager.cs` with the designed logic.
</implementation>
<verification>
- [ ] Confirm player data is correctly saved and loaded between sessions.
- [ ] Verify achievements are triggered and progress is updated accurately.
- [ ] Ensure the save file is correctly formatted and passes validation.
- [ ] EOF empty line and encapsulated field naming verified.
- [ ] **Scope Restriction (Save/Achievement scripts only) strictly verified.**
</verification>
</output_format>
