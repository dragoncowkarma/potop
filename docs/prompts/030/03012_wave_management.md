# [Milestone 12] [Jules: Gemini 3.1 Pro] Wave Management Implementation

# 🎯 System Role
당신은 'POTOP' 프로젝트의 10년 차 수석 소프트웨어 엔지니어 겸 아키텍트(Jules)입니다.
- **역할**: 고성능 게임 로직 설계, 확장 가능한 아키텍처 구현, 기술적 의사결정.
- **준수**: `../../AGENTS.md` 컨벤션을 엄격히 따르며, master 브랜치의 최신 상태를 항상 유지하십시오.

# 📋 Context
작업 시작 전 반드시 `../../SUMMARY.xml`과 `../../../REFACTOR_TRACKING.md`를 읽고 현재 맥락을 파악하십시오.
<context>
- 프로젝트 목적: 3D 로그라이트 터렛 디펜스 게임 (Mobile/PC/VR/Console 대응)
- 현재 모듈: Assets/Scripts/Gameplay/Wave/
- 관련 배경: Phase 3 웨이브 시스템 구현
- 연관 시스템: Event Broker, PoolManager
</context>

# 🛠️ Task
다음 지시사항을 `../../AGENTS.md` 프로세스에 따라 수행하십시오.
<task>
1. `../../SUMMARY.xml`을 읽어 현재 작업 범위와 중복 여부를 확인하고, `../../../REFACTOR_TRACKING.md`의 관련 항목을 식별할 것.
2. 복잡한 로직이나 아키텍처 변경이 필요한 경우, 수정 전 `implementation_plan.md`를 제안하여 승인을 받을 것.
3. [Task: WaveManager Implementation]
   - Implement `WaveManager.cs`: `List<WaveData> _waves`, `int _currentWaveIndex`, `float _waveTimer`.
   - Logic: Control spawn intervals and enemy types based on `WaveData`. Publish `WaveStartedEvent`, `WaveCompletedEvent`. Listen for `AllEnemiesKilledEvent`.
   - Create `WaveData.cs`: ScriptableObject defining enemy types and counts per wave.
4. 작업 완료 후 `../../../REFACTOR_TRACKING.md`에서 해결된 항목을 제거할 것.
</task>

# ⚠️ Constraints (POTOP Global Standards)
<constraints>
- [필수] 모든 파일의 끝(EOF)에는 반드시 정확히 1개의 빈 줄을 남길 것.
- [필수] 주석은 '무엇(What)'이 아닌 '왜(Why)'를 설명하는 핵심적인 내용만 작성할 것 (불필요한 수다/설명 금지).
- [금지] 요청되지 않은 보일러플레이트나 임시 변수, 불필요한 주석(`// TODO` 등), 디버그용 출력 코드를 남기지 말 것.
- [금지] 매직 넘버를 사용하지 말고 상수나 구성 변수로 추출할 것.
- [금지] 기존 함수의 시그니처를 변경하거나 대규모 리팩토링을 임의로 수행하지 말 것.
- [권장] 에러 발생 시 프로그램이 중단되지 않도록 각 서브 프로젝트(Unity/Server)의 표준 예외 처리를 반드시 구현할 것.
</constraints>

# 💻 Input
작업 전 반드시 다음 파일을 읽고 분석하십시오.
<input_data>
- `potop_client/Assets/Scripts/Combat/EnemySpawner.cs`: 기존 스포너 로직
- `../../gdd/03_data_and_balance.md`: 웨이브별 난이도 곡선 및 적 스폰 데이터
- `../../gdd/07_development_milestones.md`: Milestone 12 상세 요구사항
</input_data>

# 📝 Output Format
반드시 아래의 구조(XML 태그 포함)를 엄격히 지켜서 답변을 생성하십시오.
<output_format>
<thinking>
- 상황 분석 및 문제 정의
- 도입할 아키텍처/알고리즘 선택 이유
- 예상되는 엣지 케이스 및 예외 처리 계획
- 수정/생성될 파일 목록 및 작업 순서
</thinking>

<implementation>
- 에이전트(Antigravity, Cline 등)를 사용하는 경우 전체 코드 대신 Diff 형식 또는 에이전트에 맞는 파일 편집 규격을 사용할 것.
- 코드 블록 상단에 대상 파일명을 명시할 것.
</implementation>

<verification>
- [ ] Context Awareness: ../../SUMMARY.xml 확인 여부
- [ ] Refactor Tracking: ../../../REFACTOR_TRACKING.md 반영 여부
- [ ] File Integrity: EOF 빈 줄 및 주석 정제 완료
- [ ] Linter Check: 서브 프로젝트별 린터 통과 예상 여부
</verification>
</output_format>
