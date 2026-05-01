## **7. 개발 마일스톤 (Development Milestones)**

### **Phase 1: 핵심 게임 루프 및 기초 시스템 (MVP)**
> [!TIP]
> AI Prompts: [01_core_loop_mvp.md](../prompts/01_core_loop_mvp.md)

*   **[Milestone 1] 기본 포탑 회전 및 투사체 발사**
    *   **내용:** 입력 시스템에 따른 포탑 회전 로직 및 기본 사격 구현.
*   **[Milestone 2] 1인칭 마우스 룩**
    *   **내용:** FPS 방식의 시점 제어 및 클램핑.
*   **[Milestone 3] 적 스폰 및 기초 AI**
    *   **내용:** 360도 전 방향 스폰 및 플레이어 추적.
*   **[Milestone 4] GameManager 및 기본 HUD**
    *   **내용:** 게임 상태 관리 싱글톤 및 기본 UI 표시.
*   **[Milestone 5] URP 및 물리 설정**
    *   **내용:** 그래픽 파이프라인 최적화 및 레이어 충돌 설정.

---

### **Phase 1.5: 기술 부채 해결 및 기본 시스템 안정화 (Stability & Refactoring)**
> [!TIP]
> AI Prompts: [01_5_stability_refactoring.md](../prompts/01_5_stability_refactoring.md)

*   **[Milestone 6] 코드 스타일 및 파일 무결성 정비**
    *   **내용:** AGENTS.md 규격에 따른 파일 정비 및 레거시 속성 제거.
*   **[Milestone 7] 입력 시스템 고도화 및 회전 로직 통합**
    *   **내용:** 마우스 입력 처리 방식 통일 및 회전 간섭 해결.
*   **[Milestone 8] Unity 환경 설정 및 링크 검증**
    *   **내용:** 에디터 설정(URP, Physics) 및 프리팹 할당 상태 전수 조사.

---

### **Phase 2: 시스템 확장 및 구조 고도화**
> [!TIP]
> AI Prompts: [02_system_expansion.md](../prompts/02_system_expansion.md)

*   **[Milestone 9] 이벤트 브로커 시스템**
    *   **내용:** `Action<T>` 기반 중앙 이벤트 버스를 통한 디커플링.
*   **[Milestone 10] 오브젝트 풀링 매니저**
    *   **내용:** `UnityEngine.Pool`을 활용한 런타임 최적화.
*   **[Milestone 11] ScriptableObject 기반 데이터화**
    *   **내용:** 에셋 기반 데이터 관리 (Enemy, Weapon, Wave).
*   **[Milestone 12] 웨이브 매니저 구현**
    *   **내용:** 시간 경과에 따른 난이도 및 스폰 규칙 관리.
*   **[Milestone 13] 무기 시스템 아키텍처**
    *   **내용:** 인터페이스 기반 확장형 무기 시스템 설계.

---

### **Phase 3: 로그라이트 성장 및 투사체 진화**
> [!TIP]
> AI Prompts: [03_roguelite_progression.md](../prompts/03_roguelite_progression.md)

*   **[Milestone 14] 레벨업 시스템 및 UI**
    *   **내용:** 경험치 기반 성장 및 3개 무작위 강화 선택.
*   **[Milestone 15] 투사체 변이 물리 로직**
    *   **내용:** 다연발, 관통, 도탄, 폭발 등 물리적 속성 확장.

---

### **Phase 4: 콘텐츠 폴리싱 및 모바일 출시 준비**
> [!TIP]
> AI Prompts: [04_polishing_mobile.md](../prompts/04_polishing_mobile.md)

*   **[Milestone 16] 보스전: 타이탄 코어**
    *   **내용:** 15분 시점 보스 페이즈 전환 및 패턴 구현.
*   **[Milestone 17] 오버클럭 모드 (무한)**
    *   **내용:** 보스 격파 후 기하급수적 난이도 상승 로직.
*   **[Milestone 18] 특수 적 AI 구현**
    *   **내용:** 블리츠, 아머드, 스웜, 헬파이어 AI 고도화.
*   **[Milestone 19] 카메라 쉐이크 연출**
    *   **내용:** 타격감 향상을 위한 카메라 진동 로직 및 피격/사격 시 흔들림 강도 제어.
*   **[Milestone 20] 피버 타임 시스템**
    *   **내용:** 특정 킬 카운트 달성 시 일시적인 공격 속도 강화 및 시각적 피드백 제공.
*   **[Milestone 21] 사운드 시스템 통합**
    *   **내용:** 배경음악(BGM) 및 효과음(SFX) 관리 시스템 구축 및 오디오 리소스 연동.
*   **[Milestone 22] 모바일 광고 연동**
    *   **내용:** 리워드 광고 및 전면 광고 SDK 통합을 통한 수익화 구조 구축.
*   **[Milestone 23] 다국어 지원 (Multilingual Support)**
    *   **내용:** 글로벌 서비스를 위한 다국어(한국어, 영어, 일본어) 텍스트 로컬라이제이션 시스템.

---

### **Phase 5: 글로벌 리더보드 및 서버 구축**
*   **[Milestone 24] 서버 인프라 설정**
    *   **내용:** Firebase 또는 전용 서버를 통한 유저 데이터 연동.
*   **[Milestone 25] 실시간 랭킹 시스템**
    *   **내용:** 오버클럭 모드 점수 자동 업로드 및 글로벌 리더보드 UI 구현.
*   **[Milestone 26] 클라우드 세이브**
    *   **내용:** 계정 연동을 통한 기기 간 데이터 동기화.

---

### **Phase 6: PC 버전 출시 및 플랫폼 확장**
*   **[Milestone 27] SteamWorks SDK 연동**
    *   **내용:** 스팀 도전과제, 리더보드, 클라우드 저장소 연동.
*   **[Milestone 28] PC 최적화 및 조작 개선**
    *   **내용:** 고해상도 텍스처 적용 및 마우스/키보드 정밀 조작(Key Rebinding) 지원.
*   **[Milestone 29] PC 전용 콘텐츠 추가**
    *   **내용:** 와이드 모니터 지원 및 그래픽 옵션 세분화.

---

### **Phase 7: VR 버전 이식 및 몰입형 경험**
*   **[Milestone 30] SteamVR / OpenXR 도입**
    *   **내용:** VR HMD 및 모션 컨트롤러 대응.
*   **[Milestone 31] VR 전용 UI/UX 리디자인**
    *   **내용:** 월드 스페이스 UI 및 3D 인터랙션 시스템 구축.
*   **[Milestone 32] 공간 음향(Spatial Audio) 최적화**
    *   **내용:** 360도 전 방향에서 오는 적의 위치를 소리로 파악할 수 있는 사운드 시스템.

---

### **Phase 8: 콘솔 플랫폼 이식 (Console)**
*   **[Milestone 33] 콘솔 개발 키트 대응**
    *   **내용:** Nintendo Switch, PS5, Xbox 등 플랫폼별 최적화 및 퍼포먼스 튜닝.
*   **[Milestone 34] 컨트롤러 진동(Haptic) 피드백**
    *   **내용:** 각 무기별 특색 있는 진동 패턴 적용.
*   **[Milestone 35] 최종 스토어 심사 및 글로벌 런칭**
    *   **내용:** 각 플랫폼별 가이드라인(TRC/XR) 준수 확인 및 정식 출시.

