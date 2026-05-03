# [Milestone 16] [Antigravity: Gemini 3.1 Pro] Upgrade Selection Menu & Controller

# 🎯 System Role
당신은 수석 Unity 엔진 엔지니어 및 UI/UX 디자이너(Antigravity)입니다.
- **역할**: UI/UX 최적화, 비주얼 이펙트, 유니티 에디터 설정, 게임 디자인.

# 📋 Context
작업 시작 전 반드시 `../../SUMMARY.xml`과 `../../../REFACTOR_TRACKING.md`를 읽고 현재 맥락을 파악하십시오.
<context>
- 프로젝트 목적: 3D 로그라이트 터렛 디펜스 게임
- 현재 모듈: UI Toolkit (UXML/USS)
- 관련 배경: 레벨업 시 업그레이드 선택 메뉴 구현
- **의존성**: Jules의 레벨업 로직(04016_jules_xp_leveling) 및 PR 머지 완료 후 시작
</context>

# 🛠️ Task
다음 지시사항을 `../../AGENTS.md` 프로세스에 따라 수행하십시오.
<task>
1. `../../SUMMARY.xml` 확인.
2. [UXML Design]: `UpgradeMenu.uxml` 생성. 3개의 버튼('slot-0', 'slot-1', 'slot-2') 포함.
3. [USS Styling]: 유리 질감 및 호버 효과 적용.
4. [Controller]: `UpgradeUIController.cs` 구현. `LevelUpEvent` 시 시간 정지 및 무작위 업그레이드 표시.
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
- `potop_client/Assets/UI/Templates/BasePanel.uss`: 기본 스타일 시트 (참조용)
- `../../gdd/05_meta_and_progression.md`: 업그레이드 항목 목록 및 기획서
</input_data>

# 📝 Output Format
<output_format>
<thinking>
- UI Toolkit 인터랙션 및 스타일링 전략
</thinking>
<implementation>
- 에이전트 도구 사용 또는 Diff 형식 사용
</implementation>
<verification>
- [ ] UI 스타일 가이드 준수 확인
- [ ] EOF 빈 줄 준수 확인
</verification>
</output_format>
