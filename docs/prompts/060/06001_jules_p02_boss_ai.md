# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Titan Core AI Implementation (06001)
- Background: Implementing a complex state machine for the final boss encounter.
- Related Systems: StateMachine, Boss Architecture, Damage System
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `TitanCoreAI.cs`: Use a State Machine pattern to manage boss phases (Idle, Combat, Enraged, Death).
3. Handle Phase Transitions: Trigger visual and mechanical changes based on boss health thresholds.
4. Integrate with `Hitbox`: Ensure damage received from multiple hitboxes is aggregated correctly in the boss's health controller.
5. Implement complex attack patterns: E.g., AOE stomp, laser sweep, and minion spawning.
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] Use an extensible State Machine pattern (e.g., interface-based or enum-driven states).
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Boss/TitanCoreAI.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the state transition conditions to ensure the boss doesn't get stuck in a state.
- Plan the interaction between the AI and the visual components managed by Antigravity.
- **Verify Scope Restriction and potential conflicts with parallel tasks p02 and p03.**
</thinking>
<implementation>
- Create `TitanCoreAI.cs` with the designed state machine and phase logic.
</implementation>
<verification>
- [ ] Confirm boss states transition correctly based on health thresholds.
- [ ] Verify attack patterns are executed as intended.
- [ ] Ensure damage is correctly registered from all hitboxes.
- [ ] EOF empty line and naming conventions verified.
- [ ] **Scope Restriction (Boss AI script only) strictly verified.**
</verification>
</output_format>
