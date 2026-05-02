# Phase 5: 폴리싱 및 사용자 경험 (UX) - AI Prompts

### 1. [Jules] [Parallel Execution] Juice & Localization
**[Scope A: Assets/Scripts/VFX/][Task: CombatJuice]**
- Pre-flight: Check `Cinemachine` (manage_camera action="ping").
- Implement `CombatJuice.cs`: Screen shake, post-process effects on hits.

**[Scope B: Assets/Scripts/Core/Audio/][Task: AudioFoundation]**
- Pre-flight: Singleton pattern.
- Implement `AudioManager.cs`: `PlaySFX`, `SetMusicVolume`.

**[Scope C: Assets/Scripts/Core/Loca/][Task: LocaLogic]**
- Pre-flight: Read `LocaData.json`.
- Implement `LocaHelper.cs`: Map UI element IDs to localized JSON strings.

---

### 2. [Antigravity: Gemini 3.1 Pro] Audio & UI Integration
- Pre-flight: Verify `AudioManager` exists.
- Unity: Add `AudioSource` to prefabs. Link `TurretWeapon`, `EnemyBase` to `AudioManager.PlaySFX()`.

### 3. [Gemini CLI] Data & UX Audit
- Pre-flight: Check `Assets/Resources/`.
- Data: Create `LocaData.json` [ko, en] via `run_command`.
- Audit: `manage_ui action="render_ui"` (Check HUD preview).
- Health: `read_console` (Check for missing resources/keys).
