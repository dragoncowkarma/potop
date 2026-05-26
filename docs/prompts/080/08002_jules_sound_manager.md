# [TARGET: Assets/Scripts/Core/Audio/SoundManager.cs] [TASK: 8.2]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `8.2` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `PoolManager`, `SoundManager`
- **Delta**: `docs/delta/8.1.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Core/Audio/SoundManager.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **Sound Pooling**: Implement `SoundManager.cs` and `AudioPool.cs` utilizing `PoolManager` to reuse `AudioSource` components for frequent combat sounds. Static creation of `AudioSource` at runtime is prohibited.
2. **Centralized Interface**: Decouple audio clips from hardcoded triggers. Load audio variables from `AudioData.asset` ScriptableObject.
3. **Decoupled Subscription**: Listen to UI and combat events via `EventBroker` and clean up subscriptions properly.

### Verification Criteria (QA Perspective)
1. **Audio Pooling Tests**: Write unit tests asserting that playing SFX draws from the pool and doesn't instantiate new components during active playback.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
