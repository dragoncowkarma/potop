# [TARGET: Multiple .asmdef files] [TASK: 7.5.1]

## Task Metadata

| Field | Value |
|---|---|
| **Task ID** | `7.5.1` |
| **Agent Role** | `Jules (Logic/Architecture Engineer)` |
| **Priority** | `High` |

---

## Context Links

Use the Semantic Map (`docs/map.md`) to locate symbols:

- **Map**: `docs/map.md` — Required symbols: `EventBroker`, `GameManager`, `WeaponBase`
- **Delta**: `docs/delta/7.6.json`

---

## Work Scope

**Target File**: `Multiple .asmdef files`

### Technical Requirements (10-Year Expert Feedback)
1. **Assembly Definition 생성**: 5개 asmdef 파일을 생성합니다:
   - `Assets/Scripts/Core/Potop.Client.Core.asmdef` — 외부 참조 없음. Core 네임스페이스의 기반 모듈.
   - `Assets/Scripts/Data/Potop.Client.Data.asmdef` — Core 참조.
   - `Assets/Scripts/Gameplay/Potop.Client.Gameplay.asmdef` — Core, Data 참조.
   - `Assets/Scripts/UI/Potop.Client.UI.asmdef` — Core, Gameplay 참조.
   - `Assets/Scripts/Tests/Potop.Client.Tests.Editor.asmdef` — Core, Gameplay, Data 참조. `Editor` platform only. `overrideReferences: true`, `TestAssemblies` 참조 포함.
2. **테스트 분류**: `Core/Editor/` 하위의 `GameManagerTests.cs`를 `Tests/Editor/`로 이동. `Gameplay/Combat/Editor/` 하위 테스트 파일도 동일하게 이동.
3. **참조 정리**: 각 asmdef의 `references` 배열에 Unity 패키지 참조 추가 (InputSystem, UI Toolkit 등 필요한 것만).

### Verification Criteria (QA Perspective)
1. Unity 에디터에서 컴파일 에러 0건.
2. 기존 52개 EditMode 테스트 전체 통과.
3. 순환 참조 없음 확인.

### Phase Constraints
- **RED Phase**: Write failing tests first under `tests/` and verify they fail.
- **GREEN Phase**: Implement code to pass tests.
- **DOCUMENT Phase**: Update documentation.

---

## Thought Process
<!-- Write your System 2 reasoning here -->

## Code Change
<!-- Implementation goes here -->
