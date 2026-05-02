# Phase 2: 기초 아키텍처 및 데이터 시스템 - AI Prompts

## [Milestone 9, 10, 11 - Core Architecture Implementation]
### 1. [Jules] [Parallel Execution] Event Broker, Pooling, and ScriptableObjects
> [!IMPORTANT]
> **[STRICT SCOPE]**: Work only within `Assets/Scripts/Core/` and `Assets/Scripts/Data/`. Follow `Potop.Client.*` namespace conventions.

**[Scope A: Assets/Scripts/Core/Events/][Task: EventBroker System]**
- Implement `EventBroker.cs`:
  - Static utility class or Singleton.
  - Methods: `public static void Subscribe<T>(Action<T> action)`, `public static void Unsubscribe<T>(Action<T> action)`, `public static void Publish<T>(T eventData)`.
  - Storage: Use `Dictionary<Type, Delegate>` to manage subscriptions.
- Create `GameEvents.cs`:
  - `public struct ScoreChangedEvent { public int CurrentScore; }`
  - `public struct HealthChangedEvent { public int CurrentHealth; public int MaxHealth; }`
- Refactor: Update `GameManager.cs` to publish events; `GameHUD.cs` to subscribe and update UI.

**[Scope B: Assets/Scripts/Core/Pooling/][Task: Object Pooling Manager]**
- Implement `PoolManager.cs`:
  - Use `UnityEngine.Pool.ObjectPool<GameObject>` for modern pooling.
  - Implement a `Dictionary<GameObject, IObjectPool<GameObject>>` to manage multiple prefabs.
  - Methods: `public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)`, `public void Despawn(GameObject instance)`.
- Integration: Update `TurretShooter.cs` to use `PoolManager` for projectiles instead of `Instantiate/Destroy`.

**[Scope C: Assets/Scripts/Data/][Task: ScriptableObject Definitions]**
- Create `EnemyData.cs`: `[CreateAssetMenu]` including `string EnemyName`, `int MaxHealth`, `float MoveSpeed`, `int ScoreValue`.
- Create `WeaponData.cs`: `[CreateAssetMenu]` including `float FireRate`, `int Damage`, `GameObject ProjectilePrefab`.
- Update: Modify `Enemy.cs` and `TurretShooter.cs` to initialize stats from these SO assets.

---

## [Milestone 11 - Data Initialization]
### 2. [Antigravity: Gemini 3.1 Pro] Unity Data Assets & Prefab Wiring
- **Task**: Create concrete data assets and link them to the hierarchy.
- **Action**:
  - Create `NormalEnemy.asset` and `StarterGun.asset` in `Assets/Data/`.
  - Populate values from `03_data_and_balance.md`.
  - Find `EnemyBot` prefab: Update `Enemy` component to reference `NormalEnemy.asset`.
  - Find `Turret` object: Update `TurretShooter` component to reference `StarterGun.asset`.
  - Verify `PoolManager` is present on a global 'Managers' object.

---

## [Phase 2 Validation - System Integrity Check]
### 3. [Gemini CLI] Architectural Audit & Console Cleanup
- **Task**: Verify system-to-system communication and clean up technical debt.
- **Action**:
  - `manage_scene action="get_hierarchy"`: Ensure no orphaned components from refactoring.
  - `read_console`: Monitor for "Missing Reference" errors or "Action not unsubscribed" warnings.
  - `run_command`: Remove any legacy `TurretShooter_Old.cs` or temporary test scripts.
  - `manage_physics action="validate"`: Ensure layer-based collisions still work after pooling integration.
