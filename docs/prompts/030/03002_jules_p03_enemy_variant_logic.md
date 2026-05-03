# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Specialized Enemy Variants (03002)
- Background: Phase 3 Gameplay Loop
- Related Systems: Enemy AI, Variant Logic
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `BlitzEnemy.cs`: High movement speed, low health. Inherit from `EnemyBase`.
3. Implement `ArmoredEnemy.cs`: High health, low movement speed, knockback resistance. Inherit from `EnemyBase`.
4. Implement `SwarmEnemy.cs`: Low health, designed for group spawning. Inherit from `EnemyBase`.
5. Ensure each variant correctly overrides relevant `EnemyBase` methods for unique behavior.
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
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
- Scope: `Assets/Scripts/Gameplay/AI/Variants/BlitzEnemy.cs`, `Assets/Scripts/Gameplay/AI/Variants/ArmoredEnemy.cs`, `Assets/Scripts/Gameplay/AI/Variants/SwarmEnemy.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the base class dependencies for variants.
- Plan the override of `TakeDamage` or movement logic for specialized traits (e.g., armor reduction).
- **Verify Scope Restriction and potential conflicts with parallel tasks p01/p02.**
</thinking>
<implementation>
- Create concrete classes for each enemy variant inheriting from `EnemyBase`.
</implementation>
<verification>
- [ ] Confirm `BlitzEnemy` moves faster than the base enemy.
- [ ] Verify `ArmoredEnemy` health reduction is consistent with armor logic.
- [ ] EOF empty line and comment cleanup completed.
- [ ] **Scope Restriction (Enemy variant files only) strictly verified.**
</verification>
</output_format>
