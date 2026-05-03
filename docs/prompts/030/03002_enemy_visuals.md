# [Milestone 13] [Antigravity: Gemini 3.1 Pro] VFX, UI Progress Bar, and Prefab Variants

# 🎯 System Role
당신은 수석 Unity 엔진 엔지니어 및 UI/UX 디자이너(Antigravity)입니다.
- **역할**: UI/UX 최적화, 비주얼 이펙트, 유니티 에디터 설정, 게임 디자인.

# 📋 Context
작업 시작 전 반드시 `../../SUMMARY.xml`과 `../../../REFACTOR_TRACKING.md`를 읽고 현재 맥락을 파악하십시오.
<context>
- 프로젝트 목적: 3D 로그라이트 터렛 디펜스 게임
- 현재 모듈: Assets/Materials/, Assets/Prefabs/, UI Toolkit
- 관련 배경: 적 변이 시각화 및 피버 바 연동
- **의존성**: Jules의 로직 구현(03013_jules_enemy_variants) 및 PR 머지 완료 후 시작
</context>

# 🛠️ Task
다음 지시사항을 `../../AGENTS.md` 프로세스에 따라 수행하십시오.
<task>
1. `../../SUMMARY.xml` 확인.
2. [Visual Differentiation]: 3가지 머티리얼(`Mat_Blitz`, `Mat_Armored`, `Mat_Swarm`) 생성 및 색상 차별화.
3. [Prefab Variants]: `EnemyBot` 기반의 3가지 프리팹 생성. 각 특수 AI 스크립트 및 머티리얼 할당.
4. [UI Integration]: `GameHUD.uxml`에 'fever-bar'(`ProgressBar`) 추가.
5. [HUD Controller]: `GameHUD.cs`에서 `FeverGaugeChangedEvent`를 구독하여 바의 가치를 업데이트.
</task>

# ⚠️ Constraints (POTOP Global Standards)
<constraints>
- [필수] 모든 파일의 끝(EOF)에는 반드시 정확히 1개의 빈 줄을 남길 것.
- [필수] 주석은 '무엇(What)'이 아닌 '왜(Why)'를 설명하는 핵심적인 내용만 작성할 것.
- [금지] 요청되지 않은 보일러플레이트나 임시 변수, 불필요한 주석을 남기지 말 것.
- [금지] 매직 넘버를 사용하지 말고 상수나 구성 변수로 추출할 것.
- [금지] 기존 함수의 시그니처를 변경하거나 대규모 리팩토링을 임의로 수행하지 말 것.
- [권장] 에러 발생 시 프로그램이 중단되지 않도록 표준 예외 처리를 반드시 구현할 것.
</constraints>
# 💻 Input
<input_data>
- `potop_client/Assets/Prefabs/Combat/EnemyBot.prefab`: 기본 적 프리팹
- `../../gdd/06_art_and_sound.md`: 적 비주얼 스타일 가이드
</input_data>

# 📝 Output Format
<output_format>
<thinking>
- 시각적 구현 및 UI 바인딩 전략
</thinking>
<implementation>
- 에이전트 도구 사용 또는 Diff 형식 사용
</implementation>
<verification>
- [ ] UI Toolkit 바인딩 완료
- [ ] EOF 빈 줄 준수 확인
</verification>
</output_format>
