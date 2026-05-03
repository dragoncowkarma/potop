# Prompt: Phase 2.5 시스템 통합 검증 (Validation)

## Role: Gemini CLI (QA & Automation Specialist)
## Milestone: [02599] Phase 2.5 검증

### 🎯 Objective
Phase 2.5에서 구현된 무기 모듈화, 체력 시스템, 컴뱃 피드백 및 시네머신 카메라가 상호 보완적으로 작동하는지 최종 점검합니다.

### 🛠️ Tasks
1. **Scene Audit**:
    - `manage_scene action="validate"` 를 통해 하이어라키의 중복 컴포넌트나 미싱 레퍼런스 점검.
    - `Turret` 오브젝트에 시네머신 가상 카메라와 새로운 무기 파츠들이 정상 부착되었는지 확인.
2. **System Connectivity**:
    - `EnemyBot` 프리팹을 스폰하여 공격했을 때:
        - `Health` 컴포넌트의 수치가 감소하는가?
        - `VFXTrigger`에 의해 히트 플래시가 발생하는가?
        - 카메라 쉐이크가 발동하는가?
3. **Performance Check**:
    - `manage_profiler`를 통해 풀링된 발사체와 적들이 빈번하게 생성/해제될 때 GC Alloc이 치솟지 않는지 확인.
4. **Console Cleanup**:
    - `read_console`로 이벤트 구독 해제 누락(Memory Leak)이나 Null Reference 에러가 없는지 최종 확인.

### ⚠️ Constraints
- 모든 오류는 `REFACTOR_TRACKING.md`에 기록하거나 즉시 수정하세요.
- Strictly follow `AGENTS.md` and `docs/AGENTS_CONVENTIONS.md`.
