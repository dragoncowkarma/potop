# [Milestone 030] [jules] [p01] wave_management_system
- parallel: 
    - [docs/prompts/030/0300201_enemy_base_refactor.md](file:///Users/macbook/Desktop/potop/docs/prompts/030/0300201_enemy_base_refactor.md)
    - [docs/prompts/030/0300202_enemy_variant_logic.md](file:///Users/macbook/Desktop/potop/docs/prompts/030/0300202_enemy_variant_logic.md)

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Wave Management System (03001)
- Background: Phase 3 Gameplay Loop
- Related Systems: Event Broker, Spawner System
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `WaveManager.cs`: Manage `WaveData` list, handle current wave index and timers.
3. Publish `WaveStartedEvent` and `WaveCompletedEvent` via `EventBroker`.
4. Implement `WaveData.cs`: Define enemy types and counts using `ScriptableObject`.
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
- Scope: `Assets/Scripts/Gameplay/Wave/WaveManager.cs`, `Assets/Scripts/Gameplay/Wave/WaveData.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze `WaveManager` singleton pattern and `EventBroker` integration.
- Plan delta-time handling for timer logic and wave completion conditions.
- **Verify Scope Restriction and potential conflicts with parallel tasks p02/p03.**
</thinking>
<implementation>
- Create `WaveManager` and `WaveData` base structures using `create_script`.
</implementation>
<verification>
- [ ] Confirm `WaveData` assets allow inspector editing of enemy lists.
- [ ] Verify wave start/end events reach other systems (UI, Spawner).
- [ ] EOF empty line and magic number extraction verified.
- [ ] **Scope Restriction (Wave-related files only) strictly verified.**
</verification>
</output_format>
