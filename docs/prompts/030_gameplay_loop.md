# Phase 3: 게임플레이 루프 및 AI 기초 - AI Prompts

### 1. [Jules] [Parallel Execution] Gameplay Loop & AI
**[Scope A: Assets/Scripts/Gameplay/Wave/][Task: WaveManager]**
- Pre-flight: Read `03_data_and_balance.md` wave specs.
- Implement `WaveManager.cs`: Track waves, timers, and enemy counts.
- Integration: Trigger `EnemySpawner.Spawn()` via `EventBroker`.

**[Scope B: Assets/Scripts/Gameplay/AI/][Task: SpecializedAI]**
- Pre-flight: Inherit from `EnemyBase`.
- Implement `BlitzEnemy.cs` (Fast/LowHP), `ArmoredEnemy.cs` (Slow/Tank), `SwarmEnemy.cs` (Pack).

**[Scope C: Assets/Scripts/Gameplay/Fever/][Task: FeverSystem]**
- Pre-flight: Check `EnemyKilledEvent` in `EventBroker`.
- Implement `FeverManager.cs`: Increment meter on kills. At 100%, buff fire rate and trigger `FeverModeEvent`.

---

### 2. [Antigravity: Gemini 3.1 Pro] VFX, UI & Prefab Setup
- Pre-flight: Check `Milestone 13` classes.
- Unity: Create prefabs in `Assets/Prefabs/Enemies/` for Blitz/Armored/Swarm.
- UI: Add `fever-meter` ProgressBar to `GameHUD.uxml` and bind in `GameHUD.cs`.

### 3. [Gemini CLI] Loop Validation
- Pre-flight: Save all assets.
- Unity: `manage_editor action="play"`, monitor `read_console` for "Wave/Fever" logs.
- Audit: Check `manage_scene action="get_hierarchy"` for runtime spawns.
- Finalize: `manage_editor action="stop"`, save scene.
