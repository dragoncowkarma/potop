# Phase 6: 엔드게임 및 모바일 출시 준비 - AI Prompts

### 1. [Claude Opus 4.6] Boss Encounter Design
- Pre-flight: Review `Titan Core` GDD specs.
- Design: 3-phase state machine (Perimeter -> Core -> Rage). Define attack patterns.

### 2. [Jules] [Parallel Execution] Endgame Mechanics
**[Scope A: Assets/Scripts/Gameplay/Boss/][Task: TitanCoreAI]**
- Pre-flight: Follow `Claude Opus` design.
- Implement `TitanCore.cs`: Phase transitions and coordinate attack sub-components.

**[Scope B: Assets/Scripts/Gameplay/Wave/][Task: OverclockMode]**
- Pre-flight: Modify `WaveManager.cs`.
- Implement: Exponential enemy stat scaling for Endless mode (Wave 50+).

---

### 3. [Antigravity: Gemini 3.1 Pro] Mobile Monetization
- Pre-flight: `manage_packages action="list_packages"` (Check Unity Ads).
- Implementation: Create `AdManager.cs`. Integrate Rewarded Ads for Revive/Double Rewards.

### 4. [Gemini CLI] Pre-Release Audit
- Pre-flight: Ensure target platform is correct (manage_build action="status").
- Audit: `manage_profiler action="get_frame_timing"` or `stats_get` (Perf check).
- Health: `read_console` (Ensure ZERO errors before build).
