# [Milestone 045] [jules] [p03] data_ui_cleanup
- parallel: None

---

# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Phase 4.5 Core Loop Refactor - Data & UI Cleanup (04503)
- Background: `EXPGemData` (ScriptableObject) is defined inside `EXPGem.cs`, violating SRP. `UpgradeSelectController.cs` uses `Object.FindFirstObjectByType<LevelingManager>()` in `Awake`, which is a performance and architecture risk.
- Related Systems: EXPGem, LevelingManager, EventBroker, Upgrade UI.
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Extract `EXPGemData` into its own file (`Assets/Scripts/Data/Items/EXPGemData.cs`) and update any namespace/using statements if necessary.
3. Refactor `UpgradeSelectController.cs` to remove `Object.FindFirstObjectByType<LevelingManager>()`. Inject the dependency via the Inspector (`[SerializeField]`) or decouple it completely using the EventBroker.
4. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not leave unrequested boilerplate, temporary variables, or debug logs.
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring without instruction.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
Target Files:
- `Assets/Scripts/Gameplay/Items/EXPGem.cs`
- `Assets/Scripts/Data/Items/EXPGemData.cs` (New)
- `Assets/Scripts/UI/UpgradeSelectController.cs`
- Scene/Prefab files referencing `UpgradeSelectController` if Inspector injection is chosen.
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Situation analysis and edge case handling plan (Handling null references in UI Controller).
- **Verification of Scope Restriction and potential conflicts with other parallel tasks**
- Verification of `AGENTS.md` compliance
</thinking>
<implementation>
- [Instructions: Use agent tools or Diff format]
</implementation>
<verification>
- [ ] Context/Refactor Tracking verified
- [ ] EOF empty line and comment cleanup completed
- [ ] Magic Numbers removed
- [ ] **Scope Restriction (No modification outside assigned files) verified**
</verification>
</output_format>