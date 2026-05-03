# Prompt: Tactical Hazards System Implementation

## Role: Antigravity (Unity UI/VFX Specialist)
## Milestone: [Milestone 13.5] 환경 상호작용 기믹 (Tactical Hazards)

### 🎯 Objective
Implement a system for interactive environment objects (Hazards) that provide tactical advantages to the player when destroyed.

### 🛠️ Requirements
1. **Hazard Base Class**:
    - Create an abstract `EnvironmentalHazard` MonoBehaviour.
    - Fields: `maxHp`, `currentHp`, `explosionRadius`, `explosionDamage`.
    - Methods: `TakeDamage(float damage)`, `TriggerEffect()` (abstract).
2. **Unstable Core**:
    - Inherits from `EnvironmentalHazard`.
    - Effect: When destroyed, use `Physics.OverlapSphere` to find all enemies within radius.
    - Apply `explosionDamage` and a temporary `Slow` effect (reduce speed by 50% for 3 seconds).
    - Trigger a VFX (e.g., electrical explosion) and SFX.
3. **Magnetic Scrap**:
    - Inherits from `EnvironmentalHazard`.
    - Effect: When destroyed, create a "Gravity Well" for 5 seconds.
    - Every frame, find all `Gem` objects in range and pull them towards the center using `Vector3.MoveTowards` or `AddForce`.
4. **Spawner Integration**:
    - Hazards should be spawned randomly within the gameplay area via `HazardSpawner`.
    - Must use `PoolManager` for efficient memory management.

### ⚠️ Constraints
- Use Unity 6.0 LTS features.
- Ensure performant physics queries (use layer masks).
- VFX should be triggered via `EventBroker` or direct reference to a pooled VFX object.
- Strictly follow `AGENTS.md` and `docs/AGENTS_CONVENTIONS.md`.
