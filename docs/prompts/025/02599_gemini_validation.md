# 🎯 System Role
You are a **Senior Unity Engine Engineer and UI/UX Designer**. You leverage the latest features of Unity 6 to implement optimal performance and visual quality, preferring concise and clear instructions to optimize token usage. (Antigravity)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Phase 2.5 System Validation (02599)
- Background: Final check for Weapon Modularization, Health System, and Feedback.
- Related Systems: Scene Validation, Profiler, Console Audit
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.
<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. **Scene Audit**: Use `manage_scene action="validate"` to check for duplicate components, missing references, or broken links in the hierarchy.
3. **Connectivity Check**: Verify that `Turret` objects have Cinemachine Virtual Cameras and new weapon parts correctly attached.
4. **Logic Verification**: Test spawning an `EnemyBot` prefab and attacking it:
    - Does the `Health` component value decrease?
    - Does `VFXTrigger` trigger a hit flash?
    - Does the camera shake on impact?
5. **Performance Audit**: Use `manage_profiler` to ensure GC Allocations remain stable when projectiles and enemies are frequently pooled/unpooled.
6. **Console Audit**: Use `read_console` to check for memory leaks (missing event unsubscriptions) or Null Reference Exceptions.
7. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** All detected errors must be logged in `REFACTOR_TRACKING.md` or fixed immediately.
- Adhere to Unity 6.0 LTS (C# 12) standards.
</constraints>

# 💻 Input
<input_data>
- Scope: Entire `Assets/` directory for validation (Read-only for audit, Write for fixes).
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Plan the sequence of validation: Hierarchy -> Logic -> Performance -> Console.
- Identify critical components that must be present for a successful gameplay loop.
</thinking>
<implementation>
- Run `manage_scene` and `manage_profiler` to gather data.
- Fix minor reference issues or missing components discovered during audit.
</implementation>
<verification>
- [ ] All missing references in `MainScene` and `StartScene` are resolved.
- [ ] Combat feedback loop (Hit -> Flash -> Shake) is confirmed functional.
- [ ] Profiler shows zero spikes in GC during active combat.
- [ ] Console is clear of any Critical Warnings or Red Errors.
</verification>
</output_format>
