# [TARGET: Assets/Scripts/Gameplay/Enemies/Boss/TitanCoreAI.cs] [TASK: 7.2]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.2` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `EnemyBase`, `EventBroker`, `EnemyState`
- **Delta**: `docs/delta/7.1.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Gameplay/Enemies/Boss/TitanCoreAI.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **FSM Hierarchy (Logic)**: Implement `TitanCoreAI` extending `EnemyBase.cs`. Reuse and extend the state machine pattern defined in `EnemyBase` (from Task 3.5.1) using states: `BossPhase1State`, `BossPhase2State`, `BossPhase3State`. Do NOT spawn redundant coroutines for transitions.
2. **Phase Combat Logic**:
   - **Phase 1 (HP 100% to 60%)**: Rotational shield absorbs and reflects frontal damage.
   - **Phase 2 (HP 60% to 30%)**: Shield disintegrates. Casts target laser pattern targeting player (1s charge, 2s fire). Use a direct raycast sweep; extract values into ScriptableObject (`TitanCoreData.asset`).
   - **Phase 3 (HP 30% to 0%)**: Overclocked. 2x speed. Emits 8-direction bullets every 3s. No knockback.
3. **Event Broker & Decoupling**: Publish `OnBossPhaseChanged(int phase)` on threshold transitions and `OnBossDefeated` upon death via `EventBroker`. Make sure to unsubscribe from all event listeners in `OnDisable`/`OnDestroy` to prevent memory leaks.

### Verification Criteria (QA Perspective)
1. **EditMode State Transition Tests**: Write unit tests in `TitanCoreAITests.cs` mocking `EventBroker` and using a stubbed/virtual time controller to verify:
   - State transition occurs exactly at 60% and 30% HP thresholds.
   - Damage calculations verify front-shield reflection vs side vulnerability.

### Phase Constraints
- **RED Phase**: Write failing tests first under `tests/` and verify they fail.
- **GREEN Phase**: Implement code to pass tests.
- **DOCUMENT Phase**: Update documentation.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
