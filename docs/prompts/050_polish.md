# Phase 5: 폴리싱 및 사용자 경험 (UX) - AI Prompts

## [Milestone 18, 19, 20 - Polish Systems Implementation]
### 1. [Jules] [Parallel Execution] Visual Juice, Sound System, and Localization
> [!IMPORTANT]
> **[STRICT SCOPE]**: Integrate with existing systems (Combat, UI). Use `EventBroker` for audio-visual triggers (e.g., `OnImpact`, `OnFever`).

**[Scope A: Assets/Scripts/VFX/][Task: Combat Juice & Feedback]**
- Implement `CameraShakeController.cs`: Use `CinemachineImpulseSource` to trigger shakes on enemy deaths or turret firing.
- Implement `HitFlash.cs`: Add to `Enemy` prefabs. Use material property blocks to flash `_EmissionColor` on hit.
- Particle Integration: Link existing `MuzzleFlash` and `ImpactVFX` to `TurretWeapon` events.

**[Scope B: Assets/Scripts/Audio/][Task: Global Sound Manager]**
- Implement `AudioManager.cs`:
  - Static hub for SFX/BGM. Use `AudioSource` pooling or `PlayOneShot` with pitch randomization (+/- 0.1).
  - Methods: `public void PlaySFX(AudioClip clip, float volume = 1f)`, `public void PlayBGM(AudioClip clip)`.
- Integration: Add audio triggers to `TurretShooter.Fire()` and `Enemy.OnDie()`.

**[Scope C: Assets/Scripts/UI/][Task: Dynamic Localization System]**
- Implement `LocalizationManager.cs`:
  - Storage: Use a `Dictionary<string, string>` loaded from a JSON file in `StreamingAssets`.
  - Logic: Method `public string GetLocalizedString(string key)`.
  - UI Bindings: Automatically update `label.text` based on `key` attributes found in UXML elements.

---

## [Milestone 18, 19 - Asset Configuration]
### 2. [Antigravity: Gemini 3.1 Pro] Audio-Visual Asset Setup & Volume Control
- **Task**: Configure post-processing and sound assets.
- **Action**:
  - `manage_graphics action="volume_create"`: Create a global Volume with `Bloom` (Intensity: 1.5) and `Vignette` (Smoothness: 0.4).
  - Asset Placement: Create `Assets/Audio/SFX/` and `Assets/Audio/BGM/` folders.
  - Mixer Setup: Create an `AudioMixer` with Master, SFX, and Music groups. Link `AudioManager` to these groups.
  - VFX: Assign `Impact_Default.prefab` to the `EnemyData` ScriptableObjects.

---

## [Phase 5 Validation - UX & Performance]
### 3. [Gemini CLI] Polish Consistency Audit
- **Task**: Verify that "Juice" doesn't degrade performance or readability.
- **Action**:
  - `stats_get`: Monitor draw calls during high-density combat (ensure `HitFlash` uses property blocks, not material cloning).
  - `read_console`: Check for missing audio clip references or localization key warnings.
  - `manage_camera action="get_brain_status"`: Ensure `CinemachineImpulse` is properly configured on the main camera.
  - `manage_ui action="render_ui"`: Verify that localized text fits within existing button boundaries.
