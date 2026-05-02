# Phase 2: 기초 아키텍처 및 데이터 시스템 - AI Prompts

> [!IMPORTANT]
> **Jules 병렬 처리 규칙**: 각 작업은 독립적인 디렉토리를 가지므로 병렬 처리가 가능합니다. `[SCOPE]`를 엄격히 준수하십시오.

---

## [Milestone 9] 이벤트 브로커 시스템
### [Step 1] Jules 프롬프트
```text
[SCOPE: Potop.Client.Core (Assets/Scripts/Core/Events/)][TASK:Logic/Architecture]
1. Implement Potop.Client.Core.Events.EventBroker.cs:
   - Pattern: Static event hub using Dictionary<Type, List<Action<object>>> for decoupling.
   - Requirement: Define base interface IGameEvent.
   - Methods: Publish<T>(Action<T> eventData), Subscribe<T>(Action<T> listener).
2. Refactoring: Ensure UI (GameHUD.cs) and Gameplay (TurretShooter.cs) use EventBroker for state/score/health communication.
```

### [Step 2] Gemini 3 Flash 프롬프트
```text
[SCOPE: Potop.Client.Core.Events (Assets/Scripts/Core/Events/Events.cs)][TASK:Boilerplate]
1. Define event records: ScoreChangedEvent(int), HealthChangedEvent(int, int), StateChangedEvent(GameState).
```

---

## [Milestone 10] 오브젝트 풀링 매니저
### [Step 1] Jules 프롬프트
```text
[SCOPE: Potop.Client.Core (Assets/Scripts/Core/Pooling/)][TASK:Logic/Performance]
1. Implement Potop.Client.Core.Pooling.PoolManager.cs:
   - Use: UnityEngine.Pool.ObjectPool<GameObject>.
   - Logic: Dynamic pool creation per prefab. Methods: Get(GameObject), Return(GameObject).
2. Integration: Update TurretShooter.cs (Projectiles) and EnemySpawner.cs (Enemies) to use pooling.
```

---

## [Milestone 11] ScriptableObject 기반 데이터화
### [Step 1] Jules 프롬프트
```text
[SCOPE: Potop.Client.Data (Assets/Scripts/Data/)][TASK:Architecture/Data]
1. Define ScriptableObject classes:
   - EnemyData.cs: Stats (Health, Speed, Score).
   - WeaponData.cs: Stats (FireRate, Damage, Prefab).
2. Requirement: Use [CreateAssetMenu] for editor integration.
```

### [Step 2] Antigravity (Gemini 3.1 Pro) 프롬프트
```text
[TASK:Unity/Data]
1. Create assets in 'Assets/Data/': NormalEnemy.asset, StarterGun.asset.
2. Link these assets to existing prefabs and components in the inspector.
```
