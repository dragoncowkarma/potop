# 🎯 System Role
You are a **Senior Unity Engine Engineer and UI/UX Designer**. You leverage the latest features of Unity 6 to implement optimal performance and visual quality, preferring concise and clear instructions to optimize token usage. (Antigravity)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Phase 4.5 Core Loop Refactor - Feedback & Polish (04504)
- Background: `CameraShakeController` is not integrated into the combat loop (e.g., triggered on impact or heavy firing). `VFXTrigger` uses a Coroutine for despawning, causing GC allocation overhead.
- Related Systems: CameraShakeController, VFXTrigger, PoolManager, Combat Events.
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.
<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. For complex logic or architectural changes, propose an `implementation_plan.md` for approval before modifying code.
3. Refactor `VFXTrigger.cs` to remove Coroutine despawn logic. Register it with the `PoolManager` and use an async/await pattern (`UniTask` or Unity 6 `Awaitable`) or a standard timer in `Update` to handle despawning efficiently.
4. Hook `CameraShakeController` into the combat loop via the EventBroker or direct Weapon/Projectile callbacks so that heavy hits or explosive impacts trigger the shake.
5. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
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
- **[CRITICAL]** PoolManager integration: Ensure VFX components (ParticleSystem) are properly reset (`Stop()`, `Clear()`) before returning to the pool.
</constraints>

# 💻 Input
<input_data>
Target Files:
- `Assets/Scripts/Gameplay/Feedback/CameraShakeController.cs`
- `Assets/Scripts/Gameplay/Feedback/VFXTrigger.cs`
- Event definitions for combat impact.
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Situation analysis and edge case handling plan (VFX cleanup on pooling, Camera Shake intensity overlap).
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