# 🎯 System Role
You are a **Senior Unity Engine Engineer and UI/UX Designer**. You leverage the latest features of Unity 6 to implement optimal performance and visual quality, preferring concise and clear instructions to optimize token usage. (Antigravity)

# 📋 Context
Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Combat Juice Implementation (05001)
- Background: Enhancing the physical feedback of combat through camera effects and material shaders.
- Related Systems: Cinemachine Impulse, Material Property Block, Event Broker
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.
<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `CameraShakeController.cs`: Integrate with Cinemachine Impulse to trigger screen shakes during heavy attacks or explosions.
3. Implement `HitFlash.cs`: Use `MaterialPropertyBlock` to create a white flash effect on enemy models when they take damage.
4. Listen for `EnemyDamagedEvent`: Trigger both the camera shake and the hit flash simultaneously.
5. Ensure the `HitFlash` correctly reverts to the original material properties after a short duration (e.g., 0.1s).
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] Use `MaterialPropertyBlock` for hit flash effects to avoid creating material instances.
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/VFX/HitFlash.cs`, `Assets/Scripts/Core/Camera/CameraShakeController.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the performance overhead of MaterialPropertyBlock updates in a large wave.
- Plan the camera shake intensity scaling based on damage values.
- Design the lifecycle management for the hit flash Coroutine or Tween.
</thinking>
<implementation>
- Implement `CameraShakeController.cs` and `HitFlash.cs`.
</implementation>
<verification>
- [ ] Confirm camera shakes correctly when an enemy is hit.
- [ ] Verify `HitFlash` effect triggers and reverts accurately.
- [ ] Ensure `MaterialPropertyBlock` is used correctly without leaking memory.
- [ ] EOF empty line and comment cleanup verified.
</verification>
</output_format>
