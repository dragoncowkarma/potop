# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Weapon Parts Architecture (02501)
- Background: Phase 2.5 Foundation & Modularization
- Related Systems: Strategy Pattern, ScriptableObject Data
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Define `IWeapon` interface: Include `Fire()`, `Reload()`, and `UpdateState()`.
3. Create `WeaponBase` abstract class: Load base stats (Damage, FireRate, ProjectileSpeed) from `ScriptableObject`.
4. Implement Part System: Separate `WeaponBody`, `WeaponBarrel`, and `WeaponMagazine` classes. Design them to apply stat weights and special effects (e.g., spread reduction, penetration) based on `ScriptableObject` data.
5. Apply Strategy Pattern: Decouple firing logic (`FireStrategy`) to allow runtime switching between 'Straight', 'Spread', and 'Lob' fire types.
6. Publish events for firing and reloading via `EventBroker`.
7. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
- Performance: Minimize `Update` calls and GC allocations.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Gameplay/Weapons/IWeapon.cs`, `Assets/Scripts/Gameplay/Weapons/WeaponBase.cs`, `Assets/Scripts/Gameplay/Weapons/Parts/`, `Assets/Scripts/Gameplay/Weapons/Strategies/`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the modularity of weapon parts and SO data injection.
- Design the Strategy Pattern for firing logic to ensure runtime swap stability.
- **Verify Scope Restriction and potential conflicts with parallel tasks p02/p03.**
</thinking>
<implementation>
- Create scripts for `IWeapon`, `WeaponBase`, and specialized part classes.
- Implement the `FireStrategy` abstraction and concrete strategies.
</implementation>
<verification>
- [ ] Confirm weapon parts correctly modify base stats when equipped.
- [ ] Verify `FireStrategy` can be swapped at runtime without errors.
- [ ] Ensure firing events are published correctly to the `EventBroker`.
- [ ] **Scope Restriction (Weapon files only) strictly verified.**
</verification>
</output_format>
