# 🎯 System Role
You are a **Senior Software Engineer with 10 years of experience**, specializing in perfect architectural design and optimization for the 'POTOP' project. Your code is scalable, handles edge cases, and strictly adheres to the project conventions defined in `AGENTS.md`. (Jules)

# 📋 Context
> [!IMPORTANT]
> **필수 참조**: 작업 전 반드시 [`AGENTS.md`](../../../AGENTS.md) + [`potop_client/AGENTS.md`](../../../potop_client/AGENTS.md)를 숙지하세요.

Before starting, read `../SUMMARY.xml` and `../../REFACTOR_TRACKING.md` to understand the current context.
<context>
- Project Goal: 3D Roguelite Turret Defense Game (Mobile/PC/VR/Console)
- Current Module: Sound Manager Implementation (05002)
- Background: Implementing a centralized audio management system with pooling for sound effects.
- Related Systems: PoolManager, AudioSource, Global Event Broker
</context>

# 🛠️ Task
Perform the following instructions according to the `AGENTS.md` process.

> [!CAUTION]
> **Mandatory Parallel Rule: Scope Restriction**
> This task is processed in parallel. Do NOT modify, format, or access any files outside the `input_data` specified below.

<task>
1. Read `../SUMMARY.xml` to check the current scope and identify potential overlaps; identify relevant entries in `../../REFACTOR_TRACKING.md`.
2. Implement `SoundManager.cs`: A singleton manager for playing 2D/3D sound effects and background music.
3. Implement `AudioPool.cs`: Integrate with `PoolManager` to recycle `AudioSource` components for high-frequency SFX (e.g., gunshots, explosions).
4. Provide a simple API: `PlaySFX(AudioClip clip, Vector3 position)`, `PlayBGM(AudioClip clip)`.
5. Integrate with `EventBroker`: Listen for UI click events or combat events to trigger associated sounds automatically.
6. After completion, remove resolved items from `../../REFACTOR_TRACKING.md`.
</task>

# ⚠️ Constraints (POTOP Global Standards)
Code violating these rules will NOT be considered `Done`.
<constraints>
- [Required] Use an object pooling system for `AudioSource` to prevent runtime allocations.
- [Required] EXACTLY one empty line at the end of every file (EOF).
- [Required] Comments must explain "Why" (intent) rather than "What" (action).
- [Prohibited] Do not use magic numbers; extract them into constants or config variables.
- [Prohibited] Do not change existing function signatures or perform large-scale refactoring.
- **[CRITICAL]** Any modification outside the specified `input_data` will result in immediate rejection.
</constraints>

# 💻 Input
<input_data>
- Scope: `Assets/Scripts/Core/Audio/SoundManager.cs`, `Assets/Scripts/Core/Audio/AudioPool.cs`
</input_data>

# 📝 Output Format
Generate your response strictly following the structure below (including XML tags).
<output_format>
<thinking>
- Analyze the concurrency of sound effects to determine the optimal pool size.
- Plan the volume management logic (Master, BGM, SFX channels) for future settings integration.
- **Verify Scope Restriction and potential conflicts with parallel task p02.**
</thinking>
<implementation>
- Create `SoundManager.cs` and `AudioPool.cs` with the designed pooling logic.
</implementation>
<verification>
- [ ] Confirm `SoundManager` correctly plays 2D and 3D sounds.
- [ ] Verify `AudioSource` objects are correctly returned to the pool after playback.
- [ ] Ensure BGM transitions smoothly when a new track is played.
- [ ] EOF empty line and encapsulated field naming verified.
- [ ] **Scope Restriction (Audio scripts only) strictly verified.**
</verification>
</output_format>
