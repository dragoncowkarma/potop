# [TARGET: Assets/Scripts/Gameplay/Wave/WaveManager.cs] [TASK: 7.5.4]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.5.4` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `WaveManager`, `TitanCoreAI`, `GameFlowController`, `OverclockMode`, `SpreadFireStrategy`, `LobFireStrategy`
- **Delta**: `docs/delta/7.5.3.json`

---

## Work Scope

**Target File**: `Assets/Scripts/Gameplay/Wave/WaveManager.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **보스 스폰 자동화**: `WaveManager`에 15분(900초) 경과 시 `TitanCore` 프리팹을 자동 스폰하는 로직 추가. 스폰 직후 `GameFlowController.TransitionTo(GameFlowState.BossBattle)` 호출. 보스 스폰 위치는 플레이어 전방 고정 거리(50m).
2. **Overclock 연동 검증**: `OverclockMode.cs`에서 `OnBossDefeated` 이벤트 수신 후 `WaveManager.EnterContinuousMode()` 호출 경로 확인 및 구현. `EnterContinuousMode()`는 웨이브 간 대기시간을 0으로 설정하고 무한 루프 진입.
3. **SpreadFireStrategy 구현**: TODO 제거. 부채꼴(Fan) 패턴으로 3~5발 동시 발사. 각도 간격은 `WeaponData.SpreadAngle`에서 읽어옴. 각 투사체는 독립적 방향 벡터.
4. **LobFireStrategy 구현**: TODO 제거. 포물선 궤도 구현. 투사체에 `Rigidbody.useGravity = true` 설정 + 초기 속도 벡터를 위쪽으로 기울여 발사 (`LaunchAngle` from WeaponData). 착탄 시 AoE 범위 (Juggernaut 특성).

### Verification Criteria (QA Perspective)
1. 보스 스폰 시점 테스트: 900초 경과 시 `BossSpawnedEvent` 발행 확인.
2. Overclock 진입 테스트: BossDefeated 후 WaveManager가 continuous 모드 진입 확인.
3. SpreadFireStrategy 테스트: 발사 시 투사체 개수와 각도 분산 검증.
4. LobFireStrategy 테스트: 발사 시 초기 속도 벡터의 y 성분이 양수인지 검증.

### Phase Constraints
- **RED Phase**: Write failing tests first under `tests/` and verify they fail.
- **GREEN Phase**: Implement code to pass tests.
- **DOCUMENT Phase**: Update documentation.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
