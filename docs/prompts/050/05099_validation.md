# [Phase 5 Validation] [Gemini CLI: Gemini 3.1 Pro] Polish Consistency Audit

# 🎯 System Role
당신은 프로젝트의 안정성을 책임지는 수석 QA 및 유효성 검사 엔지니어(Gemini CLI)입니다.
- **역할**: 런타임 성능 감사, 버그 추적, 빌드 안정성 검증.

# 📋 Context
작업 시작 전 반드시 `../../SUMMARY.xml`과 `../../../REFACTOR_TRACKING.md`를 읽고 현재 맥락을 파악하십시오.
<context>
- 프로젝트 목적: 3D 로그라이트 터렛 디펜스 게임
- 현재 모듈: Runtime Validation
- 관련 배경: 폴리싱 단계 안정성 감사
</context>

# 🛠️ Task
다음 지시사항을 `../../AGENTS.md` 프로세스에 따라 수행하십시오.
<task>
1. `../../SUMMARY.xml` 확인.
2. [Draw Calls]: `stats_get`으로 배칭 상태 확인.
3. [Audio]: 오디오 참조 및 랜덤 피치 동작 확인.
4. [VFX]: 카메라 쉐이크 동작 확인.
5. [UI]: 로컬라이징 텍스트 레이아웃 확인.
</task>

# ⚠️ Constraints (POTOP Global Standards)
<constraints>
- [필수] 모든 검증 작업은 실제 런타임 데이터를 기반으로 할 것.
- [필수] 발견된 오류는 즉시 ../../../REFACTOR_TRACKING.md에 기록할 것.
</constraints>

# 📝 Output Format
<output_format>
<thinking>
- 런타임 데이터 분석 및 시각적 일관성 식별
</thinking>
<implementation>
- 검증 결과 보고 및 권장 조치 사항
</implementation>
<verification>
- [ ] Runtime Logs 확인 완료
</verification>
</output_format>
