# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Modular Weapon Architecture (04001)
- Background: Phase 4 Progression & Mutation
- Related Systems: Interface Design, Modifier System
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Design `IWeapon` interface: Include `Fire()`, `Reload()`, and `StopFire()`.
3. Create `WeaponBase.cs` abstract class: Implement common logic for cooldowns and ammunition management.
4. Design a Modifier System structure: Allow for seamless integration with external buffs and mutations.
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
- Scope: `Assets/Scripts/Gameplay/Weapons/IWeapon.cs`, `Assets/Scripts/Gameplay/Weapons/WeaponBase.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the flexibility of the `IWeapon` interface for both hitscan and projectile weapons.
- Plan the internal state management in `WeaponBase` for high-performance reloading.
- **Verify Scope Restriction and potential conflicts with parallel tasks p02-p05.**
</thinking>
<implementation>
- Create `IWeapon.cs` and `WeaponBase.cs` with the designed architecture.
</implementation>
<verification>
- [ ] Confirm `WeaponBase` correctly handles fire rate cooldowns.
- [ ] Verify `IWeapon` methods are accessible and implementable by derived classes.
- [ ] EOF empty line and comment cleanup completed.
- [ ] **Scope Restriction (Weapon interface files only) strictly verified.**
</verification>
</output_format>
