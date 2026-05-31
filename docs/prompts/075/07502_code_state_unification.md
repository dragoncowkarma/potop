# [TARGET: Assets/Scripts/Core/GameManager.cs] [TASK: 7.5.2]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.5.2` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `GameManager`, `GameFlowController`, `GameFlowState`, `GameState`, `WeaponData`, `EnemyBase`
- **Delta**: `docs/delta/7.5.1.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Core/GameManager.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **GameState enum 제거 및 통합**: `GameState` (Start/Playing/GameOver) enum을 완전히 제거합니다. `GameManager`는 `GameFlowController.CurrentState`를 참조하여 Playing 상태를 판단합니다. `GameManager.ChangeState()` → `GameFlowController.TransitionTo()` 위임. `GameManager.OnStateChanged` 이벤트를 `GameFlowStateChangedEvent`로 대체합니다.
2. **WeaponData 통합**: 레거시 `Potop.Client.Data.WeaponData` 클래스 삭제. 참조하는 `EnemyBot`, `TurretShooter` 등을 `Potop.Client.Gameplay.Weapons.WeaponData`로 이전.
3. **Gem 경제 분리**: `GameFlowController.OnEXPCollected`에서 `_gemsEarned += 1` 제거. EXP와 Gem은 독립 경제. 적 처치 시 `GemDropEvent` 발행, 이를 구독하여 Gem 집계.
4. **Reflection 제거**: `GameHUD.UpdateOvercharge()`에서 `System.Reflection`으로 `_overchargeData` private 필드 접근하는 코드를 제거. `OverchargeController`에 `public OverchargeData Data { get; }` property 추가.
5. **마이너 정리**: `EnemyBase.Move()` deprecated 메서드 삭제. `EnemyBase.ApplyKnockback()`에서 `GetComponent<Rigidbody>()` → 캐시된 `_rb` 사용.

### Verification Criteria (QA Perspective)
1. 기존 52개 EditMode 테스트 전체 통과.
2. `GameState` enum grep 0건.
3. `System.Reflection` 사용처 grep 0건 (테스트 파일 제외).
4. 레거시 `Data.WeaponData` grep 0건.

### Phase Constraints
- **RED Phase**: Write failing tests first under `tests/` and verify they fail.
- **GREEN Phase**: Implement code to pass tests.
- **DOCUMENT Phase**: Update documentation.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
