# [TARGET: Assets/Scripts/Core/Audio/SoundManager.cs] [TASK: 8.2]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.2` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `High` |

---

## Context Links

Before editing, read `SUMMARY.xml`, `REFACTOR_TRACKING.md`, `docs/SUMMARY.xml`, and `docs/architecture/06_art_and_sound.md`.

- **Map**: `docs/map.md` — Required symbols: `PoolManager`, `SoundManager`
- **Delta**: `docs/delta/8.1.json`
- **Architecture**: `docs/architecture/04_technical_architecture.md` — Object pooling and EventBroker rules

---

## Work Scope

**Target Files**:
- `Assets/Scripts/Core/Audio/SoundManager.cs`
- `Assets/Scripts/Core/Audio/AudioPool.cs`
- `Assets/Data/Audio/AudioData.asset` or matching ScriptableObject path already used in the project

### Technical Requirements (10-Year Expert Feedback)
1. **Sound Pooling**: Implement `SoundManager` and `AudioPool` so combat SFX reuse prewarmed `AudioSource` objects. Runtime `AddComponent<AudioSource>()` or `new GameObject()` inside playback paths is prohibited.
2. **Audio Data Contract**: Resolve clips, mixer group, volume, pitch randomization range, cooldown, and max simultaneous voices from `AudioData` ScriptableObject entries. Do not hardcode clip references in gameplay code.
3. **Voice Budgeting**: Cap repeated sounds by key/category so rapid kills, projectile hits, and gem pickups cannot create audio clutter or CPU spikes.
4. **Mixer Separation**: Route BGM, SFX, UI, and warning cues through explicit Audio Mixer groups. Preserve user-facing volume hooks for Phase 9 lobby/settings.
5. **EventBroker Integration**: Subscribe to combat, progression, UI, and game-flow events; unsubscribe in `OnDisable`/`OnDestroy` and avoid duplicate subscriptions after scene reload.
6. **Adaptive Music Hooks**: Expose BGM state changes for wave phase, fever level, boss phase, and overclock mode without implementing full composition logic in this task.
7. **Mobile Safety**: Avoid per-frame allocations and avoid decompress-on-load surprises for frequently played SFX.

### Verification Criteria (QA Perspective)
1. **Audio Pooling Tests**: Assert that repeated SFX playback draws from the pool and does not instantiate components during active playback.
2. **Subscription Tests**: Simulate enable/disable twice and verify each event fires exactly one audio response.
3. **Voice Cap Tests**: Trigger 100 identical hit events in one second and assert max simultaneous voices never exceeds the configured cap.
4. **GC Gate**: Profiler or allocation recorder shows 0B GC allocations on the hot playback path after pool warmup.
5. **Documentation Sync**: Update `docs/architecture/06_art_and_sound.md` if mixer groups, naming, or adaptive music states change.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
