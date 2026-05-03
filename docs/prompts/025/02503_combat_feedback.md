# Prompt: 컴뱃 피드백 아키텍처 (Combat Feedback Architecture)

## Role: Antigravity (Visuals & Unity Specialist)
## Milestone: [02503] 컴뱃 피드백 아키텍처

### 🎯 Objective
전투의 타격감을 결정짓는 시각/청각 피드백(Juice)을 개별 스크립트에 하드코딩하지 않고, 이벤트 기반으로 처리하는 아키텍처를 구축합니다.

### 🛠️ Requirements
1. **Feedback Triggers**:
    - `VFXTrigger.cs`: `EventBroker`의 `OnDamaged` 또는 `OnDeath` 이벤트를 구독하여 특정 위치에 파티클을 재생하거나 `Hit Flash`(머티리얼 색상 변경) 효과를 실행.
    - `SFXTrigger.cs`: `EventBroker`를 통해 사운드 클립을 재생하는 경량 컴포넌트.
2. **Hit Flash System**:
    - 피격 시 적으로 사용되는 머티리얼의 `_EmissionColor` 또는 `_BaseColor`를 순간적으로 흰색으로 변경했다가 되돌리는 로직 구현 (Coroutine 또는 Simple Timer).
3. **Event Integration**:
    - `02502`에서 정의한 `Health` 컴포넌트의 이벤트를 구독하여 시각 효과가 자동 연동되도록 설정.
4. **Data-Driven**:
    - 어떤 이펙트를 쓸지는 `ScriptableObject`나 인스펙터의 레퍼런스를 통해 설정 가능해야 함.

### ⚠️ Constraints
- 머티리얼 인스턴싱으로 인한 메모리 누수 방지 (`MaterialPropertyBlock` 사용 권장).
- 오디오 소스 풀링 고려 (기존 `PoolManager` 활용).
- Strictly follow `AGENTS.md` and `docs/AGENTS_CONVENTIONS.md`.
