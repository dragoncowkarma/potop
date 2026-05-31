# [TARGET: Assets/Scripts/UI/GameHUD.cs] [TASK: 7.5.3]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.5.3` |
| **Agent Role** | `Antigravity (Unity UI/Visuals Engineer)` |
| **Priority** | `Medium` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `GameHUD`, `EventBroker`, `OverchargeController`, `FeverManager`, `EnergyManager`
- **Delta**: `docs/delta/7.5.2.json`

---

## Work Scope

**Target File**: `Assets/Scripts/UI/GameHUD.cs`

### Technical Requirements (10-Year Expert Feedback)
1. **위젯 분리**: `GameHUD`(454줄)를 6개 독립 위젯 클래스로 분리합니다:
   - `HealthBarWidget.cs` — HP 바, 피격 플래시. `PlayerHealthChangedEvent` 구독.
   - `ScoreWidget.cs` — 점수 표시. `ScoreChangedEvent` 구독.
   - `FeverGaugeWidget.cs` — 피버 게이지 + 티어 표시. `FeverChangedEvent` 구독.
   - `OverchargeWidget.cs` — 오버차지 게이지 + 과열 상태. `OverchargeChangedEvent` 구독.
   - `TacticalSkillBar.cs` — 3개 스킬 아이콘 + 쿨다운 오버레이 + 에너지 바. `EnergyChangedEvent`, `SkillCooldownEvent` 구독.
   - `GameOverPanel.cs` — 게임오버 UI + 재시작/메뉴 버튼.
2. **GameHUD 축소**: `GameHUD`는 위젯 초기화, UXML 참조 할당, 레이아웃 관리만 담당. 100줄 이내 목표.
3. **EventBroker 직접 구독**: 각 위젯이 `OnEnable`에서 `EventBroker.Subscribe`, `OnDisable`에서 `Unsubscribe` 직접 호출. 위젯 간 의존성 없음.

### Verification Criteria (QA Perspective)
1. `GameHUD.cs` 줄 수 100줄 이내.
2. 각 위젯이 독립적으로 이벤트 구독/해제.
3. 기존 HUD 기능 동일하게 동작.

### Phase Constraints
- **RED Phase**: Write failing tests first under `tests/` and verify they fail.
- **GREEN Phase**: Implement code to pass tests.
- **DOCUMENT Phase**: Update documentation.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
