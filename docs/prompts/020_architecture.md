# Phase 2: 기초 아키텍처 및 데이터 시스템 - AI Prompts

### 1. [Jules] [Parallel Execution] Core Logic & Data Infrastructure
> [!IMPORTANT]
> **[STRICT SCOPE]**: Do NOT modify files outside the assigned paths. Use `EventBroker` for all inter-system communication.

**[Scope A: Assets/Scripts/Core/Events/][Task: EventBroker]**
- Pre-flight: Read `AGENTS.md`, `SUMMARY.xml`.
- Implement `EventBroker.cs`: Static hub with `Subscribe<T>`, `Unsubscribe<T>`, `Publish<T>`.
- Refactor `GameHUD.cs`, `TurretShooter.cs` to use events for score/health.
- Create `GameEvents.cs`: Define `ScoreChangedEvent(int)`, `HealthChangedEvent(int, int)`.

**[Scope B: Assets/Scripts/Core/Pooling/][Task: PoolManager]**
- Pre-flight: Verify `UnityEngine.Pool` API.
- Implement `PoolManager.cs`: `ObjectPool<GameObject>` wrapper with `Get/Return` logic.
- Integration: Update `TurretShooter` (Projectiles) and `EnemySpawner` (Enemies).

**[Scope C: Assets/Scripts/Data/][Task: ScriptableObjects]**
- Pre-flight: Check `AGENTS.md` naming conventions.
- Implement `EnemyData.cs` (Health, Speed, Score) and `WeaponData.cs` (FireRate, Damage, Prefab).
- Use `[CreateAssetMenu]` for editor assets.

---

### 2. [Antigravity: Gemini 3.1 Pro] Unity Data & Inspector Setup
- Pre-flight: Verify scripts in `Assets/Scripts/Data/` are compiled.
- Unity: Create `NormalEnemy.asset`, `StarterGun.asset` in `Assets/Data/`.
- Config: Assign stats per `03_data_and_balance.md` and link to prefabs/components.

### 3. [Gemini CLI] Architectural Integrity Audit
- Pre-flight: Check Unity Edit Mode.
- Audit: `manage_scene action="get_hierarchy"` (Check Manager objects).
- Health: `read_console` (Ensure 0 Errors/Warnings).
- Cleanup: Remove legacy boilerplate in `Assets/Scripts/`.
