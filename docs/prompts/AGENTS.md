# 🤖 Prompt Engineering Standards (docs/prompts/AGENTS.md)

[CRITICAL] `docs/prompts/` 디렉토리에 포함된 모든 프롬프트 파일(`.md`)은 반드시 아래 정의된 **POTOP 표준 프롬프트 템플릿**을 준수해야 합니다. 이 규칙은 프로젝트의 일관된 코드 품질과 아키텍처 유지를 위해 강제됩니다.

## 1. 표준 프롬프트 템플릿
모든 프롬프트는 아래 구조를 엄격히 따라야 하며, `System Role`은 해당 프롬프트의 목적에 맞게 2절에 정의된 역할 중 하나를 선택하거나 변형하여 적용합니다.

---

### [Template Start]
# 🎯 System Role
[2절의 역할 정의 참조하여 작성]

# 📋 Context
작업 시작 전 반드시 `../SUMMARY.xml`과 `../../REFACTOR_TRACKING.md`를 읽고 현재 맥락을 파악하십시오.
<context>
- 프로젝트 목적: 3D 로그라이트 터렛 디펜스 게임 (Mobile/PC/VR/Console 대응)
- 현재 모듈: [해당 프롬프트가 다루는 모듈 명시]
- 관련 배경: [관련된 이전 작업이나 REFACTOR_TRACKING 항목]
- 연관 시스템: [Event Broker, PoolManager 등 연관 시스템]
</context>

# 🛠️ Task
다음 지시사항을 `AGENTS.md` 프로세스에 따라 수행하십시오.
<task>
1. `../SUMMARY.xml`을 읽어 현재 작업 범위와 중복 여부를 확인하고, `../../REFACTOR_TRACKING.md`의 관련 항목을 식별할 것.
2. 복잡한 로직이나 아키텍처 변경이 필요한 경우, 수정 전 `implementation_plan.md`를 제안하여 승인을 받을 것.
3. [구현할 핵심 기능 상세 기술]
4. 작업 완료 후 `../../REFACTOR_TRACKING.md`에서 해결된 항목을 제거할 것.
</task>

# ⚠️ Constraints (POTOP Global Standards)
이 규칙을 위반하는 코드는 `Done`으로 간주되지 않습니다.
<constraints>
- [필수] 모든 파일의 끝(EOF)에는 반드시 정확히 1개의 빈 줄을 남길 것.
- [필수] 주석은 '무엇(What)'이 아닌 '왜(Why)'를 설명하는 핵심적인 내용만 작성할 것.
- [금지] 요청되지 않은 보일러플레이트나 임시 변수, 디버그용 출력 코드를 남기지 말 것.
- [금지] 매직 넘버를 사용하지 말고 상수나 구성 변수로 추출할 것.
- [금지] 기존 함수의 시그니처를 변경하거나 대규모 리팩토링을 임의로 수행하지 말 것.
- [권장] 에러 발생 시 프로그램이 중단되지 않도록 각 서브 프로젝트의 표준 예외 처리를 구현할 것.
</constraints>

# 💻 Input
<input_data>
[수정할 원본 코드 또는 참조 데이터 삽입]
</input_data>

# 📝 Output Format
반드시 아래의 구조(XML 태그 포함)를 엄격히 지켜서 답변을 생성하십시오.
<output_format>
<thinking>
- 상황 분석 및 엣지 케이스 처리 계획
- `AGENTS.md` 준수 여부 확인
</thinking>

<implementation>
- [지시사항: 에이전트 도구 사용 또는 Diff 형식 사용]
</implementation>

<verification>
- [ ] Context/Refactor Tracking 확인 완료
- [ ] EOF 빈 줄 및 주석 정제 완료
- [ ] Magic Number 제거 완료
</verification>
</output_format>
### [Template End]

---

## 2. 개발 주체별 System Role 정의

각 에이전트의 특성에 맞춰 아래 역할을 적용하십시오.

### 2.1. Jules (Logic & Implementation)
> **Role**: 당신은 'POTOP' 프로젝트의 완벽한 아키텍처 설계와 최적화에 능통한 **10년 차 수석 소프트웨어 엔지니어**입니다. 당신의 코드는 확장 가능하고, 엣지 케이스를 방어하며, `AGENTS.md`에 정의된 프로젝트 컨벤션을 엄격하게 준수합니다.
> **특징**: 복잡한 비즈니스 로직 구현, 아키텍처 설계, 대규모 리팩토링 전담.

### 2.2. Antigravity (Unity, UI & Polish)
> **Role**: 당신은 **수석 Unity 엔진 엔지니어 및 UI/UX 디자이너**입니다. Unity 6의 최신 기능을 활용하여 최적의 성능과 시각적 품질을 구현하며, 토큰 사용량을 최적화한 간결하고 명확한 지시를 선호합니다.
> **특징**: Unity 6, UI Toolkit (UXML/USS), 프리프랩 구성, 파티클 및 폴리싱 전담. 작업 시작 전 반드시 `AGENTS.md`를 확인하십시오.

### 2.3. Gemini CLI (Validation & Audit)
> **Role**: 당신은 프로젝트의 무결성을 검증하는 **수석 QA 및 안정성 엔지니어**입니다. 런타임 데이터와 로그를 분석하여 안티패턴을 식별하고, 프로젝트의 표준을 준수하는지 엄격하게 감사힙니다.
> **특징**: 런타임 로그 분석, 물리 설정 검증, 빌드 안정성 체크 전담. 작업 시작 전 반드시 `AGENTS.md`를 확인하십시오.

## 3. Communication Standards

에이전트 간 또는 에이전트에게 내리는 명령 프롬프트는 아래 표준을 따릅니다.

### 3.1. PR 피드백 및 수정 요청 (Comment Prompt)
AI가 생성한 PR에 오류가 있거나 보완이 필요한 경우 사용하는 전용 프롬프트입니다.
- **필수 포함**: 수정 대상 파일 경로, 에러 로그 또는 구체적인 문제 증상, `AGENTS.md` 확인 지시.

### 3.2. 빌드 오류 수정 요청
컴파일 에러나 런타임 예외 발생 시 사용하는 긴급 수정 프롬프트입니다.
- **필수 포함**: `Compiler Output` 또는 `StackTrace`, 관련 파일 목록, `AGENTS.md` 준수 강조.

### 3.3. Merge Conflict 해결 요청
브랜치 병합 시 발생하는 컨플릭트 해결을 위한 프롬프트입니다.
- **필수 포함**: 컨플릭트가 발생한 코드 블록(<<<<<<<, =======, >>>>>>>), `master` 브랜치의 기준 로직 설명.

## 4. 관리 규칙
1. **일관성**: 모든 신규 프롬프트 파일은 위 템플릿을 복사하여 작성합니다.
2. **업데이트**: `AGENTS.md`의 전역 규칙이 변경되면, 이 문서의 템플릿도 즉시 동기화되어야 합니다.

---
*Last Updated: 2026-05-03*
