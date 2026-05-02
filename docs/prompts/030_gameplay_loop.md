# Phase 3: 게임플레이 루프 및 AI 기초 - AI Prompts

## [Milestone 12, 13, 14 - Systems Implementation]
### 1. [Jules] [Parallel Execution] Wave Management, Specialized AI, and Fever System
> [!IMPORTANT]
> **[STRICT SCOPE]**: Work within `Assets/Scripts/Gameplay/`. Use `EventBroker` for system-wide triggers (e.g., `OnWaveStarted`, `OnFeverActivated`).

**[Scope A: Assets/Scripts/Gameplay/Wave/][Task: WaveManager Implementation]**
- Implement `WaveManager.cs`:
  - Members: `List<WaveData> _waves`, `int _currentWaveIndex`, `float _waveTimer`.
  - Logic: Control spawn intervals and enemy types based on `WaveData`.
  - Integration: Publish `WaveStartedEvent` and `WaveCompletedEvent`. Listen for `AllEnemiesKilledEvent` to progress waves.
- Create `WaveData.cs`: ScriptableObject defining enemy types and counts per wave.

**[Scope B: Assets/Scripts/Gameplay/AI/][Task: Specialized Enemy Variants]**
- Refactor `Enemy.cs` into `EnemyBase.cs`.
- Implement Specialized Classes:
  - `BlitzEnemy.cs`: High speed, low health, direct beeline to player.
  - `ArmoredEnemy.cs`: High health, low speed, ignores minor knockback.
  - `SwarmEnemy.cs`: Low health, spawns in clusters of 3-5.
- Logic: Ensure each variant uses `PoolManager` and references its own `EnemyData` ScriptableObject.

**[Scope C: Assets/Scripts/Gameplay/Fever/][Task: Fever Mode System]**
- Implement `FeverManager.cs`:
  - State: `float _currentFeverGauge`, `bool _isFeverActive`.
  - Logic: Subscribe to `EnemyKilledEvent`. Increment gauge (0-100).
  - Activation: When 100%, trigger `FeverModeActiveEvent`. While active, multiply `TurretShooter._fireRate` by 0.5 (double speed).
  - Timer: Fever lasts for 10 seconds, then gauge resets and `FeverModeEndedEvent` is published.

---

## [Milestone 13 - Enemy Visuals]
### 2. [Antigravity: Gemini 3.1 Pro] VFX, UI Progress Bar, and Prefab Variants
- **Task**: Visual differentiation and HUD integration.
- **Action**:
  - Create 3 materials (`Mat_Blitz`, `Mat_Armored`, `Mat_Swarm`) with distinct colors.
  - Create 3 prefabs inheriting from `EnemyBot`: Assign respective AI scripts and materials.
  - UI Toolkit: Add a `ProgressBar` named 'fever-bar' to `GameHUD.uxml`.
  - `GameHUD.cs`: Subscribe to `FeverGaugeChangedEvent` and update the bar's `.value` property.

---

## [Phase 3 Validation - Loop & Stress Test]
### 3. [Gemini CLI] Gameplay Performance Audit
- **Task**: Verify wave progression and resource cleanup.
- **Action**:
  - `manage_editor action="play"`: Monitor console for `WaveCompleted` logs.
  - `profiler_status`: Check memory usage to ensure `PoolManager` is preventing allocation spikes.
  - `read_console`: Check for `NullReferenceException` in `FeverManager` during rapid kills.
  - `manage_scene action="get_hierarchy"`: Verify `EnemyInstance` objects are properly recycled, not duplicated.
