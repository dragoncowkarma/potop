# Prompt: 시네머신 도입 및 카메라 로직 정비 (Cinemachine & Camera Control)

## Role: Antigravity (Unity Specialist)
## Milestone: [02504] 시네머신 및 카메라 로직 정비

### 🎯 Objective
현재의 단순한 `FirstPersonLook` 스크립트를 시네머신(Cinemachine)으로 대체하여, 보다 부드러운 시점 전환과 확장성 있는 카메라 연출(데미지 쉐이크, 타겟 트래킹 등)을 확보합니다.

### 🛠️ Requirements
1. **Cinemachine Setup**:
    - 프로젝트에 `Cinemachine` 패키지 설치 확인 및 `CinemachineBrain` 설정.
    - `Virtual Camera` (POV 또는 Third Person) 생성 및 플레이어 리깅 연동.
2. **Camera Shake (Impulse)**:
    - `Cinemachine Impulse Source`를 사용하여 폭발이나 피격 시 카메라 흔들림 효과 구현.
    - `EventBroker`를 통해 특정 강도의 쉐이크 이벤트를 수신하여 실행.
3. **Input Polish**:
    - `Input System`의 `Look` 액션과 시네머신의 `POV` 모듈을 연동하여 마우스 감도 및 부드러움(Smoothing) 조정.
4. **Scene Cleanup**:
    - 기존의 `FirstPersonLook.cs` 등 수동 카메라 제어 스크립트 제거 또는 시네머신용 Wrapper로 전환.

### ⚠️ Constraints
- Cinemachine 3.x (Unity 6 호환) 버전 기준 설정.
- 런타임에 가상 카메라의 우선순위(Priority)를 조절하여 연출 전환이 가능하도록 설계.
- Strictly follow `AGENTS.md` and `docs/AGENTS_CONVENTIONS.md`.
