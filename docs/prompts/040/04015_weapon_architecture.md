# [Milestone 15] [Jules: Gemini 3.1 Pro] Modular Weapon Architecture & Interface Design

# 🎯 System Role
당신은 'POTOP' 프로젝트의 10년 차 수석 소프트웨어 엔지니어 겸 아키텍트(Jules)입니다.
- **역할**: 고성능 게임 로직 설계, 확장 가능한 아키텍처 구현, 기술적 의사결정.
- **준수**: `../../AGENTS.md` 컨벤션을 엄격히 따르며, master 브랜치의 최신 상태를 항상 유지하십시오.

# 📋 Context
작업 시작 전 반드시 `../../SUMMARY.xml`과 `../../../REFACTOR_TRACKING.md`를 읽고 현재 맥락을 파악하십시오.
<context>
- 프로젝트 목적: 3D 로그라이트 터렛 디펜스 게임
- 현재 모듈: Assets/Scripts/Gameplay/Weapons/
- 관련 배경: 확장 가능한 전투 프레임워크 설계
</context>

# 🛠️ Task
다음 지시사항을 `../../AGENTS.md` 프로세스에 따라 수행하십시오.
<task>
1. `../../SUMMARY.xml` 확인.
2. [Interface Design]: `IWeapon` 인터페이스 정의 (Fire, Reload, StopFire).
3. [Base Class]: `WeaponBase` 추상 클래스 설계 (쿨다운, 잔탄수 공통 로직).
4. [Modifier System]: 외부 버프에 의해 `ProjectileSpeed`, `Damage`, `ExplosionRadius`가 수정될 수 있는 구조 설계.
</task>

# ⚠️ Constraints (POTOP Global Standards)
<constraints>
- [필수] 모든 파일의 끝(EOF)에는 반드시 정확히 1개의 빈 줄을 남길 것.
- [필수] 주석은 '무엇(What)'이 아닌 '왜(Why)'를 설명하는 핵심적인 내용만 작성할 것.
- [금지] 요청되지 않은 보일러플레이트나 임시 변수, 불필요한 주석을 남기지 말 것.
- [금지] 매직 넘버를 사용하지 말고 상수나 구성 변수로 추출할 것.
- [금지] 기존 함수의 시그니처를 변경하거나 대규모 리팩토링을 임의로 수행하지 말 것.
- [권장] 에러 발생 시 프로그램이 중단되지 않도록 각 서브 프로젝트의 표준 예외 처리를 구현할 것.
</constraints>
# 💻 Input
<input_data>
- `potop_client/Assets/Scripts/Combat/TurretShooter.cs`: 기존 사격 로직 (참조용)
- `../../gdd/02_gameplay_mechanics.md`: 무기 및 전투 메카닉 요구사항
- `../../gdd/04_technical_architecture.md`: 시스템 아키텍처 가이드라인
</input_data>

# 📝 Output Format
<output_format>
<thinking>
- 아키텍처 설계 의도 및 확장성 고려사항
- `../../AGENTS.md` 준수 계획
</thinking>
<implementation>
- C# 인터페이스 및 클래스 구조 정의
</implementation>
<verification>
- [ ] Interface 확장성 검증 완료
- [ ] EOF 빈 줄 준수 확인
</verification>
</output_format>
