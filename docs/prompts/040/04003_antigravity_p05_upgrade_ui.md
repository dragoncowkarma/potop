# 🎯 System Role
You are a **Senior Unity Engine Engineer and UI/UX Designer**. You leverage the latest features of Unity 6 to implement optimal performance and visual quality, preferring concise and clear instructions to optimize token usage. (Antigravity)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Upgrade UI Implementation (04003)
- Background: Implementing the card selection interface that appears upon leveling up.
- Related Systems: UI Toolkit (UXML/USS), LevelManager Events
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.
<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Create `UpgradeMenu.uxml`: Design a layout with three upgrade card slots using UI Toolkit.
3. Implement `UpgradeUIController.cs`: Manage the display logic of the upgrade menu.
4. Listen for `LevelUpEvent`: Automatically pause the game (`Time.timeScale = 0`) and show the upgrade menu.
5. Implement Card Selection: On clicking a card, apply the selected modifier to the weapon and resume the game (`Time.timeScale = 1`).
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] Use UI Toolkit (UXML/USS) for all UI elements.
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/UI/UpgradeMenu.uxml`, `Assets/Scripts/UI/UpgradeUIController.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the card animation sequence using USS transitions for a premium feel.
- Plan the data binding for upgrade card info (name, description, icon).
- Design the input blocking logic while the upgrade menu is active.
</thinking>
<implementation>
- Create the UXML/USS files and the controller script.
</implementation>
<verification>
- [ ] Confirm `UpgradeMenu` appears correctly upon a `LevelUpEvent`.
- [ ] Verify game time is paused while the menu is open.
- [ ] Ensure selecting an upgrade card correctly applies the modifier and resumes the game.
- [ ] EOF empty line and UI Toolkit standards verified.
</verification>
</output_format>
