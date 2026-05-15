# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Phase 4.5 Core Loop Refactor - Projectile Features (04502)
- Background: `NovaWeapon` and `JuggernautWeapon` lack AoE, Pierce, and Knockback logic in the `Projectile` class (currently marked as TODO). This ruins the weapon variety promised in Phase 4.
- Related Systems: Projectile, Physics/Collision, PoolManager.
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.
<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. For complex logic or architectural changes, propose an `implementation_plan.md` for approval before modifying code.
3. Update `Projectile.cs` (or specific weapon scripts) to support:
   - **AoE (Area of Effect)**: Use `Physics.OverlapSphere` (or 2D equivalent based on project physics settings). Filter by Enemy layer.
   - **Pierce**: Add a pierce counter that decrements on hit. Destroy projectile only when it reaches 0.
   - **Knockback**: Apply impulse force to enemies with a Rigidbody component upon hit.
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
- [Recommended] Implement standard exception handling for each sub-project to prevent crashes.
- **[CRITICAL]** Exception Handling: Ensure Knockback handles targets that are destroyed simultaneously by the damage application.
</constraints>

# 💻 Input
<input_data>
Target Files:
- `Assets/Scripts/Gameplay/Weapons/Projectile.cs`
- `Assets/Scripts/Gameplay/Weapons/NovaWeapon.cs`
- `Assets/Scripts/Gameplay/Weapons/JuggernautWeapon.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Situation analysis and edge case handling plan (Layer filtering for AoE, null checks for Knockback).
- Verification of `AGENTS.md` compliance.
</thinking>

<implementation>
- [Instructions: Use agent tools or Diff format]
</implementation>

<verification>
- [ ] Context/Refactor Tracking verified
- [ ] EOF empty line and comment cleanup completed
- [ ] Magic Numbers removed
</verification>
</output_format>