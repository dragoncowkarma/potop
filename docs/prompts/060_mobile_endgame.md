# Phase 6: 엔드게임 및 모바일 출시 준비 - AI Prompts

## [Milestone 21 - Boss Design]
### 1. [Claude Opus 4.6] Boss AI State Machine & Pattern Design
- **Task**: Design the "Titan Core" encounter logic.
- **Action**:
  - Phase 1 (Perimeter): Defensive orbit, shield generators active.
  - Phase 2 (Core): Shield down, reveals weak point, rapid projectile volleys.
  - Phase 3 (Rage): High speed, environmental hazards (beam sweeps).
  - Design: Provide transition triggers (Health Thresholds) and precise attack patterns for Jules.

---

## [Milestone 21, 22 - Endgame Implementation]
### 2. [Jules] [Parallel Execution] Boss AI, Achievement System, and Mobile Input
> [!IMPORTANT]
> **[STRICT SCOPE]**: Implement mobile-specific optimizations. Use `UNITY_ANDROID` / `UNITY_IOS` preprocessors where necessary.

**[Scope A: Assets/Scripts/Gameplay/Boss/][Task: Titan Core AI Implementation]**
- Implement `TitanCoreAI.cs`:
  - Architecture: Use a state machine pattern (State Class or Enum Switch).
  - Methods: `TransitionToPhase2()`, `TriggerBarrageAttack()`.
  - Collision: Multiple hitboxes (Shields vs. Core). Link with `HealthChangedEvent`.

**[Scope B: Assets/Scripts/Meta/][Task: Achievement & Save System]**
- Implement `AchievementManager.cs`:
  - Tasks: "Kill 1000 Enemies", "Reach Level 50", "Defeat Titan Core".
  - Storage: Use `PlayerPrefs` or JSON-based `SaveSystem.cs` for persistent data.
  - UI: Publish `AchievementUnlockedEvent` to show a temporary banner on `GameHUD`.

**[Scope C: Assets/Scripts/Input/][Task: Mobile Touch & Joystick Control]**
- Implement `MobileInputManager.cs`:
  - Detect touch or use `OnScreenJoystick`.
  - Logic: Map touch delta to `TurretRotation`. Implement "Tap to Fire" or "Auto-Fire" toggle.

---

## [Milestone 23 - Monetization & Ads]
### 3. [Antigravity: Gemini 3.1 Pro] Unity Ads Integration & Revive UI
- **Task**: Implement rewarded video logic for "Continue" capability.
- **Action**:
  - `manage_packages action="add_package" package="com.unity.ads"`: Verify availability.
  - Implement `AdsManager.cs`:
    - Logic: Method `public void ShowRewardAd(Action onCompleted)`.
    - UI: When player dies, show "Revive for Ad" button for 5 seconds.
    - Integration: If ad completed, call `GameManager.RevivePlayer()`.

---

## [Phase 6 Validation - Production Readiness]
### 4. [Gemini CLI] Build & Mobile Compatibility Audit
- **Task**: Final sanity check before deployment.
- **Action**:
  - `manage_build action="status"`: Verify target platform is set to `android` or `windows64`.
  - `manage_build action="settings" property="bundle_id" value="com.dragoncowkarma.potop"`: Ensure metadata is correct.
  - `read_console`: Check for `Assembly` errors in mobile preprocessor blocks.
  - `manage_profiler action="get_frame_timing"`: Verify target frame rate (60 FPS) is achievable in the current scene.
